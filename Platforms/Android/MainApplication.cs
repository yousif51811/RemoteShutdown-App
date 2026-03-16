using Android.App;
using Android.Runtime;
using Microsoft.Maui;
using Microsoft.Extensions.Hosting;
using Microsoft.Maui.Hosting;

namespace RemoteShutdownApp
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
