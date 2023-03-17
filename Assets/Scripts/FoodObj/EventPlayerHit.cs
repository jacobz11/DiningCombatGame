using System;

namespace DiningCombat
{
    internal class EventPlayerHit : EventArgs
    {
        private byte m_PonitToAdd;

        public byte PonitToAdd
        {
            get => m_PonitToAdd;
            private set => m_PonitToAdd = value;
        }

        public EventPlayerHit(params byte[] i_Point)
        {
            PonitToAdd = calculatePoints(i_Point);
        }

        private byte calculatePoints(byte[] i_Point)
        {
            byte point = 0;

            for (byte i = 0; i < i_Point.Length; i++)
            {
                point += i;
            }

            return point;
        }
    }
}
