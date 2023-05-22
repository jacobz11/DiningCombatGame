namespace DiningCombat.Util
{
    public interface IBuffer<T>
    {
        public T DefaultLimt { get; }
        void Clear();
        public bool IsBufferOver();
        public void AddToData(T i_Dalta);
        public void SetDataToInit();
        public void UpdeteData(T i_NewData);
    }
}