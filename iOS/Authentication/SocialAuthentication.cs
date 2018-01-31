using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atividade6.Authentication;
using Atividade6.Helpers;
using Atividade6.iOS.Authentication;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthentication))]
namespace Atividade6.iOS.Authentication
{
    public class SocialAuthentication: IAuthentication
    {
        async Task<MobileServiceUser> IAuthentication.LoginAsync(MobileServiceClient client,
                                                          MobileServiceAuthenticationProvider provider,
                                                          IDictionary<string, string> parameters = null)
        {
            try
            {
                var current = GetController();
                var user = await client.LoginAsync(Forms.Context, provider);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId;

                return user;


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private UIKit.UINavigationController GetController()
        {

            var window = UIKit.UIApplication.SharedApplication.KeyWindow;
            var root = window.RootViewController;

            if (root == null) return null;

            var current = root;

            while (current.PresentedViewController != null)
            {
                current = current.PresentedViewController;
            }

            return current;

        }



    }
}
