using System;
using System.Diagnostics;

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
            Logging.Log();

            DiskCheck.ScheduleCheck();

            DisableTasks.SetTasks();

            DisableServices.SetServices();

            HideFiles.SetAsHidden();

            DeleteInDirectory.DeleteSetFiles();

            DeleteInDirectoryOlder.DeleteSetFiles();

            DeleteFiles.DeleteSetFiles();


            Trace.WriteLine(Environment.NewLine);

            Environment.Exit(0);
        }
    }
}
