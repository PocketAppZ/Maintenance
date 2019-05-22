using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Maintenance
{
    public class DisableTasks
    {
        public static void SetTasks()
        {
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
                            Trace.WriteLine(DateTime.Now + "   |     Disabling task: " + taskname);
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
    }
}
