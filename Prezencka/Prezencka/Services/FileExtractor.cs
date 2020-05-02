using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Prezencka.Services
{
    public static class FileExtractor
    {
        public static Task Extract(string name, string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return ExtractFromAssembly(assembly, name, path);
        }

        public static async Task ExtractFromAssembly(Assembly assembly, string name, string path)
        {
            if (assembly is null)
                throw new ArgumentNullException(nameof(assembly));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            var writeStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (writeStatus != PermissionStatus.Granted && !await RequestWritePermission())
            {
                // TODO: display fail alert
                return;
            }

            using (var stream = assembly.GetManifestResourceStream(name))
                using (var fileStream = File.OpenWrite(path))
                    await stream.CopyToAsync(fileStream);
        }

        private static async Task<bool> RequestWritePermission()
        {
            var result = await Permissions.RequestAsync<Permissions.StorageWrite>();
            return result == PermissionStatus.Granted;
        }
    }
}
