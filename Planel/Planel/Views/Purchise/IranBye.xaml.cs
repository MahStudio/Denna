using Planel.ZarinPal;
using System;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IranBye : Page
    {
        string merchantCode = "3820a6ee-e2ab-11e6-8aa9-000c295eb8fc";
        string authority = string.Empty;
        int amount = 2000;
        string description = "Denna licence IR";
        string email = string.Empty;
        string mobile = string.Empty;
        string callBackUrl = "http://mahholding.ir/paymentcomplete.html";
        public IranBye()
        {
            InitializeComponent();
        }

        async void goit_Click(object sender, RoutedEventArgs e)
        {
            if (mob.Text != "" && eemail.Text != "")
            {
                buyer();
            }
            else
            {
                var messageDialog = new MessageDialog("لطفا شماره موبایل و ایمیلتان را وارد کنید");
                messageDialog.ShowAsync();
            }
        }

        async void buyer()
        {
            mobile = mob.Text;
            email = eemail.Text;

            if (amount < 100 || string.IsNullOrEmpty(merchantCode) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(callBackUrl))
                return;

            var zp = new PaymentGatewayImplementationServicePortTypeClient();

            var response = await zp.PaymentRequestAsync(merchantCode, amount, description, email, mobile, callBackUrl);

            authority = response.Body.Authority;

            if (response.Body.Status == 100)
            {
                webView.Visibility = Visibility.Visible;
                webView.Navigate(new Uri("https://www.zarinpal.com/pg/StartPay/" + authority));
            }
            else
                ShowMessage("There is a problem in creating authority code. please contact developer \r\nStatus code: " + response.Body.Status);
        }

        async void webView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            // آدرس فعلی ای که وب ویو در آن است
            var myUrl = sender.Source.ToString();

            if (myUrl.Contains(callBackUrl))
            {
                sender.Visibility = Visibility.Collapsed;

                var zp = new PaymentGatewayImplementationServicePortTypeClient();

                var response = await zp.PaymentVerificationAsync(merchantCode, authority, amount);

                if (response.Body.Status == 100)
                    ShowMessage("Purchase succeed.\r\n please restart the app due to licence activation \r\n RefId in case you need: " + response.Body.RefID);
                else
                    ShowMessage("There is a problem while purchasing... please contact developer \r\nStatus code: " + response.Body.Status);
                ApplicationData.Current.LocalSettings.Values["LicenceActive"] = true;
            }
        }

        async void ShowMessage(string content)
        {
            await new Windows.UI.Popups.MessageDialog(content).ShowAsync();
        }
    }
}