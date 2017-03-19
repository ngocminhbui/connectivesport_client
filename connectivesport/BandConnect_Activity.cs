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
        BandConnector BandCon = new BandConnector();
        IBandInfo myBand;
        IBandInfo[] lsBand;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.connectBand);

            ListView Bands = (ListView)FindViewById(Resource.Id.listBand);
            lsBand = BandCon.FindBand();
            myBand = lsBand[0];

            for(int i = 0; i < lsBand.Length; i++)
            {
                TextView band = new TextView(this);
                band.Text = lsBand[i].Name;
                //Bands.AddView(band);
            }

            Button connect = (Button)FindViewById(Resource.Id.connectband);
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
            bool result = await BandCon.BandConnect(myBand);
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