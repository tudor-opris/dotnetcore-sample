using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SeleniumCore.Helpers
{
    public static class EmbeddedResource
    {
        public static byte[] GetResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
                if (stream != null)
                {
                    var ba = new byte[stream.Length];
                    stream.Read(ba, 0, ba.Length);
                    return ba;
                }
            return null;
        }

        public static string GetResourceFilePath(string fileName)
        {
            var filesUsedInTestsDir = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) + "\\FilesUsedInTests";
            var resourceName = $"{Assembly.GetExecutingAssembly().GetName().Name}" +
                $".{new DirectoryInfo(filesUsedInTestsDir).Name}.{fileName}";

            var resource = GetResource(resourceName);
            if (resource == null)
                throw new NotFoundException(resourceName);

            var resourceFilePath = Path.Combine(filesUsedInTestsDir, fileName);

            return resourceFilePath;
        }

        public static string GetResourceFolderFilePath(string fileName)
        {
            var filesUsedInTestsDir = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) + "\\FilesUsedInTests";
            var resourceName = $"{Assembly.GetExecutingAssembly().GetName().Name}" +
                $".{new DirectoryInfo(filesUsedInTestsDir).Name}.{fileName}";

            var resource = GetResource(resourceName);
            if (resource == null)
                throw new NotFoundException(resourceName);


            return filesUsedInTestsDir;
        }


        public static string GetTestFileLocation(string fileName)
        {
            return GetResourceFilePath(fileName);
        }
    }
}
