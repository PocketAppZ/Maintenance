using System;
using System.IO;
using System.Diagnostics;
using System.ServiceProcess;
using System.Linq;
using System.Management;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows.Forms;

namespace Maintenance
{
    static class Program
    {
        static DateTime today = DateTime.Today;
        static string LogDirectory = Environment.CurrentDirectory + @"\Log";
        static string LogFile = LogDirectory + @"\Application.log";
        static string LogBak = LogDirectory + @"\Application.log.bak";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                if (File.Exists(LogFile))
                {
                    File.Copy(LogFile, LogBak, true);
                    File.Delete(LogFile);
                }
                // Check if App.config Exists
                if (ConfigurationManager.GetSection("PathFilesToDelete") as NameValueCollection == null)
                {
                    MessageBox.Show("Either the configuration File is missing or is corrupt.\n\nYou will need to recreate this file in order for the application to work correctly and process any settings.", "No Configuration File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
                // Log all instances
                if (ConfigurationManager.GetSection("Logging") is NameValueCollection Logging)
                {
                    foreach (var value in Logging)
                    {
                        string logging = Logging.GetValues(value.ToString()).FirstOrDefault();
                        if (logging != "")
                        {
                            if (logging == "true")
                            {
                                if (!Directory.Exists(LogDirectory))
                                {
                                    Directory.CreateDirectory(LogDirectory);
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
                                Trace.WriteLine("-- Application has started " + DateTime.Now);
                            }
                        }
                    }
                }
                // Run Disk Check once a month on next reboot from Monday's first boot up
                string checkFile = "C:\\checkFile";
                if (today.DayOfWeek == DayOfWeek.Monday && today.Day <= 7 && !File.Exists(checkFile))
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
                    File.Create(checkFile);
                    FileAttributes attributes = File.GetAttributes(checkFile);
                    if (attributes != FileAttributes.Hidden || attributes != FileAttributes.System)
                    {
                        Trace.WriteLine(DateTime.Now + " Hiding file: " + checkFile);
                        File.SetAttributes(checkFile, File.GetAttributes(checkFile) | FileAttributes.Hidden);
                        File.SetAttributes(checkFile, File.GetAttributes(checkFile) | FileAttributes.System);
                    }
                }
                if (today.DayOfWeek == DayOfWeek.Tuesday && today.Day > 7 && File.Exists(checkFile))
                {
                    File.Delete(checkFile);
                }
                // Disable Tasks
                if (ConfigurationManager.GetSection("TasksToDisable") as NameValueCollection != null)
                {
                    foreach (var tasks in ConfigurationManager.GetSection("TasksToDisable") as NameValueCollection)
                    {
                        string tasksToDisable = (ConfigurationManager.GetSection("TasksToDisable") as NameValueCollection).GetValues(tasks.ToString()).FirstOrDefault();
                        if (tasksToDisable != "")
                            Taskexistance(tasksToDisable);
                    }
                }
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
                // Hide Files
                if (ConfigurationManager.GetSection("FilesToHide") as NameValueCollection != null)
                {
                    foreach (var hide in ConfigurationManager.GetSection("FilesToHide") as NameValueCollection)
                    {
                        string filesToHide = (ConfigurationManager.GetSection("FilesToHide") as NameValueCollection).GetValues(hide.ToString()).FirstOrDefault();
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
                if (ConfigurationManager.GetSection("PathFilesToDelete") as NameValueCollection != null)
                {
                    int deleteTemp = 0;
                    Retry:;
                    foreach (var paths in ConfigurationManager.GetSection("PathFilesToDelete") as NameValueCollection)
                    {
                        try
                        {
                            string Variable = (ConfigurationManager.GetSection("PathFilesToDelete") as NameValueCollection).GetValues(paths.ToString()).FirstOrDefault();
                            var filesPath = Environment.ExpandEnvironmentVariables(Variable);
                            if (filesPath != "")
                            {
                                try
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
                                        catch (Exception)
                                        {
                                            foreach (var catcher in ConfigurationManager.GetSection("PathFilesToDeleteCatch") as NameValueCollection)
                                            {
                                                try
                                                {
                                                    string catcherVariable = (ConfigurationManager.GetSection("PathFilesToDeleteCatch") as NameValueCollection).GetValues(catcher.ToString()).FirstOrDefault();

                                                    var catcherPath = Environment.ExpandEnvironmentVariables(catcherVariable);
                                                    if (catcherPath == filesPath)
                                                    {
                                                        DialogResult result = MessageBox.Show(filesPath + " cannot be deleted at this time. Please check that a process is not holding it and then try again.", "Maintenance", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation);
                                                        if (result == DialogResult.Abort)
                                                        {
                                                            Environment.Exit(0);
                                                        }
                                                        else if (result == DialogResult.Retry)
                                                        {
                                                            goto Retry;
                                                        }
                                                        else if (result == DialogResult.Ignore)
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                }
                                                catch(Exception)
                                                {
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    continue;
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
                if (ConfigurationManager.GetSection("PathFilesToDeleteOlder") as NameValueCollection != null)
                {
                    foreach (var paths in ConfigurationManager.GetSection("PathFilesToDeleteOlder") as NameValueCollection)
                    {
                        try
                        {
                            string Variable = (ConfigurationManager.GetSection("PathFilesToDeleteOlder") as NameValueCollection).GetValues(paths.ToString()).FirstOrDefault();
                            var filesPath = Environment.ExpandEnvironmentVariables(Variable);
                            if (filesPath != "")
                            {
                                // Files to delete
                                foreach (string d in Directory.GetDirectories(filesPath))
                                {
                                    foreach (string f in Directory.GetFiles(d))
                                    {
                                        if (ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection != null)
                                        {
                                            foreach (var days in ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection)
                                            {
                                                string DaysNumberValue = (ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection).GetValues(days.ToString()).FirstOrDefault();
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
                                            }
                                        }
                                    }
                                }
                                foreach (string file in Directory.GetFiles(filesPath))
                                {
                                    if (ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection != null)
                                    {
                                        foreach (var days in ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection)
                                        {
                                            string DaysNumberValue = (ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection).GetValues(days.ToString()).FirstOrDefault();
                                            int DaysNumber = Convert.ToInt32(DaysNumberValue);
                                            if (paths.ToString() == days.ToString())
                                            {
                                                try
                                                {
                                                    FileInfo fi = new FileInfo(file);
                                                    if (fi.CreationTime < DateTime.Now.AddDays(-DaysNumber))
                                                    {
                                                        Trace.WriteLine(DateTime.Now + " Deleting file: " + file);
                                                        File.Delete(file);
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
                                }
                                // Directories to delete
                                foreach (string d in Directory.GetDirectories(filesPath))
                                {
                                    if (ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection != null)
                                    {
                                        foreach (var days in ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection)
                                        {
                                            string DaysNumberValue = (ConfigurationManager.GetSection("PathFilesToDeleteDays") as NameValueCollection).GetValues(days.ToString()).FirstOrDefault();
                                            int DaysNumber = Convert.ToInt32(DaysNumberValue);

                                            if (paths.ToString() == days.ToString())
                                            {
                                                try
                                                {
                                                    DirectoryInfo fi = new DirectoryInfo(d);
                                                    if (fi.CreationTime < DateTime.Now.AddDays(-DaysNumber))
                                                    {
                                                        Trace.WriteLine(DateTime.Now + " Deleting file: " + d);
                                                        Directory.Delete(d, true);
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
                if (ConfigurationManager.GetSection("FilesToDelete") as NameValueCollection != null)
                {
                    foreach (var file in ConfigurationManager.GetSection("FilesToDelete") as NameValueCollection)
                    {
                        string fileTodelete = (ConfigurationManager.GetSection("FilesToDelete") as NameValueCollection).GetValues(file.ToString()).FirstOrDefault();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }
        // Set File Attrubutes
        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }
        // Disable Sceduled Tasks
        static void Taskexistance(string taskname)
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
        // C:\Windows\System32
        public static void StopServices(string serviceName)
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
