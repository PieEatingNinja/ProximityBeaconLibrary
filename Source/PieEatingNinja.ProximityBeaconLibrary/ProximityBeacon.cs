namespace PieEatingNinja.ProximityBeaconLibrary
{
    /// <summary>
    /// Abstract class representing a proximity beacon
    /// </summary>
    public abstract class ProximityBeacon
    {
        public abstract ProximityBeaconType BeaconType { get; }
    }
}
