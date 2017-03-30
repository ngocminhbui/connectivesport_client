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
using Wikitude.Common.Util;
using Android.Content.PM;
using Wikitude.Common.Camera;
using Wikitude.Tools.Device.Features;
using Android.Util;

namespace connectivesport
{
    [Activity(Label = "TestAR_Activity", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.KeyboardHidden)]
    public class TestAR_Activity : Activity, ArchitectView.IArchitectUrlListener
    {
        protected ArchitectView architectView;

        private const string SAMPLE_WORLD_URL = "samples/POI2/index.html";

        public const string WIKITUDE_SDK_KEY = "qmh1Uynkxp1bhnqymMkItsVjqpl8PK5U/xBsAjFCaJxBExCEfiNcTxgLQrK4NWZq5nA1rP6n2AC1taCsDYfsEssjmMbdGG8xlOXqlZejP7p2+boG5/L3w8AsmOXst+DtJ3s+EgANJQFSyj1CuOuQ0ztoMEKiARQX+2/NwhEU/tVTYWx0ZWRfX+yokGspFYRWejA0HNDE/gS0GfTSI49EuNOIS/0mYtBHlulGZec3fzfTHJR8A+SpNlIAE1TF4hzM6fyosLcZs5zNHxTugyF8YTfb+MRJEXLMWoPnBEfrK01FpQTAus/bkCa4/3u8wkPSXWGOH5C9ejXveU1YQ22IGB/Ivdxd6hvZErqShjW5ZfWMy0P6BAqRLrggaktNgI36NjYRmcHCd+MsM53b9aBWwI6oZ5cU1LDWwAOMngSpAojKqael/6mchcTP0vwMRpJi0YfjIpHF4iAs7oWRh2qbujfgDjranVoqxNAvr2zi1aQwkOPVOPUYATp2HSOBFQ8CDZrCUuHE7zJjm6sA8B3VtB65336jV3jvix+uqMnWuI8D04qVmfA0oq/J7nfXytJadjX7zeC+ewJnni2b635rfndzy09lgSD5lmn44hJ2K2WxGMQPPl1ArPQ3Ngt52cUNXLdEw6Wfa6xNXZyoDwkoymrK6xQHhNmvDvB24cFP+Wc=";
        public const string WIKITUDE_SDK_KEY2 = "NGBXvYi66YY4pT3CqeLvcv31N9xl4uasQSZwF5xPfJ5lcrI5leTkRrzzVihYTbEbRWRf9S9hWqkeCykxd1IgU/qbg5WJjwSK7dk9f/zHwlV1Qa/JYIB6l+sh2OjrdrXO7E9Qdqih1RYGF+3MDt7CC3BmMUrhkFanOvCf/eXMmX1TYWx0ZWRfX6PikQ4qQBjn7mRR7l4e36y3jrIqcuQfE4vdeKCDiD2pePwQ41U/FnA7HSShjqq9TcTpQaASuWQL+nnrKUU3ybpck+50zKokc0nK6tX0rjqAE3cKZJIXMV1VRszX2rUJFFzM80eMWNQ2FN6I3e0LlyY3gkAt05XUiTq4YaOVb62gRlytIPNvaxwFoj3Xvh5+vR4afdbKAgdAlxT4KLazRObTUBuYHWeKU9/cXR4RagzSDUt+mpYzEVpZTB8OjGFWKf+j+5kCRrQ/ra4gYIuf3KqYFy0JsuAeN2keaI5M34saqcTNSUV7Ng1V/ZjJg9Ac56TLC+D1FuMDdpZ6c3eWTsaccwc4tMmnyiA8Y60GqXIeFOClE1locWR0Fu/MXmOkoFSXGy/ldfzVOo756Mhb4xCSTvbN+PUKbyM9EYWrmj3Yu88wxglud9L+G8etmi+Mr1wO4SGCfIQdzu1Pt9go8QhZpIB7Nyk1IirWW99b10Kzh8rW1fj8ReVBddHb4SU5+r2/CmAUMrbohodJFBefpbagBhQ7EV8sg/1ylBYaNVXUi3bfCt437rcwNniWV6/Pm4thryejMMflAji9gp+TgioY4r1ex6LDzzzRrHGZ+Qypwrm41oxWpz5Gw4hbkeakbqhVT7AgTq2bvQ++Gkksaw5RO54rIzuh2QOu1Ad+XY81VHLYMnudcLzWuY3WcrQEqgQ/jRIGOH4ZywKTOqtXozF4Us85z5CWHfdESc2foe68ZbOem/pn7a9Hk+foWy9oFp8/0lEKjoVCZp264eeU7EsdQoHNnPDq8w3UZSfVGd3sOytAIQAkeFER/5GNQBXM+gsoCU3cd5Wrfvj9LHrC223CqdDENpyVc2uHc+RfyOAhXMZxtw0+Hi2etkJXkC+6y4ncxwKt8wlThhBtpDfgE0wB6wZ0sHLE+fmK7LOSUipj8qc7s6sD01la+xHL7yrcpWDJIJ46cgiycaVb7AWSW5c8wXpKK+i/JgI8IDVDPqMFZVfhFhfW8jg=";

        public bool UrlWasInvoked(string p0)
        {
            return true;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.test_ar_layout);

            architectView = FindViewById<ArchitectView>(Resource.Id.architectView);
            ArchitectStartupConfiguration startupConfiguration = new ArchitectStartupConfiguration();
            startupConfiguration.setLicenseKey(WIKITUDE_SDK_KEY);
            startupConfiguration.setFeatures(ArchitectStartupConfiguration.Features.ImageTracking);
            startupConfiguration.setCameraResolution(CameraSettings.CameraResolution.Auto);

            int requiredFeatures = ArchitectStartupConfiguration.Features.Geo | ArchitectStartupConfiguration.Features.ImageTracking;

            MissingDeviceFeatures missingDeviceFeatures = ArchitectView.isDeviceSupported(this, requiredFeatures);
 
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
                }
                catch (Exception ex)
                {
                    Log.Error("WIKITUDE_SAMPLE", ex.ToString());
                }
            }
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
    }
}