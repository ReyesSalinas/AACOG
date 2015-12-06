using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Library.Interfaces.JobTests.Fighter;

namespace Library.Interfaces
{
    
    public interface ICharacter
    {
        string First { get; set; }
        string Last { get; set; }
        Stats Stats { get; set; }
        List<ICharacterJob> Job { get; set; }
        BaseStats BaseStats { get; set; }
        Race Race { get; set; }
        
    }

    public class Stats
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Wisdom { get; set; }
        public int Intelligence { get; set; }
        public int Charisma { get; set; }

        public int GetModifier(int stat)
        {
            int mod = (stat - 10)/2;
            return mod;
        }
    }
}
