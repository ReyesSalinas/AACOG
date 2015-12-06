using System.Collections.Generic;

namespace Library.Interfaces.JobTests.Fighter.Interfaces
{
    public interface IBonusSet
    {
        List<IBonus> Bonuses { get; set; }
    }
}