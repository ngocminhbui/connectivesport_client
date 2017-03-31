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
using System.Diagnostics;
using System.Timers;

namespace connectivesport
{
    [Activity(Label = "Practice")]
    public class Practice : Activity
    {
        public static bool useBand = false;
        int sec = 0, min = 0, hour = 0;
        Timer time;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.practice);
            
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Connect to a Microsoft Band");
            alert.SetPositiveButton("Connect", (senderAlert, args) => {
                StartActivityForResult(typeof(BandConnect_Activity), 0);
                
            });
            alert.SetNegativeButton("Cancel", (senderAlert, args) => {
            });
            
            Dialog dialog = alert.Create();
            dialog.Show();
            
            time = new Timer();
            time.Interval = 1000;
            time.Elapsed += new ElapsedEventHandler(t_Elapsed);
            

            Button stop = (Button)FindViewById(Resource.Id.stop);
            Button start = (Button)FindViewById(Resource.Id.start);
            start.Click += (sender, e) =>
            {
                time.Start();
            };
            stop.Click += (sender, e) =>
            {
                time.Stop();
            };
            
        }

        protected void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            sec++;
            if (sec >= 60)
            {
                min++;
                sec -= 60;
            }
            if (min >= 60)
            {
                hour++;
                min -= 60;
            }
            RunOnUiThread(() =>
            {
                TextView duration = (TextView)FindViewById(Resource.Id.duration);
                duration.Text = $"{hour}:{min}:{sec}";
            });
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
                CaloriesSensor sensors = BandConnector.instance.BandClient.SensorManager.CreateCaloriesSensor();
                sensors.ReadingChanged += Sensors_ReadingChanged;
                
                //if (BandConnector.BandClient.SensorManager.Co != UserConsent.Granted)
                //await BandConnector.BandClient.SensorManager.RequestHeartRateConsentTaskAsync(this);
                sensors.StartReadings();
            }
        }
    }
}