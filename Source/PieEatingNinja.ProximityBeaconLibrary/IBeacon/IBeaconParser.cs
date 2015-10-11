using PieEatingNinja.ProximityBeaconLibrary.Extensions;

namespace PieEatingNinja.ProximityBeaconLibrary.IBeacon
{
    internal sealed class IBeaconParser : ProximityBeaconParser
    {
        const int IBEACON_TYPE_ZERO_INDEX = 2;
        const int IBEACON_TYPE_ZERO_PROXIMITY_VALUE = 0x02;
        const int IBEACON_TYPE_ONE_INDEX = 3;
        const int IBEACON_TYPE_ONE_PROXIMITY_VALUE = 0x15;

        const int IBEACON_COMPANYID_INDEX = 0;
        const int IBEACON_COMPANYID_LENGTH = 1;
        const int IBEACON_UUID_INDEX = 4;
        const int IBEACON_UUID_LENGTH = 20;
        const int IBEACON_MAJOR_INDEX = 20;
        const int IBEACON_MAJOR_LENGTH = 2;
        const int IBEACON_MINOR_INDEX = 22;
        const int IBEACON_MINOR_LENGTH = 2;
        const int IBEACON_MEASUREDPOWER_INDEX = 24;
        const int IBEACON_MEASUREDPOWER_LENGTH = 1;

        internal override ProximityBeaconType Type
        {
            get
            {
                return ProximityBeaconType.IBeacon;
            }
        }

        private static ushort GetMajor(byte[] advertisementData)
        {
            return advertisementData.ReadAsUShort(IBEACON_MAJOR_INDEX, IBEACON_MAJOR_LENGTH);
        }

        private static ushort GetMinor(byte[] advertisementData)
        {
            return advertisementData.ReadAsUShort(IBEACON_MINOR_INDEX, IBEACON_MINOR_LENGTH);
        }

        private static ushort GetCompanyId(byte[] advertisementData)
        {
            return advertisementData.ReadAsUShort(IBEACON_COMPANYID_INDEX, IBEACON_COMPANYID_LENGTH);
        }

        private static string GetUUID(byte[] advertisementData)
        {
            return advertisementData.ReadAsString(IBEACON_UUID_INDEX, IBEACON_UUID_LENGTH);
        }

        private static short GetMeasuredPower(byte[] advertisementData)
        {
            return advertisementData.ReadAsShort(IBEACON_MEASUREDPOWER_INDEX, IBEACON_MEASUREDPOWER_LENGTH);
        }

        internal protected override bool CanParseAdvertisement(byte[] databytes)
        {
            return databytes[IBEACON_TYPE_ZERO_INDEX] == IBEACON_TYPE_ZERO_PROXIMITY_VALUE &&
                   databytes[IBEACON_TYPE_ONE_INDEX] == IBEACON_TYPE_ONE_PROXIMITY_VALUE;
        }

        internal protected override ProximityBeacon Parse(byte[] databytes)
        {
            string uuid;
            ushort minor, major, companyId;
            short measuredPower;

            major = GetMajor(databytes);
            minor = GetMinor(databytes);
            uuid = GetUUID(databytes);
            companyId = GetCompanyId(databytes);
            measuredPower = (short)(GetMeasuredPower(databytes) - 256); //we need the 2's-complement

            return new IBeacon(uuid, major, minor, companyId, measuredPower);
        }
    }
}
