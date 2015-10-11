using PieEatingNinja.ProximityBeaconLibrary.IBeacon;
using System;
using System.Diagnostics;
using System.Linq;
using Windows.Devices.Bluetooth.Advertisement;

namespace PieEatingNinja.ProximityBeaconLibrary
{
    public static class ProximityBeaconReader
    {
        //List holding all parsers that can be used to parse a particular type of proximity beacon
        private static ProximityBeaconParser[] AvailableParsers = new ProximityBeaconParser[] { new IBeaconParser() };

        /// <summary>
        /// Tries to read proximity beacon data from the given advertisementReceivedEventArgs.
        /// </summary>
        /// <param name="advertisementReceivedEventArgs">The bluetooth advertisement message from which to read a proximity beacon message from.</param>
        /// <param name="beacon">The resulting proximity beacon data that can be read from the advertisement.</param>
        /// <param name="requestedTypes">The type(s) of proximity beacons to look for.</param>
        /// <returns>True when proximity beacon data can be read from the advertisement.</returns>
        public static bool TryReadProximityBeacon(BluetoothLEAdvertisementReceivedEventArgs advertisementReceivedEventArgs,
            out ProximityBeacon beacon, params ProximityBeaconType[] requestedTypes)
        {
            if (requestedTypes.Count() == 1 && requestedTypes[0] == ProximityBeaconType.Unknown)
            {
                throw new ArgumentException($"Proximity Beacon Type {nameof(ProximityBeaconType.Unknown)} cannot be parsed!");
            }

            bool result = false;
            beacon = null;

            foreach (var parser in AvailableParsers.Where(a => requestedTypes.Contains(a.Type)))
            {
                if (parser.CanParseAdvertisement(advertisementReceivedEventArgs))
                {
                    try
                    {
                        beacon = parser.Parse(advertisementReceivedEventArgs);
                        result = true;
                        break;
                    }
                    catch (Exception ex)
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
