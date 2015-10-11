using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Bluetooth.Advertisement;

namespace PieEatingNinja.ProximityBeaconLibrary
{
    internal abstract class ProximityBeaconParser
    {
        internal virtual bool CanParseAdvertisement(BluetoothLEAdvertisementReceivedEventArgs advertisementReceivedEventArgs)
        {
            var dataSections = advertisementReceivedEventArgs.Advertisement.DataSections;
            if (dataSections.Count() > 1)
            {
                var datasection = dataSections.ElementAt(1);
                var bytes = datasection.Data.ToArray();
                return CanParseAdvertisement(bytes);
            }
            return false;
        }

        internal protected abstract bool CanParseAdvertisement(byte[] databytes);

        internal abstract ProximityBeaconType Type { get; }

        internal virtual ProximityBeacon Parse(BluetoothLEAdvertisementReceivedEventArgs advertisementReceivedEventArgs)
        {
            var dataSections = advertisementReceivedEventArgs.Advertisement.DataSections;
            if (dataSections.Count() > 1)
            {
                var datasection = dataSections.ElementAt(1);
                var bytes = datasection.Data.ToArray();
                return Parse(bytes);
            }
            return null;
        }

        internal protected abstract ProximityBeacon Parse(byte[] databytes);
    }
}
