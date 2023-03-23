using Assets.Scrips_new.DesignPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrips_new.Player
{
    internal class StateFree : State
    {
        public override string ToString()
        {
            return "StateFree : " + this.name;
        }
    }
}
