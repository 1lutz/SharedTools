using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace SharedTools
{
    public class IniDocument
    {
        private const string DefaultSection = "General";
        private const string Empty = "##~HaRaKiRi~##";
        private const int MaxValueLength = 255;
        private static readonly string ApplicationName = Assembly.GetEntryAssembly().GetName().Name;
        public string Path { get; }

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string section, string key, string standard, StringBuilder retVal, int size, string filePath);

        private IniDocument(string pfad)
        {
            Path = System.IO.Path.GetFullPath(pfad);
        }

        public bool TryGetValue(string section, string key, out string value)
        {
            StringBuilder builder = new StringBuilder(MaxValueLength);
            GetPrivateProfileString(section, key, Empty, builder, MaxValueLength, Path);
            value = builder.ToString();

            if (value == Empty)
            {
                value = null;
                return false;
            }
            return true;
        }

        public bool TryGetValue(string key, out string value)
        {
            return TryGetValue(DefaultSection, key, out value);
        }

        public string this[string section, string key]
        {
            get
            {
                string fertig;
                if (!TryGetValue(section, key, out fertig)) throw new KeyNotFoundException();
                return fertig;
            }
            set
            {
                WritePrivateProfileString(section, key, value, Path);
            }
        }

        public string this[string key]
        {
            get { return this[DefaultSection, key]; }
            set { this[DefaultSection, key] = value; }
        }

        public static IniDocument Open(string pfad) => new IniDocument(pfad);

        public static IniDocument Open() => Open(ApplicationName + ".ini");
    }
}
