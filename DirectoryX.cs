using System.IO;

namespace SharedTools
{
    public static class DirectoryX
    {
        public static void Copy(string sourceDirName, string destDirName)
        {
            if (!Directory.Exists(destDirName)) Directory.CreateDirectory(destDirName);

            foreach (string datei in Directory.GetFiles(sourceDirName))
            {
                string zieldatei = Path.Combine(destDirName, Path.GetFileName(datei));
                File.Copy(datei, zieldatei);
            }
            foreach (string ordner in Directory.GetDirectories(sourceDirName))
            {
                string zielordner = Path.Combine(destDirName, Path.GetFileName(ordner));
                Copy(ordner, zielordner);
            }
        }

        public static void MoveVolumeSafe(string sourceDirName, string destDirName)
        {
            if (!Directory.Exists(sourceDirName)) return;

            if (Path.GetPathRoot(sourceDirName) == Path.GetPathRoot(destDirName))
            {
                Directory.Move(sourceDirName, destDirName);
            }
            else
            {
                Copy(sourceDirName, destDirName);
                Directory.Delete(sourceDirName, true);
            }
        }
    }
}
