using Logger;
using System;
using System.Text;

namespace Maintenance
{
    static class Program
    {
        public static string PuranDefragArgs = string.Empty;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Logging.Log();

            Logging.Info("*******************************  Schedule Disck Check  *******************************" + Environment.NewLine, "FullCheckup");
            DiskCheck.ScheduleCheck();

            Logging.Info("*******************************  Disable Scheduled Tasks  *******************************" + Environment.NewLine, "FullCheckup");
            DisableTasks.SetTasks();

            Logging.Info("*******************************  Unused Services To Manual  *******************************" + Environment.NewLine, "FullCheckup");
            DisableServices.SetServices();

            Logging.Info("*******************************  Set Files to Hidden  *******************************" + Environment.NewLine, "FullCheckup");
            HideFiles.SetAsHidden();

            Logging.Info("*******************************  Delete Files In Directory  *******************************" + Environment.NewLine, "FullCheckup");
            DeleteInDirectory.DeleteSetFiles();

            Logging.Info("*******************************  DeleteInDirectoryOlder.DeleteSetFiles  *******************************" + Environment.NewLine, "FullCheckup");
            DeleteInDirectoryOlder.DeleteSetFiles();

            Logging.Info("*******************************  DeleteFiles.DeleteSetFiles  *******************************" + Environment.NewLine, "FullCheckup");
            DeleteFiles.DeleteSetFiles();

            Logging.Info("*******************************  Full Checkup *******************************" + Environment.NewLine, "FullCheckup");
            if (args.Length > 0)
            {
                try
                {
                    string A0 = args[0].ToLower();

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
                        FullCheckup.StartCheckup(PuranDefragArgs);
                    }
                    if (A0 == "/help" || A0 == "/?" || A0 == "-help" || A0 == "-?")
                    {
                        Console.WriteLine("/FULLCHECKUP as a scheduled task when you are not using the computer for a long while.");

                        Logging.Info("/FULLCHECKUP as a scheduled task when you are not using the computer for a long while." + Environment.NewLine, "/HELP");
                    }
                }
                catch (Exception ex)
                {
                    Logging.Info(ex.Message, "Program");
                    throw;
                }
            }

            Environment.Exit(0);
        }
    }
}
