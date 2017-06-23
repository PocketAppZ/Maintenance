using System;
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
        static DateTime today = DateTime.Today;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // App.config Values
            var PathFilesToDelete = ConfigurationManager.GetSection("PathFilesToDelete") as NameValueCollection;
            var PathFilesToDeleteOlder = ConfigurationManager.GetSection("PathFilesToDeleteOlder") as NameValueCollection;
            var PathFilesToDeleteDays = ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection;
            var FilesToDelete = ConfigurationManager.GetSection("FilesToDelete") as NameValueCollection;
            var FilesToHide = ConfigurationManager.GetSection("FilesToHide") as NameValueCollection;
            var TasksToDisable = ConfigurationManager.GetSection("TasksToDisable") as NameValueCollection;
            var ServicesToManual = ConfigurationManager.GetSection("ServicesToManual") as NameValueCollection;
            var Logging = ConfigurationManager.GetSection("Logging") as NameValueCollection;

            // Log all instances
            if (Logging != null)
            {
                foreach (var value in Logging)
                {
                    string logging = Logging.GetValues(value.ToString()).FirstOrDefault();
                    if (logging != "")
                    {
                        if (logging == "true")
                        {
                            string LogDirectory = Environment.CurrentDirectory + @"\Log";
                            string LogFile = LogDirectory + @"\Application.log";
                            string LogBak = LogDirectory + @"\Application.log.bak";
                            if (!Directory.Exists(LogDirectory))
                            {
                                Directory.CreateDirectory(LogDirectory);
                            }
                            FileInfo fi = new FileInfo(LogFile);
                            if (fi.LastWriteTime < DateTime.Now.AddDays(-7))
                            {
                                if (File.Exists(LogBak))
                                {
                                    File.Delete(LogBak);
                                }
                                if (File.Exists(LogFile))
                                {
                                    File.Copy(LogFile, LogBak);
                                    File.Delete(LogFile);
                                }
                            }
                            Trace.Listeners.Clear();
                            TextWriterTraceListener twtl = null;
                            try
                            {
                                twtl = new TextWriterTraceListener(LogFile);
                                ConsoleTraceListener ctl = null;
                                try
                                {
                                    ctl = new ConsoleTraceListener(false);
                                    Trace.Listeners.Add(twtl);
                                    Trace.Listeners.Add(ctl);
                                    Trace.AutoFlush = true;
                                }
                                finally
                                {
                                    if (ctl != null)
                                    {
                                        ctl.Dispose();
                                    }
                                }
                            }
                            finally
                            {
                                if (twtl != null)
                                {
                                    twtl.Dispose();
                                }
                            }
                            Trace.WriteLine(DateTime.Now + " Application has started");
                        }
                    }
                }
            }
            // Run Disk Check once a month before 11 am on Monday
            if (today.DayOfWeek == DayOfWeek.Monday && today.Day <= 7 && DateTime.Now.Hour <= 11)
            {
                Trace.WriteLine(DateTime.Now + " Scheduling a disk check to run at next reboot.");
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
                                Trace.WriteLine(DateTime.Now + " Hiding file: " + filePath);
                                File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Hidden);
                                File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.System);
                            }
                        }
                        else if (Directory.Exists(filePath))
                        {
                            Trace.WriteLine(DateTime.Now + " Hiding directory: " + filePath);
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
            // Delete Files and Folders in a Directory
            if (PathFilesToDelete != null)
            {
                int deleteTemp = 0;
                foreach (var paths in PathFilesToDelete)
                {
                    try
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
                                        Trace.WriteLine(DateTime.Now + " Deleting file: " + file);
                                        File.Delete(file);
                                    }
                                    else
                                    {
                                        deleteTemp++;
                                        if (deleteTemp == 1)
                                        {
                                            Trace.WriteLine(DateTime.Now + " Deleting temp files");
                                            DirectoryInfo directory = new DirectoryInfo(Path.GetDirectoryName(file));
                                            directory.DeleteTemp();
                                        }
                                    }
                                }
                                catch (FileNotFoundException)
                                {
                                    continue;
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
                                    Trace.WriteLine(DateTime.Now + " Deleting directory: " + directory);
                                    Directory.Delete(directory, true);
                                }
                                catch (DirectoryNotFoundException)
                                {
                                    continue;
                                }
                                catch (IOException)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    catch (DirectoryNotFoundException)
                    {
                        continue;
                    }
                }
            }
            // Delete Files and Folders in a Directory older than 1 day
            if (PathFilesToDeleteOlder != null)
            {
                foreach (var paths in PathFilesToDeleteOlder)
                {
                    try
                    {
                        string Variable = PathFilesToDeleteOlder.GetValues(paths.ToString()).FirstOrDefault();
                        var filesPath = Environment.ExpandEnvironmentVariables(Variable);
                        if (filesPath != "")
                        {
                            foreach (string d in Directory.GetDirectories(filesPath))
                            {
                                foreach (string f in Directory.GetFiles(d))
                                {
                                    if (PathFilesToDeleteDays != null)
                                    {
                                        foreach (var days in PathFilesToDeleteDays)
                                        {
                                            string DaysNumberValue = PathFilesToDeleteDays.GetValues(days.ToString()).FirstOrDefault();
                                            int DaysNumber = Convert.ToInt32(DaysNumberValue);
                                            if (paths.ToString() == days.ToString())
                                            {
                                                try
                                                {
                                                    FileInfo fi = new FileInfo(f);
                                                    if (fi.CreationTime < DateTime.Now.AddDays(-DaysNumber))
                                                    {
                                                        Trace.WriteLine(DateTime.Now + " Deleting file: " + f);
                                                        File.Delete(f);
                                                    }
                                                }
                                                catch (UnauthorizedAccessException)
                                                {
                                                    continue;
                                                }
                                                catch (FileNotFoundException)
                                                {
                                                    continue;
                                                }
                                                catch (IOException)
                                                {
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    FileInfo fi = new FileInfo(f);
                                                    if (fi.CreationTime < DateTime.Now.AddDays(-1))
                                                    {
                                                        Trace.WriteLine(DateTime.Now + " Deleting file: " + f);
                                                        File.Delete(f);
                                                    }
                                                }
                                                catch (UnauthorizedAccessException)
                                                {
                                                    continue;
                                                }
                                                catch (FileNotFoundException)
                                                {
                                                    continue;
                                                }
                                                catch (IOException)
                                                {
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            FileInfo fi = new FileInfo(f);
                                            if (fi.CreationTime < DateTime.Now.AddDays(-1))
                                            {
                                                Trace.WriteLine(DateTime.Now + " Deleting file: " + f);
                                                File.Delete(f);
                                            }
                                        }
                                        catch (UnauthorizedAccessException)
                                        {
                                            continue;
                                        }
                                        catch (FileNotFoundException)
                                        {
                                            continue;
                                        }
                                        catch (IOException)
                                        {
                                            continue;
                                        }
                                    }
                                }
                            }
                            foreach (string directory in Directory.GetDirectories(filesPath))
                            {
                                try
                                {
                                    DirectoryInfo di = new DirectoryInfo(directory);
                                    if (di.CreationTime < DateTime.Now.AddDays(-1))
                                    {
                                        Trace.WriteLine(DateTime.Now + " Deleting directory: " + directory);
                                        Directory.Delete(directory, true);
                                    }
                                }
                                catch (UnauthorizedAccessException)
                                {
                                    continue;
                                }
                                catch (DirectoryNotFoundException)
                                {
                                    continue;
                                }
                                catch (IOException)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    catch (DirectoryNotFoundException)
                    {
                        continue;
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
                    {
                        Trace.WriteLine(DateTime.Now + " Deleting file: " + fileTodelete);
                        File.Delete(fileTodelete);
                    }
                }
            }
            Trace.WriteLine("\n");
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
                            Trace.WriteLine(DateTime.Now + " Disabling task: " + taskname);
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
                Trace.WriteLine(DateTime.Now + " Setting service: " + serviceName + " to manual.");
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
