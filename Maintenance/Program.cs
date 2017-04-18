using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.ServiceProcess;
using System.Linq;
using System.Management;
using System.Collections.Specialized;
using System.Configuration;

namespace Maintenance
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // App.config Values
            var PathFilesToDelete = ConfigurationManager.GetSection("PathFilesToDelete") as NameValueCollection;
            var FilesToDelete = ConfigurationManager.GetSection("FilesToDelete") as NameValueCollection;
            var FilesToHide = ConfigurationManager.GetSection("FilesToHide") as NameValueCollection;
            var TasksToDisable = ConfigurationManager.GetSection("TasksToDisable") as NameValueCollection;
            var ServicesToManual = ConfigurationManager.GetSection("ServicesToManual") as NameValueCollection;

            // Run Disk Check once a month before 11 am on Monday
            DateTime today = DateTime.Today;
            if (today.DayOfWeek == DayOfWeek.Monday && today.Day <= 7 && DateTime.Now.Hour <= 11)
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "CMD.exe";
                    process.StartInfo.Arguments = "/c echo Y | chkdsk /F C:";
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.Start();
                    process.WaitForExit();
                }
            }
            // Disable Tasks
            if (TasksToDisable != null)
            {
                foreach (var tasks in TasksToDisable)
                {
                    string tasksToDisable = TasksToDisable.GetValues(tasks.ToString()).FirstOrDefault();
                    if (tasksToDisable != "")
                        taskexistance(tasksToDisable);
                }
            }
            // Disable Services
            if (ServicesToManual != null)
            {
                foreach (var services in ServicesToManual)
                {
                    string servicesToManual = ServicesToManual.GetValues(services.ToString()).FirstOrDefault();
                    if (servicesToManual != "")
                    {
                        if (ServiceExists(servicesToManual) && ServiceStatus(servicesToManual) != "Manual")
                        {
                            DisableService(servicesToManual);
                        }
                    }
                }
            }
            // Hide Files
            if (FilesToHide != null)
            {
                foreach (var hide in FilesToHide)
                {
                    string filesToHide = FilesToHide.GetValues(hide.ToString()).FirstOrDefault();
                    var Variable = filesToHide;
                    var filePath = Environment.ExpandEnvironmentVariables(Variable);
                    if (filePath != "")
                    {
                        if (File.Exists(filePath))
                        {
                            FileAttributes attributes = File.GetAttributes(filePath);
                            if (attributes != FileAttributes.Hidden || attributes != FileAttributes.System)
                            {
                                File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Hidden);
                                File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.System);
                            }
                        }
                        else if (Directory.Exists(filePath))
                        {
                            FileAttributes attributes = File.GetAttributes(filePath);
                            if (attributes != FileAttributes.Hidden || attributes != FileAttributes.System)
                            {
                                File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Hidden);
                                File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.System);
                            }
                        }
                    }
                }
            }
            // Delete Files in a Directory
            if (PathFilesToDelete != null)
            {
                int deleteTemp = 0;
                foreach (var paths in PathFilesToDelete)
                {
                    string Variable = PathFilesToDelete.GetValues(paths.ToString()).FirstOrDefault();
                    var filesPath = Environment.ExpandEnvironmentVariables(Variable);
                    if (filesPath != "")
                    {
                        foreach (string file in Directory.GetFiles(filesPath))
                        {
                            try
                            {
                                if (!file.Contains(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Temp"))
                                {
                                    File.Delete(file);
                                }
                                else
                                {
                                    deleteTemp++;
                                    if (deleteTemp == 1)
                                    {
                                        DirectoryInfo directory = new DirectoryInfo(Path.GetDirectoryName(file));
                                        directory.DeleteTemp();
                                    }
                                }
                            }
                            catch (IOException)
                            {
                                continue;
                            }
                        }
                        foreach (string directory in Directory.GetDirectories(filesPath))
                        {
                            try
                            {
                                Directory.Delete(directory, true);
                            }
                            catch (IOException)
                            {
                                continue;
                            }
                        }
                        try
                        {
                            Directory.Delete(filesPath, true);
                        }
                        catch (IOException)
                        {
                            continue;
                        }
                    }
                }
            }
            // Delete Files
            if (FilesToDelete != null)
            {
                foreach (var file in FilesToDelete)
                {
                        string fileTodelete = FilesToDelete.GetValues(file.ToString()).FirstOrDefault();
                    if (fileTodelete != "")
                        File.Delete(fileTodelete);
                }
            }
            Environment.Exit(0);
        }
        // Set File Attrubutes
        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }
        // Disable Sceduled Tasks
        static void taskexistance(string taskname)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "schtasks.exe";
            start.UseShellExecute = false;
            start.CreateNoWindow = true;
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.Arguments = "/query /TN " + "\"" + taskname + "\"";
            start.RedirectStandardOutput = true;

            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string stdout = reader.ReadToEnd();
                    if (stdout.Contains(taskname))
                    {
                        if (stdout.Contains("Ready"))
                        {
                            ProcessStartInfo info = new ProcessStartInfo();
                            info.FileName = "schtasks.exe";
                            info.UseShellExecute = false;
                            info.CreateNoWindow = true;
                            info.WindowStyle = ProcessWindowStyle.Hidden;
                            info.Arguments = "/change /TN " + "\"" + taskname + "\"" + " /DISABLE";
                            info.RedirectStandardOutput = true;
                            using (Process proc = Process.Start(info))
                            {
                                proc.WaitForExit();
                            }
                        }
                    }
                }
            }
        }
        // Set Services to manual that you don't want running
        static string status;
        static bool ServiceExists(string serviceName)
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
        public static void DisableService(string serviceName)
        {
            using (var mo = new ManagementObject(string.Format("Win32_Service.Name=\"{0}\"", serviceName)))
            {
                mo.InvokeMethod("ChangeStartMode", new object[] { "Manual" });
            }
        }
        public static void DeleteTemp(this DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                try
                {
                    if (DateTime.UtcNow - file.CreationTimeUtc > TimeSpan.FromHours(6))
                    {
                        file.Delete();
                    }
                }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
            }
            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                try
                {
                    subDirectory.Delete(true);
                }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
            }
        }
    }
}
