using Logger;
using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using static Maintenance.Properties.Settings;

namespace Maintenance
{
    public class DisableServices
    {
        public static void SetServices()
        {
            // Services To Manual
            foreach (string service in Default.ServicesToManual)
            {
                try
                {
                    if (ServiceStatus(service) != "Manual")
                    {
                        SetService(service, "Manual");
                        StopServices(service);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            // Services To Disable
            foreach (string service in Default.ServicesToDisable)
            {
                try
                {
                    if (ServiceStatus(service) != "Disabled")
                    {
                        SetService(service, "Disabled");
                        StopServices(service);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        public static void SetService(string serviceName, string status)
        {
            using (var mo = new ManagementObject(string.Format("Win32_Service.Name=\"{0}\"", serviceName)))
            {
                Logging.Info("Setting service: " + serviceName + " to: " + status, "DisableServices");

                mo.InvokeMethod("ChangeStartMode", new object[] { status });
            }
        }

        private static void StopServices(string serviceName)
        {
            using (Process proc = new Process())
            {
                proc.StartInfo.Arguments = "stop " + "\"" + serviceName + "\"";
                proc.StartInfo.FileName = "sc";
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                proc.WaitForExit();
            }
        }

        static string ServiceStatus(string serviceName)
        {
            string status = string.Empty;

            if (ServiceExists(serviceName))
            {
                string wmiQuery = "SELECT * FROM Win32_Service WHERE Name='" + serviceName + "\'";
                var searcher = new ManagementObjectSearcher(wmiQuery);
                var results = searcher.Get();

                foreach (ManagementObject service in results)
                {
                    status = (service["StartMode"]).ToString();
                }
            }

            return status;
        }

        private static bool ServiceExists(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            var service = services.FirstOrDefault(s => s.ServiceName == serviceName);
            return service != null;
        }
    }
}
