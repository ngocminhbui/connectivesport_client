using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Wikitude.Architect;
using Android.Content.PM;
using Wikitude.Common.Camera;
using Wikitude.Tools.Device.Features;
using Android.Util;
using System.Timers;

namespace connectivesport
{
    [Activity(Label = "WikitudeActivity", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden)]
    public class ARActivity : Activity, ArchitectView.IArchitectUrlListener
    {
        protected ArchitectView architectView;

        private const string SAMPLE_WORLD_URL = "samples/08_BrowsingPois_2_AddingRadar/index.html";

        private const string TITLE = "Image on Target";

        Timer clock = new Timer();
        //private const string WIKITUDE_SDK_KEY = "qmh1Uynkxp1bhnqymMkItsVjqpl8PK5U/xBsAjFCaJxBExCEfiNcTxgLQrK4NWZq5nA1rP6n2AC1taCsDYfsEssjmMbdGG8xlOXqlZejP7p2+boG5/L3w8AsmOXst+DtJ3s+EgANJQFSyj1CuOuQ0ztoMEKiARQX+2/NwhEU/tVTYWx0ZWRfX+yokGspFYRWejA0HNDE/gS0GfTSI49EuNOIS/0mYtBHlulGZec3fzfTHJR8A+SpNlIAE1TF4hzM6fyosLcZs5zNHxTugyF8YTfb+MRJEXLMWoPnBEfrK01FpQTAus/bkCa4/3u8wkPSXWGOH5C9ejXveU1YQ22IGB/Ivdxd6hvZErqShjW5ZfWMy0P6BAqRLrggaktNgI36NjYRmcHCd+MsM53b9aBWwI6oZ5cU1LDWwAOMngSpAojKqael/6mchcTP0vwMRpJi0YfjIpHF4iAs7oWRh2qbujfgDjranVoqxNAvr2zi1aQwkOPVOPUYATp2HSOBFQ8CDZrCUuHE7zJjm6sA8B3VtB65336jV3jvix+uqMnWuI8D04qVmfA0oq/J7nfXytJadjX7zeC+ewJnni2b635rfndzy09lgSD5lmn44hJ2K2WxGMQPPl1ArPQ3Ngt52cUNXLdEw6Wfa6xNXZyoDwkoymrK6xQHhNmvDvB24cFP+Wc=";
        private const string WIKITUDE_SDK_KEY = "2BxchMxrW/jtB44uOQHN6nk0cGBtML7oFcwod+qkffm7+vfEixhDSbGYMX/5Juo6LigfVWeFxR7cfMlBU2BrphkNe37m4JMA6ojU4k/Kmd6+WQJIBx9ZvJGeDs6NQsVCvSqC3OGqQSiigNjoh8/fspuDVjstrbPtWOz0D8QhtTRTYWx0ZWRfXxzyFOgdfZIM800QV8bOSHn7F2merq8oIf3m7/r5b/2fg2VRee0gEJSxFisUJzdvBaBsLg3VEWZjCuMiCchmeU4rjLkUV1BIsgVyDbyZb9J7dY4vl/A14MNNzdPI9vP6YmI/NAdx/x2TrHt1fl1HsHNUnneBrNwsXE4dfOhZgzGoN6FA9lHEfGOtgRK6Zrq+lBz6efYqoGoUB/Q7fNKJmLHKmJ7OYQWvblL6otEE7m7ugojOka1VWNGH9VUqxZyXpXB8KWbQ2IElFTdUHGQCLYnxWiurH78A4/yKVW46oQIaWCwjYmec5w7IHnlxiadW0JucXb9M6c1UC1j3ebhJoAEDLTj77rN6z9q6Gp6dTjDWYNuIGd7tWIBUYU/JbiwNSRMP7cfW8mz/EirpjF92fKq0mHwwWNhf2CuIf8ZQXtDfDZLOxdJZmkIIXdjepwDfRxVnibBbAEF894OQroCnIWJCI8k0wamJLyKRR7s/ihITdsEbi52zl0e8FL8HXyQ7/JBk26h5Xtva7AXxK9b3QVEMpF5m1V6eWQ==";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.AR_layout);

            //Title = TITLE;

            architectView = FindViewById<ArchitectView>(Resource.Id.architectView);
            ArchitectStartupConfiguration startupConfiguration = new ArchitectStartupConfiguration();
            startupConfiguration.setLicenseKey(WIKITUDE_SDK_KEY);
            //startupConfiguration.setFeatures(ArchitectStartupConfiguration.Features.Tracking2D);
            startupConfiguration.setCameraResolution(CameraSettings.CameraResolution.Auto);

            /* use  
			   int requiredFeatures = StartupConfiguration.Features.Tracking2D | StartupConfiguration.Features.Geo;
			   if you need both 2d Tracking and Geo
			*/
            int requiredFeatures = ArchitectStartupConfiguration.Features.Geo;
            MissingDeviceFeatures missingDeviceFeatures = ArchitectView.isDeviceSupported(this, requiredFeatures);

            clock.Interval = 3000;
            if ((ArchitectView.getSupportedFeaturesForDevice(Android.App.Application.Context) & requiredFeatures) == requiredFeatures)
            {
                architectView.OnCreate(startupConfiguration);
                architectView.RegisterUrlListener(this);

            }
            else
            {
                architectView = null;
                Toast.MakeText(this, missingDeviceFeatures.getMissingFeatureMessage(), ToastLength.Long).Show();
            }

            clock.Elapsed += Clock_Elapsed;
        }

        private void Clock_Elapsed(object sender, ElapsedEventArgs e)
        {
            architectView.SetLocation(10.752, 106.6617, 1.0);
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (architectView != null)
                architectView.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (architectView != null)
                architectView.OnPause();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (architectView != null)
            {
                architectView.OnDestroy();
            }
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();

            if (architectView != null)
                architectView.OnLowMemory();
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);

            if (architectView != null)
            {
                architectView.OnPostCreate();

                try
                {
                    architectView.Load(SAMPLE_WORLD_URL);
                    architectView.SetLocation(10.752, 106.6617, 1.0);
                    clock.Start();
                }
                catch (Exception ex)
                {
                    Log.Error("WIKITUDE_SAMPLE", ex.ToString());
                }
            }
        }

        public bool UrlWasInvoked(string url)
        {
            /* This is a example implementation of the UrlWasInvoked method */
            /* The url is defined in JS where document.location = 'architectsdk://...'; is used. */
            //Console.WriteLine("architect view invoked url: " + url);
            return true;
        }
    }
}