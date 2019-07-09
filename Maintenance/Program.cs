using Logger;
using System;
using System.Text;
using static Maintenance.Properties.Settings;

namespace Maintenance
{
    static class Program
    {
        public static string PuranDefragArgs = string.Empty;

        /// <summary>
        /// Cleanup and Maintenance
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (Default.UpgradeRequired)
            {
                Default.Upgrade();
                Default.UpgradeRequired = false;
                Default.Save();
                Default.Reload();
            }
            if (Default.FirstRun)
            {
                Default.FirstRun = false;
                Default.Save();
                Default.Reload();

                SettingsForm settings = new SettingsForm();
                settings.ShowDialog();

                Environment.Exit(0);
            }
            if (args.Length > 0)
            {
                try
                {
                    string A0 = args[0].ToLower();

                    if (args[0] == "/Logon" || args[0] == "-Logon")
                    {
                        StartLightCleanup();
                    }
                    else
                    {
                        StartLightCleanup();

                        // Get conditions for Full Checkup
                        if (args.Length > 1)
                        {
                            string A1 = args[1].ToLower();

                            if (A1 == "/puranfd" || A1 == "-puranfd")
                            {
                                StringBuilder sb = new StringBuilder();

                                foreach (string arg in args)
                                {
                                    if (arg != args[0] && arg != args[1])
                                    {
                                        if (sb.ToString() == string.Empty)
                                        {
                                            sb.Append(arg);
                                        }
                                        else
                                        {
                                            sb.Append(" " + arg);
                                        }
                                    }
                                }
                                PuranDefragArgs = sb.ToString();
                            }
                        }

                        if (A0 == "/fullcheckup" || A0 == "-fullcheckup")
                        {
                            Logging.Info("*********************  Full Checkup *********************" + Environment.NewLine, "FullCheckup");

                            FullCheckup.StartCheckup(PuranDefragArgs);
                        }
                        if (A0 == "/help" || A0 == "/?" || A0 == "-help" || A0 == "-?")
                        {
                            Console.WriteLine("/FULLCHECKUP as a scheduled task when you are not using the computer for a long while.");

                            Logging.Info("/FULLCHECKUP as a scheduled task when you are not using the computer for a long while." + Environment.NewLine, "/HELP");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logging.Info(ex.Message, "Program");
                    throw;
                }
            }
            else
            {
                // Run Settings and Close
                SettingsForm settings = new SettingsForm();
                settings.ShowDialog();
            }

            Environment.Exit(0);
        }

        private static void StartLightCleanup()
        {
            if (Default.LoggingEnabled)
            {
                Logging.Log();
            }

            if (Default.RunDiskCheckMonthly)
            {
                Logging.Info("*********************  Schedule Disck Check  *********************" + Environment.NewLine, "FullCheckup");
                DiskCheck.ScheduleCheck();
            }

            if (Default.TasksToDisable.Count > 0)
            {
                Logging.Info("*********************  Disable Scheduled Tasks  *********************" + Environment.NewLine, "FullCheckup");
                DisableTasks.SetTasks();
            }

            if (Default.ServicesToManual.Count > 0 || Default.ServicesToDisable.Count > 0)
            {
                Logging.Info("*********************  Unused Services To Manual  *********************" + Environment.NewLine, "FullCheckup");
                DisableServices.SetServices();
            }

            if (Default.FilesToHide.Count > 0)
            {
                Logging.Info("*********************  Set Files to Hidden  *********************" + Environment.NewLine, "FullCheckup");
                HideFiles.SetAsHidden();
            }

            if (Default.PathFilesToDelete.Count > 0)
            {
                Logging.Info("*********************  Delete Files In Directory  *********************" + Environment.NewLine, "FullCheckup");
                DeleteInDirectory.DeleteSetFiles();
            }

            if (Default.PathFilesToDeleteOlder.Count > 0)
            {
                Logging.Info("*********************  DeleteInDirectoryOlder.DeleteSetFiles  *********************" + Environment.NewLine, "FullCheckup");
                DeleteInDirectoryOlder.DeleteSetFiles();
            }

            if (Default.FilesToDelete.Count > 0)
            {
                Logging.Info("*********************  DeleteFiles.DeleteSetFiles  *********************" + Environment.NewLine, "FullCheckup");
                DeleteFiles.DeleteSetFiles();
            }
        }
    }
}
