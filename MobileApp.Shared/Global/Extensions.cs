using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace MobileApp.Shared.Global
{
    static class Extensions
    {
        public static void RedirectTo(this Application app, Page newPage, bool isNavigated = true)
        {
            Device.BeginInvokeOnMainThread(
                () => { app.MainPage = isNavigated ? new NavigationPage(newPage) : newPage; });
        }


        public static Stream GetEmbeddedResource(this Application app, string path)
        {
#if __IOS__
            var resourcePrefix = "MobileApp.Shared.iOS.";
#endif
#if __ANDROID__
            var resourcePrefix = "MobileApp.Shared.Droid.";
#endif
#if WINDOWS_PHONE
            var resourcePrefix = "MobileApp.Shared.WinPhone.";
#endif

            var assembly = app.GetType().GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream($@"MobileApp.Shared.{path.Trim()}");
        }

        public static byte[] ToArray(this Stream stream)
        {
            var res = new byte[stream.Length];
            stream.Read(res, 0, res.Length);
            return res;
        }
    }
}
