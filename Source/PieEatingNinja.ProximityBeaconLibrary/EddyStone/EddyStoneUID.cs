namespace PieEatingNinja.ProximityBeaconLibrary.EddyStone
{
    public sealed class EddyStoneUID : ProximityBeacon
    {
        public override ProximityBeaconType BeaconType
        {
            get
            {
                return ProximityBeaconType.EddyStoneUID;
            }
        }
    }
}
