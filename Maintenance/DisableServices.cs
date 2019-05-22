using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.ServiceProcess;

namespace Maintenance
{
    public class DisableServices
    {
        public static void SetServices()
        {
            // Disable Services
            if (ConfigurationManager.GetSection("ServicesToManual") as NameValueCollection != null)
            {
                foreach (var services in ConfigurationManager.GetSection("ServicesToManual") as NameValueCollection)
                {
                    string servicesToManual = (ConfigurationManager.GetSection("ServicesToManual") as NameValueCollection).GetValues(services.ToString()).FirstOrDefault();
                    if (servicesToManual != "")
                    {
                        if (ServiceExists(servicesToManual) && ServiceStatus(servicesToManual) != "Manual")
                        {
                            DisableService(servicesToManual);
                            StopServices(servicesToManual);
                        }
                    }
                }
            }
        }

        public static void DisableService(string serviceName)
        {
            using (var mo = new ManagementObject(string.Format("Win32_Service.Name=\"{0}\"", serviceName)))
            {
                Trace.WriteLine(DateTime.Now + "   |     Setting service: " + serviceName + " to manual.");
                mo.InvokeMethod("ChangeStartMode", new object[] { "Manual" });
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

        // Set Services to manual that you don't want running
        private static string status;
        private static bool ServiceExists(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            var service = services.FirstOrDefault(s => s.ServiceName == serviceName);
            return service != null;
        }
        static string ServiceStatus(string serviceName)
        {
            string wmiQuery = "SELECT * FROM Win32_Service WHERE Name='" + serviceName + "\'";
            var searcher = new ManagementObjectSearcher(wmiQuery);
            var results = searcher.Get();

            foreach (ManagementObject service in results)
            {
                status = (service["StartMode"]).ToString();
            }
            return status;
        }
    }
}
