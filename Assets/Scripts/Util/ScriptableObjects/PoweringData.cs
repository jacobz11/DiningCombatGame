namespace Assets.scrips.Player.Data
{
    using System;
    [Serializable]
    internal class PoweringData
    {
        public float MaxPower = 3000;
        public float DataTimeAddingPower = 1400;

        public float NormalizingPower(float power)
        {
            return power / MaxPower;
        }
    }
}
