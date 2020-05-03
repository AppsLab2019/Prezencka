using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Prezencka.Services
{
    public static class FileExtractor
    {
        public static Task<bool> Extract(string name, string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return ExtractFromAssembly(assembly, name, path);
        }

        public static async Task<bool> ExtractFromAssembly(Assembly assembly, string name, string path)
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
                return false;
            }

            using (var stream = assembly.GetManifestResourceStream(name))
                using (var fileStream = File.Open(path, FileMode.Create, FileAccess.Write))
                    await stream.CopyToAsync(fileStream);

            return true;
        }

        private static async Task<bool> RequestWritePermission()
        {
            var result = await Permissions.RequestAsync<Permissions.StorageWrite>();
            return result == PermissionStatus.Granted;
        }
    }
}
