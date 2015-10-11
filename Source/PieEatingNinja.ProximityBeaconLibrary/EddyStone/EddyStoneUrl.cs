namespace PieEatingNinja.ProximityBeaconLibrary.EddyStone
{
    public sealed class EddyStoneUrl : ProximityBeacon
    {
        public override ProximityBeaconType BeaconType
        {
            get
            {
                return ProximityBeaconType.EddyStoneUrl;
            }
        }
    }
}
