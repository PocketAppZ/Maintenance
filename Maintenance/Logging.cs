using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Maintenance
{
    public class Logging
    {
        static string LogDirectory = Environment.CurrentDirectory;
        static string LogFile = LogDirectory + @"\Application.log";
        static string LogBak = LogDirectory + @"\Application.log.bak";

        public static void Log()
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
        }
    }
}
