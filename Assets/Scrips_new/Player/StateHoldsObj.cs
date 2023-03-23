using Assets.Scrips_new.DesignPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrips_new.Player
{
    internal class StateHoldsObj : State
    {
        public override string ToString()
        {
            return "StateHoldsObj : " + this.name;
        }
    }
}
