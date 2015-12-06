using Library.Interfaces.JobTests;
using Library.Interfaces.JobTests.Fighter;
using Library.Interfaces.JobTests.Fighter.Interfaces;

namespace Library.Interfaces
{
    public enum ProgressionType
    {
        Bad = 1, Good = 2, Great = 3, Epic = 4
    }
    public interface ICharacterJob
    {
        string JobName { get; }
        int JobLevel { get; set; }
        ProgressionType HitPointProgressionType { get; }
        ProgressionType SkillRankProgressionType { get; }
        ProgressionType BaseAttackProgressionType { get; }
        ProgressionType FortitudeSaveProgressionType { get; }
        ProgressionType ReflexSaveProgressionType { get;  }
        ProgressionType WillSaveProgressionType { get;  }
        BaseStats BaseStats { get; set; }
        IBonusSet JobBonuses { get; set; }
    }
}