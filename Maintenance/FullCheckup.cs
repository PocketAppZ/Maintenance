using Logger;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Maintenance
{
    public class FullCheckup
    {
        public static void StartCheckup(string PuranDefragArgs)
        {
            // Flush DNS
            Logging.Info("*******************************  Flush DNS  *******************************" + Environment.NewLine, "FullCheckup");
            RunCommand("cmd.exe", "/C ipconfig /flushdns");

            // DISM Restorehealth
            Logging.Info("*******************************  DISM Restorehealth  *******************************" + Environment.NewLine, "FullCheckup");
            RunCommand("cmd.exe", "/C DISM.exe /Online /Cleanup-image /Restorehealth");

            // DISM startcomponentcleanup
            Logging.Info("*******************************  DISM Component Cleanup  *******************************" + Environment.NewLine, "FullCheckup");
            RunCommand("cmd.exe", "/C DISM.exe /online /cleanup-image /startcomponentcleanup");

            // System File Checker
            Logging.Info("*******************************  System File Checker  *******************************" + Environment.NewLine, "FullCheckup");
            RunCommand("cmd.exe", "/C sfc /scannow");

            // Run Offline Defrag
            Logging.Info("*******************************  Offline Defrag  *******************************" + Environment.NewLine, "FullCheckup");
            if (PuranDefragArgs != string.Empty && File.Exists("PuranFD.exe"))
            {
                RunCommand("PuranFD.exe", PuranDefragArgs);
            }
        }

        private static void RunCommand(string filename, string args)
        {
            using (Process process = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    StandardOutputEncoding = Encoding.GetEncoding(437),
                    FileName = filename,
                    Arguments = args
                };

                process.StartInfo = startInfo;

                process.ErrorDataReceived += Proc_ErrorReceived;
                process.OutputDataReceived += Proc_DataReceived;

                process.Start();

                process.BeginErrorReadLine();
                process.BeginOutputReadLine();

                process.WaitForExit();
            }
        }

        private static void Proc_DataReceived(object sender, DataReceivedEventArgs e)
        {
            var Output = Regex.Replace(e.Data, "\x00", "");

            if (!Output.Contains("[=") && !Output.Contains("%"))
            {
                Logging.Info(Output, "Full Checkup - Data Recieved");
            }
        }

        private static void Proc_ErrorReceived(object sender, DataReceivedEventArgs e)
        {
            var Output = Regex.Replace(e.Data, "\x00", "");

            if (!Output.Contains("[=") && !Output.Contains("%"))
            {
                Logging.Info(Output, "Full Checkup - Error Recieved");
            }
        }
    }
}
