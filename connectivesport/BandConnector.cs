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
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

[assembly: UsesPermission(Android.Manifest.Permission.Bluetooth)]
[assembly: UsesPermission(Microsoft.Band.BandClientManager.BindBandService)]

namespace connectivesport
{
    class BandConnector
    {
        public static IBandInfo MyBand;
        public static IBandClient BandClient;

        public async Task<bool> BandConnect(IBandInfo myBand)
        {
            MyBand = myBand;
            BandClient = BandClientManager.Instance.Create(Application.Context, MyBand);
            var result = await BandClient.ConnectTaskAsync();
            if (BandClient.IsConnected)
            {
                return true;
            }
            else
                return false;
        }

        public IBandInfo[] FindBand()
        {

            IBandInfo[] pairedBand = BandClientManager.Instance.GetPairedBands();
            return pairedBand;
        }

        public async Task<string> GetVersionAsync()
        {
            string result = "";
            try
            {
                result = await BandClient.GetFirmwareVersionTaskAsync();
            }
            catch (BandException e)
            {
                
            }
            
            return result;
        }
    }
}