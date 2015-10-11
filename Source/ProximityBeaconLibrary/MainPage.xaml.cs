using PieEatingNinja.ProximityBeaconLibrary;
using PieEatingNinja.ProximityBeaconLibrary.IBeacon;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProximityBeaconLibrary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        BluetoothLEAdvertisementWatcher watcher;
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        public Task BeaconHelper { get; private set; }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            watcher = new BluetoothLEAdvertisementWatcher();

            var manufacturerData = new BluetoothLEManufacturerData();
            //  manufacturerData.CompanyId = 0x004C; //0x004C == Apple 
            //  watcher.AdvertisementFilter.Advertisement.ManufacturerData.Add(manufacturerData);
            watcher.SignalStrengthFilter.InRangeThresholdInDBm = -70;
            watcher.SignalStrengthFilter.OutOfRangeThresholdInDBm = -75;
            watcher.SignalStrengthFilter.OutOfRangeTimeout = TimeSpan.FromMilliseconds(1000);

            watcher.Received += Watcher_Received;

            watcher.Start();
        }

        private async void Watcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            ProximityBeacon beacon = null;
            if (ProximityBeaconReader.TryReadProximityBeacon(args, out beacon, ProximityBeaconType.IBeacon))
            {
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    if (beacon.BeaconType == ProximityBeaconType.IBeacon)
                    {
                        var iBeacon = beacon as IBeacon;

                        Debug.WriteLine($"RSSI: {args.RawSignalStrengthInDBm}, distance: {ProximityBeaconHelper.CalculateDistance(iBeacon.MeasuredPower, args.RawSignalStrengthInDBm)}");

                        // Display these information on the list
                        ReceivedAdvertisementListBox.Items.Add(
                            $"Beacon: UUID={iBeacon.UUID}\n\tMajor={iBeacon.Major}\n\tMinor={iBeacon.Minor}\n\trssi={args.RawSignalStrengthInDBm}\n\ttimestamp={args.Timestamp.ToString("HH\\:mm\\:ss\\.fff")}\n\tAddress:{args.BluetoothAddress}");
                    }
                });
            }
        }
    }
}
