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

		TextView txtcalories;

		TextView txtstep;

		TextView txtHeartRate;







        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.practice);
            
            //AlertDialog.Builder alert = new AlertDialog.Builder(this);
            //alert.SetTitle("Connect to a Microsoft Band");
            //alert.SetPositiveButton("Connect", (senderAlert, args) => {
            //    StartActivityForResult(typeof(BandConnect_Activity), 0);
                
            //});
            //alert.SetNegativeButton("Cancel", (senderAlert, args) => {
            //});
            
            //Dialog dialog = alert.Create();
            //dialog.Show();
            
            time = new Timer();
            time.Interval = 1000;
            time.Elapsed += new ElapsedEventHandler(t_Elapsed);
            
			txtcalories = FindViewById<TextView>(Resource.Id.textViewCalories);
			txtstep = FindViewById<TextView>(Resource.Id.textViewSteps);
			txtHeartRate = FindViewById<TextView>(Resource.Id.textViewBeat);

			ImageButton stop = (ImageButton)FindViewById(Resource.Id.stop);
            ImageButton start = (ImageButton)FindViewById(Resource.Id.start);
            start.Click += (sender, e) =>
            {
                time.Start();
            };
            stop.Click += (sender, e) =>
            {
                time.Stop();
            };

			if (BandConnector.instance.isConnected())
			{
				CaloriesSensor sensorCalories = BandConnector.instance.BandClient.SensorManager.CreateCaloriesSensor();
				sensorCalories.ReadingChanged += CaloriesSensors_ReadingChanged;

				HeartRateSensor sensorHeartRate = BandConnector.instance.BandClient.SensorManager.CreateHeartRateSensor();
				sensorHeartRate.ReadingChanged += HeartRateSensors_ReadingChanged;

				DistanceSensor sensorDistance = BandConnector.instance.BandClient.SensorManager.CreateDistanceSensor();
				sensorDistance.ReadingChanged += DistanceSensors_ReadingChanged;

				//if (BandConnector.instance.BandClient.SensorManager.CurrentHeartRateConsent != UserConsent.Granted)
				//	BandConnector.instance.BandClient.SensorManager.RequestHeartRateConsentTaskAsync(this).Wait();

				sensorCalories.StartReadings();
				sensorDistance.StartReadings();
				//sensorHeartRate.StartReadings();
			}
            
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



        protected override void OnResume()
        {
            base.OnResume();

        
        }

		void DistanceSensors_ReadingChanged(object sender, IBandSensorEventEventArgs<IBandDistanceEvent> e)
		{
			try
			{
				long curStep = e.SensorReading.DistanceToday;
				RunOnUiThread(() =>
				{
					txtstep.Text = (e.SensorReading.DistanceToday - curStep).ToString();
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);

			}
		}

		void HeartRateSensors_ReadingChanged(object sender, IBandSensorEventEventArgs<IBandHeartRateEvent> e)
		{
			
			try
			{
				RunOnUiThread(() =>
				{
					txtHeartRate.Text = e.SensorReading.HeartRate.ToString();
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);

			}
		}

		void CaloriesSensors_ReadingChanged(object sender, IBandSensorEventEventArgs<IBandCaloriesEvent> e)
		{
			try
			{
				long curCategories = e.SensorReading.CaloriesToday;

				RunOnUiThread(() =>
				{
					txtcalories.Text = (e.SensorReading.CaloriesToday - curCategories).ToString();
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);

			}
		}
}
}