
// Helpers/Settings.cs This file was automatically added when you installed the Settings Plugin. If you are not using a PCL then comment this file back in to use it.
using Android.Locations;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace connectivesport
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
  {
    private static ISettings AppSettings
    {
      get
      {
        return CrossSettings.Current;
      }
    }

    #region Setting Constants

    private const string SettingsKey = "settings_key";
    private static readonly string SettingsDefault = string.Empty;
		internal static string fbid;

		#endregion






		public static string GeneralSettings
	    {
	      get
	      {
	        return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
	      }
	      set
	      {
	        AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
	      }
	    }
		public static string HubRegistrationId { get; internal set; }
		public static string PNSRegistrationId { get; internal set; 
		internal static Location currentLocation;


		public static User User { get; internal set; }


	}
}