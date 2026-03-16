using Microsoft.Maui.Controls.Platform;
using Dropbox.Api;
using System.Net.Http;
using Dropbox.Api.TeamLog;
namespace RemoteShutdownApp
{
    public partial class MainPage : ContentPage
    {
        Timer _timer;
        private readonly HttpClient _httpClient = new HttpClient();
        private string _access = "Access-key";
        private string _url = "Url-To-File";
        private DropboxClient _dropboxClient;
        private bool toggling = false;
        private bool is1 = false;
        public MainPage()
        {
            InitializeComponent();
            _timer = new Timer(async _ => await GetValue(), null, 0, 1000);
            _dropboxClient = new DropboxClient(_access);
            _url = Preferences.Default.Get("url", _url);
            _access = Preferences.Default.Get("access", _access);
            MainThread.BeginInvokeOnMainThread(() =>
            {
                accesstxt.Text = _access;
                URLtxt.Text = _url;
            });
        }

        async Task GetValue()
        {
            try
            {
                string content = await _httpClient.GetStringAsync(_url);
                content = content.Trim();
                if (toggling) { return; }
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    
                    if (content == "1")
                    {
                        Statustxt.Text = "Enabled";
                        Statustxt.TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#00FF00");
                        MainToggle.IsToggled = true;
                        is1 = true;
                    }
                    else
                    {
                        Statustxt.Text = "Disabled";
                        Statustxt.TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#FF0000");
                        MainToggle.IsToggled = false;
                        is1 = false;
                    }
                });
            }
            catch { }
        }

        private async void MainToggle_Toggled(object sender, ToggledEventArgs e)
        {
            await Toggle();
        }
        async Task Toggle()
        {
            toggling = true;
            try
            {
                Console.WriteLine("Starting...");
                if (is1)
                {
                    Console.WriteLine("True");
                    await _dropboxClient.Files.UploadAsync("/sh.txt", Dropbox.Api.Files.WriteMode.Overwrite.Instance, body: new MemoryStream(System.Text.Encoding.UTF8.GetBytes("0")));
                }
                else
                {
                    Console.WriteLine("False");
                    await _dropboxClient.Files.UploadAsync("/sh.txt", Dropbox.Api.Files.WriteMode.Overwrite.Instance, body: new MemoryStream(System.Text.Encoding.UTF8.GetBytes("1")));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.ToString());
            }
            finally
            {
                toggling = false;
            }
            
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            _url = URLtxt.Text;
            _access = accesstxt.Text;
            Preferences.Default.Set("url", _url);
            Preferences.Default.Set("access", _access);
            await DisplayAlertAsync("Saved", "Settings have been saved successfully.", "OK");
            _dropboxClient.Dispose();
            _dropboxClient = new DropboxClient(_access);
        }
    }
}
