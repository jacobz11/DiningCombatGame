using System;

[Serializable]
internal struct Cuntter
{
    public short m_Max;
    public short m_Min;
    private short m_Value;


    public bool TryInc()
    {
        bool canInc = m_Value <= m_Max;
        if (canInc)
        {
            m_Value++;
        }
        return canInc;
    }

    public bool TryDec()
    {
        bool canDec = m_Value >= m_Min;
        if (canDec)
        {
            m_Value--;
        }
        return canDec;
    }

    internal bool CanInc()
    {
        return m_Value <= m_Max;
    }
}
