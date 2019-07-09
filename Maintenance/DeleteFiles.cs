using Logger;
using System.IO;
using static Maintenance.Properties.Settings;

namespace Maintenance
{
    public class DeleteFiles
    {
        public static void DeleteSetFiles()
        {
            // Delete Files
            foreach (string file in Default.FilesToDelete)
            {
                if (File.Exists(file))
                {
                    Logging.Info("Deleting file: " + file, "DeleteFiles");
                    File.Delete(file);
                }
            }
        }
    }
}
