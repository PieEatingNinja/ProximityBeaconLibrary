using PieEatingNinja.ProximityBeaconLibrary.IBeacon;
using System;
using System.Diagnostics;
using System.Linq;
using Windows.Devices.Bluetooth.Advertisement;

namespace PieEatingNinja.ProximityBeaconLibrary
{
    public static class ProximityBeaconReader
    {
        private static ProximityBeaconParser[] Parsers = new ProximityBeaconParser[] { new IBeaconParser() };
        public static bool TryReadProximityBeacon(BluetoothLEAdvertisementReceivedEventArgs advertisementReceivedEventArgs,
            out ProximityBeacon beacon, params ProximityBeaconType[] supportedTypes)
        {
            bool result = false;
            beacon = null;
            foreach (var parser in Parsers.Where(a => supportedTypes.Contains(a.Type)))
            {
                if (parser.CanParseAdvertisement(advertisementReceivedEventArgs))
                {
                    try
                    {
                        beacon = parser.Parse(advertisementReceivedEventArgs);
                        result = true;
                        break;
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                else
                    continue;
            }
            return result;
        }
    }
}
