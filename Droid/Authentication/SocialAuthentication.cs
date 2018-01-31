﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Atividade6.Authentication;
using Atividade6.Droid.Authentication;
using Atividade6.Helpers; 
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthentication)) ]
namespace Atividade6.Droid.Authentication
{
    public class SocialAuthentication : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsync(MobileServiceClient client, 
                                                  MobileServiceAuthenticationProvider provider, 
                                                  IDictionary<string, string> parameters = null)
        {
            try
            {
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
    }
}
