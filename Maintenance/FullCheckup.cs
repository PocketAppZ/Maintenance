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
            if (PuranDefragArgs != string.Empty && File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\PuranFD.exe"))
            {
                RunCommand("PuranFD.exe", PuranDefragArgs);
            }
        }

        private static void RunCommand(string filename, string args)
        {
            try
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
            catch (Exception ex)
            {
                Logging.Error("Filename: " + filename + " args: " + args + " : " + ex, "FullCheckup - Process");
            }
        }

        private static void Proc_DataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data != null)
                {
                    if (!e.Data.Contains("[=") && !e.Data.Contains("%") && e.Data != string.Empty)
                    {
                        string Output = Regex.Replace(e.Data, "\x00", "");
                        if (Output != string.Empty)
                        {
                            Logging.Info(Output, "Full Checkup - Data Recieved");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex, "FullCheckup - Data Recieved");
            }
        }

        private static void Proc_ErrorReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data != null)
                {
                    if (e.Data != string.Empty)
                    {
                        string Output = Regex.Replace(e.Data, "\x00", "");
                        Logging.Info(Output, "Full Checkup - Error Recieved");
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(ex, "FullCheckup - Error Recieved");
            }
        }
    }
}
