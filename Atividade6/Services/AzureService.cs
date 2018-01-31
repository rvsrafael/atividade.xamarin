using System;
using System.Threading.Tasks;
using Atividade6.Authentication;
using Atividade6.Helpers;
using Atividade6.Services;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(AzureService))]
namespace Atividade6.Services
{
    public class AzureService
    {

        static readonly string AppUrl = "https://fiapworkshopdobemrvs.azurewebsites.net";

        public MobileServiceClient Client { get; set; } = null;

        public static bool UseAuth { get; set; } = false;

        public void Initialize()
        {
            Client = new MobileServiceClient(AppUrl);

            if(!string.IsNullOrWhiteSpace(Settings.AuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId)   )
            {
                Client.CurrentUser = new MobileServiceUser(Settings.UserId)
                {
                    MobileServiceAuthenticationToken = Settings.AuthToken
                };
                
            } 

        }

        public async Task<bool> LoginAsync()
        {
            Initialize();

            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.LoginAsync(Client, MobileServiceAuthenticationProvider.Facebook);

            if(user == null)
            {
                Settings.AuthToken = string.Empty;
                Settings.UserId = string.Empty;

                Device.BeginInvokeOnMainThread(async () => 
                {
                    await App.Current.MainPage.DisplayAlert("Ops!","Não conseguimos efetuar o seu login, tente novamente", "OK");
                });

                return false;

            } else 
            {
                Settings.AuthToken = user.MobileServiceAuthenticationToken;
                Settings.UserId = user.UserId;

                return true;
            }


        }

    }
}
