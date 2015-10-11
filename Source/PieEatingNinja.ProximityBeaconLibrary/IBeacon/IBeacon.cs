namespace PieEatingNinja.ProximityBeaconLibrary.IBeacon
{
    /// <summary>
    /// Representation of a Proximity Beacon according to the iBeacon protocol standardized by Apple
    /// </summary>
    public sealed class IBeacon : ProximityBeacon
    {
        public ushort Major { get; private set; }
        public ushort Minor { get; private set; }
        public ushort CompanyId { get; private set; }
        public short MeasuredPower { get; private set; }
        public string UUID { get; private set; }

        public override ProximityBeaconType BeaconType
        {
            get
            {
                return ProximityBeaconType.IBeacon;
            }
        }

        public IBeacon(string uuid, ushort major, ushort minor, ushort companyId, short measuredPower)
        {
            UUID = uuid;
            Major = major;
            Minor = minor;
            CompanyId = companyId;
            MeasuredPower = measuredPower;
        }
    }
}
