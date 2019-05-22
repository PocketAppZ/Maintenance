using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Maintenance
{
    public class DeleteFiles
    {
        public static void DeleteSetFiles()
        {
            // Delete Files
            if (ConfigurationManager.GetSection("FilesToDelete") as NameValueCollection != null)
            {
                foreach (var file in ConfigurationManager.GetSection("FilesToDelete") as NameValueCollection)
                {
                    string fileTodelete = (ConfigurationManager.GetSection("FilesToDelete") as NameValueCollection).GetValues(file.ToString()).FirstOrDefault();
                    if (fileTodelete != "")
                    {
                        if (File.Exists(fileTodelete))
                        {
                            Trace.WriteLine(DateTime.Now + "   |     Deleting file: " + fileTodelete);
                            File.Delete(fileTodelete);
                        }
                    }
                }
            }
        }
    }
}
