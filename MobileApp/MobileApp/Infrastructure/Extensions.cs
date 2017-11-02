using System.IO;
using System.Reflection;
using Java.Lang;
using Xamarin.Forms;

namespace MobileApp.Infrastructure
{
    static class Extensions
    {
        public static void RedirectTo(this Application app, Page newPage, bool isNavigated = true)
        {
            app.MainPage = isNavigated ? new NavigationPage(newPage) : newPage;
        }

        public static Stream GetEmbeddedResource(this Application app, string path)
        {
#if __IOS__
            var resourcePrefix = "MobileApp.iOS.";
#endif
#if __ANDROID__
            var resourcePrefix = "MobileApp.Droid.";
#endif
#if WINDOWS_PHONE
            var resourcePrefix = "MobileApp.WinPhone.";
#endif

            var assembly = app.GetType().GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream($@"{resourcePrefix}{path.Trim()}");
        }

        public static byte[] ToArray(this Stream stream)
        {
            var res = new byte[stream.Length];
            stream.Read(res, 0, res.Length);
            return res;
        }
    }
}
