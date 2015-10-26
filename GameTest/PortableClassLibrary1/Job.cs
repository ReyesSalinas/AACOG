using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableClassLibrary1
{
    public abstract class Job
    {
        protected int Name { get; set; }
        protected int baseHp { get; set; }
        protected int baseMana { get; set; }

        //since most of the data for skills come from a database
        //i find that we can reference them from int list rather than
        // keep all this data held here
        protected List<int> skillList = new List<int>();
        
    }
}
