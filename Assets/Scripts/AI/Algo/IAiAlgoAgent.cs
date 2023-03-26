using System.Collections.Generic;
namespace Assets.Scrips_new.AI.Algo
{
    /// <typeparam name="TStructure"> mast be A generic data structure</typeparam>
    internal interface IAiAlgoAgent<TData, TReslut>
    {   
        void SetData(List<TData> data);

        List<TData> GetData();

        TReslut RunAlgo();
    }
}
