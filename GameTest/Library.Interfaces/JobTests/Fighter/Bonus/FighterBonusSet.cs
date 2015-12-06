using System.Collections.Generic;
using Library.Interfaces.JobTests.Fighter.Interfaces;

namespace Library.Interfaces.JobTests.Fighter.Bonus
{
    public class FighterBonusSet : IBonusSet
    {
        public FighterBonusSet()
        {
            Bonuses = new List<IBonus> {new FighterAttackBonus(), new FighterReflexBonus(), new FighterWillBonus()};
        }

        public List<IBonus> Bonuses { get; set; }
    }
}