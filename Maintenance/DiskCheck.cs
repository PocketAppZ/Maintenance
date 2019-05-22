using System;
using System.Diagnostics;
using System.IO;

namespace Maintenance
{
    public class DiskCheck
    {
        static DateTime today = DateTime.Today;
        static string[] drives = Directory.GetLogicalDrives();

        public static void ScheduleCheck()
        {
            // Run Disk Check once a month on next reboot from Monday's first boot up
            string checkFile = "C:\\checkFile";
            if (today.DayOfWeek == DayOfWeek.Monday && today.Day <= 7 && !File.Exists(checkFile))
            {
                Trace.WriteLine(DateTime.Now + "   |     Scheduling a disk check to run at next reboot.");

                using (Process process = new Process())
                {
                    foreach (string drive in drives)
                    {
                        process.StartInfo.FileName = "CMD.exe";
                        process.StartInfo.Arguments = "/c echo Y | chkdsk /F " + drive.Replace("\\", "");
                        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        process.Start();
                        process.WaitForExit();
                    }
                }

                File.Create(checkFile);

                FileAttributes attributes = File.GetAttributes(checkFile);
                if (attributes != FileAttributes.Hidden || attributes != FileAttributes.System)
                {
                    Trace.WriteLine(DateTime.Now + "   |     Hiding file: " + checkFile);
                    File.SetAttributes(checkFile, File.GetAttributes(checkFile) | FileAttributes.Hidden);
                    File.SetAttributes(checkFile, File.GetAttributes(checkFile) | FileAttributes.System);
                }

                if (today.DayOfWeek == DayOfWeek.Tuesday && today.Day > 7 && File.Exists(checkFile))
                {
                    File.Delete(checkFile);
                }
            }
        }
    }
}
