namespace Assets.scrips.Player.Data
{
    using System;
    [Serializable]
    internal class PoweringData
    {
        public float MaxPower;
        public float DataTimeAddingPower;

        public float NormalizingPower(float power)
        {
            return power / MaxPower;
        }
    }
}
