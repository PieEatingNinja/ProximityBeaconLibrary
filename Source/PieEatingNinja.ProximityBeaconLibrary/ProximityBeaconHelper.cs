using System;

namespace PieEatingNinja.ProximityBeaconLibrary
{
    public static class ProximityBeaconHelper
    {
        public static double CalculateDistance(double measuredPower, double rssi)
        {
            if (rssi == 0)
            {
                return -1.0;
            }

            double ratio = rssi * 1.0 / measuredPower;
            if (ratio < 1.0)
            {
                return Math.Pow(ratio, 10);
            }
            else
            {
                double accuracy = (0.89976) * Math.Pow(ratio, 7.7095) + 0.111;
                return accuracy;
            }
        }
    }
}
