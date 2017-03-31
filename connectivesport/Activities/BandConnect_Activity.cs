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
using Microsoft.Band;

namespace connectivesport
{
    [Activity(Label = "BandConnect_Activity")]
    public class BandConnect_Activity : Activity
    {
        IBandInfo myBand;
        IBandInfo[] lsBand;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.connectBand);

            Button connect = (Button)FindViewById(Resource.Id.connectband);
            ListView Bands = (ListView)FindViewById(Resource.Id.listBand);

            lsBand = BandConnector.instance.FindBand();
            if (lsBand.Length != 0)
                myBand = lsBand[0];
            else
            {
                Toast.MakeText(this, "No Band found", ToastLength.Long);
                connect.Clickable = false;
                connect.Visibility = ViewStates.Invisible;
            }

            for(int i = 0; i < lsBand.Length; i++)
            {
                TextView band = new TextView(this);
                band.Text = lsBand[i].Name;
                //Bands.AddView(band);
            }

            //Button connect = (Button)FindViewById(Resource.Id.connectband);
            connect.Click += Connect_Click;

            //Bands.ItemClick += Bands_ItemClick;
        }

        private void Bands_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            ListView Bands = (ListView)FindViewById(Resource.Id.listBand);
            myBand = lsBand[Bands.SelectedItemPosition];
            Button connect = (Button)FindViewById(Resource.Id.connectband);
            connect.Clickable = true;
        }

        private async void Connect_Click(object sender, EventArgs e)
        {
            bool result = await BandConnector.instance.BandConnect(myBand);
            Practice.useBand = result;
            if (result)
            {
                Toast.MakeText(this, "connect successful", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "fail to connect", ToastLength.Short).Show();
            }
            this.Finish();
        }
    }
}