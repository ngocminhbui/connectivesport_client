using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Band;
using Microsoft.Band.Sensors;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using connectivesport;

namespace connectivesport
{
    [Activity(Label = "Practice")]
    public class Practice : Activity
    {
        public static bool useBand = false;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Practice);
            
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Connect to a Microsoft Band");
            alert.SetPositiveButton("Connect", (senderAlert, args) => {
                StartActivityForResult(typeof(BandConnect_Activity), 0);
                
            });
            alert.SetNegativeButton("Cancel", (senderAlert, args) => {
            });
            
            Dialog dialog = alert.Create();
            dialog.Show();

        }

        private void Sensors_ReadingChanged(object sender, IBandSensorEventEventArgs<IBandCaloriesEvent> e)
        {
            RunOnUiThread(() =>
            {
                TextView calories = (TextView)FindViewById(Resource.Id.calories);
                calories.Text = e.SensorReading.CaloriesToday.ToString();
            });
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (useBand)
            {
                CaloriesSensor sensors = BandConnector.BandClient.SensorManager.CreateCaloriesSensor();
                sensors.ReadingChanged += Sensors_ReadingChanged;
                
                //if (BandConnector.BandClient.SensorManager.Co != UserConsent.Granted)
                //await BandConnector.BandClient.SensorManager.RequestHeartRateConsentTaskAsync(this);
                sensors.StartReadings();
            }
        }
    }
}