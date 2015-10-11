using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Bluetooth.Advertisement;

namespace PieEatingNinja.ProximityBeaconLibrary
{
    /// <summary>
    /// Base Proximity Beacon parser
    /// </summary>
    internal abstract class ProximityBeaconParser
    {
        /// <summary>
        /// Checks if the given Advertisement can be parsed by this parser.
        /// </summary>
        /// <param name="advertisementReceivedEventArgs"></param>
        /// <returns>True if the advertisement can be parsed as a proximity beacon by this parser.</returns>
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

        /// <summary>
        /// Checks if the given data section of an advertisement can be parsed by this parser.
        /// </summary>
        /// <param name="databytes">The bytes of the section data</param>
        /// <returns>True if this section data can be parsed as a proximity beacon by this parser.</returns>
        internal protected abstract bool CanParseAdvertisement(byte[] databytes);

        /// <summary>
        /// Indicates to what Proximity Beacon type this parser parsers the advertisement.
        /// </summary>
        internal abstract ProximityBeaconType Type { get; }

        /// <summary>
        /// Parses the given advertisementReceivedEventArgs to a Proximity Beacon data type
        /// </summary>
        /// <param name="advertisementReceivedEventArgs">The Advertisement to parse.</param>
        /// <returns>The Advertisement as a ProximityBeacon</returns>
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

        /// <summary>
        /// Parses the bytes of the datasection of the Advertisement to a Proximity Beacon
        /// </summary>
        /// <param name="databytes">The bytes to parse.</param>
        /// <returns>The bytes as a ProximityBeacon</returns>
        internal protected abstract ProximityBeacon Parse(byte[] databytes);
    }
}
