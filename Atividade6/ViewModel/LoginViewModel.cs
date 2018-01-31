using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Atividade6.Helpers;
using Atividade6.Services;
using Atividade6.View;
using Xamarin.Forms;

namespace Atividade6.ViewModel
{
    public class LoginViewModel 
    {
        AzureService azureService;
        INavigation navigation;

        ICommand loginCommand;

        public ICommand LoginCommand =>
        loginCommand ?? (loginCommand = new Command(async () => await ExecuteLoginCommandAsync()));



        public LoginViewModel (INavigation nav)
        {
            azureService = DependencyService.Get<AzureService>();
            navigation = nav;

        }

        private async Task ExecuteLoginCommandAsync()
        {
            if(!(await LoginAsync()))
            {
                return;
            } 
            else 
            {
                var mainPage = new Atividade6Page();
                await navigation.PushAsync(mainPage);

                RemovePageFromStack();
            }
        }

        private void RemovePageFromStack()
        {
            var existinPages = navigation.NavigationStack.ToList();
            foreach(var page in existinPages)
            {
                if (page.GetType() == typeof(LoginPage))
                    navigation.RemovePage(page);
            }
        }

        public Task<bool> LoginAsync()
        {
            if(Settings.IsLoggedIn)
            {
                return Task.FromResult(true);
            }

            return azureService.LoginAsync();
        }
    
    }
}
