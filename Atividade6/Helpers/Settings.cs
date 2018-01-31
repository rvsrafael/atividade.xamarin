// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Atividade6.Helpers
{
	public static class Settings
	{
        private static ISettings AppSettings => CrossSettings.Current;

        const string UserIdKey = "userId";
        static readonly string UserIdDefault = string.Empty;

        const string AuthTokenKey = "authToken";
        static readonly string AuthTokenDefault = string.Empty;

        public static string AuthToken
        {
            get { return AppSettings.GetValueOrDefault(AuthTokenKey, AuthTokenDefault); }
            set { AppSettings.AddOrUpdateValue(AuthTokenKey, value); }
        }

        public static string UserId
        {
            get { return AppSettings.GetValueOrDefault(UserIdKey, UserIdDefault); }
            set { AppSettings.AddOrUpdateValue(UserIdKey, value); }
        }

        public static bool IsLoggedIn => !string.IsNullOrWhiteSpace(UserId);

	}
}