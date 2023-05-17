using System;
namespace DiningCombat.Player.Data
{
    [Serializable]
    public class PoweringData
    {
        public float MaxPower = 3000;
        public float DataTimeAddingPower = 1400;

        public float NormalizingPower(float power)
        {
            return power / MaxPower;
        }
    }
}
