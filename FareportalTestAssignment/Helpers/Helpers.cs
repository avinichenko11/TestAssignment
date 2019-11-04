using System;
using System.Configuration;
using System.IO;
using System.Reflection;


namespace FareportalTestAssignment.Helpers
{
    public static class Helpers
    {
        public static string GetPathToFileinAssemblyDirectory(string fileName)
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.Combine(Path.GetDirectoryName(path), $"Files\\{fileName}");
        }

        public static bool CompareByteArrays(byte[] first, byte[] second)
        {
            if (first.Length == second.Length)
            {
                int i = 0;
                while (i < first.Length && (first[i] == second[i]))
                {
                    i++;
                }
                if (i == first.Length)
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetUrl() => ConfigurationSettings.AppSettings["BaseUrl"];
    }
}
