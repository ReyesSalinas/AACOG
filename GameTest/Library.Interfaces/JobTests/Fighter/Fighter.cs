using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Library.Interfaces.JobTests.Fighter.Bonus;
using Library.Interfaces.JobTests.Fighter.Interfaces;

namespace Library.Interfaces.JobTests.Fighter
{
    public class Fighter : ICharacterJob
    {
        public Fighter(FighterBonusSet bonuses)
        {
            JobBonuses = bonuses;
        
        }

        public FighterBonusSet FighterBonuses => JobBonuses as FighterBonusSet;


        public string JobName
        {
            get { return "Fighter"; }
        }

        public int JobLevel { get; set; }
        public List<int> HitPoints { get; set; }

        public ProgressionType HitPointProgressionType { get; } = ProgressionType.Great;
        public ProgressionType BaseAttackProgressionType { get; } = ProgressionType.Great; 
        public ProgressionType FortitudeSaveProgressionType { get; }=ProgressionType.Good;
        public ProgressionType ReflexSaveProgressionType { get; }= ProgressionType.Bad;
        public ProgressionType WillSaveProgressionType { get; }= ProgressionType.Bad;
        public ProgressionType SkillRankProgressionType { get;}= ProgressionType.Bad; 
        public JobBaseStats BaseStats { get; set; }
        public IBonusSet JobBonuses { get; set; }
    }

    public interface IBaseStats
    {
        int SkillsRanks { get; set; }
        int AttackBonus { get; set; }
        int FortitudesSaveBonus { get; set; }
        int ReflexSaveBonus { get; set; }
        int WillSavesBonus { get; set; }
    }

    public interface ICharcterStats:IBaseStats
    {
        int HitPoints { get; set; }
    }

    public interface IJobStats:IBaseStats
    {
        List<int> HitPoints { get; set; }
       
    }
    public class CharacterBaseStats:ICharcterStats
    {
        public int SkillsRanks { get; set; }
        public int AttackBonus { get; set; }
        public int FortitudesSaveBonus { get; set; }
        public int ReflexSaveBonus { get; set; }
        public int WillSavesBonus { get; set; }
        public int HitPoints { get; set; }
    }

    public class JobBaseStats : IJobStats
    {
        public int SkillsRanks { get; set; }
        public int AttackBonus { get; set; }
        public int FortitudesSaveBonus { get; set; }
        public int ReflexSaveBonus { get; set; }
        public int WillSavesBonus { get; set; }
        public List<int> HitPoints { get; set; }
    }

    public class JobLevelingService:ILevelingServce
    {
       

        private int SaveCalculator(int joblevel, ProgressionType type)
        {
            if (type == ProgressionType.Bad)
            {
                return Formulas.BadSave(joblevel);
            }

            return Formulas.GoodSave(joblevel);
        }

        private int AttackCalculator(int joblevel, ProgressionType type)
        {
            switch (type)
            {
                case ProgressionType.Bad:
                    return Formulas.BadAttack(joblevel);
                    break;
                case ProgressionType.Good:
                    return Formulas.GoodAttack(joblevel);
                    break;
                case ProgressionType.Great:
                    return Formulas.GreatAtttack(joblevel);
                    break;
                case ProgressionType.Epic:
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        public void CalculateSave(ICharacter character)
        {
            int totalBaseSave;

            foreach (var job in character.Jobs)
            {
                // after reaching epic levels( > 20) you gain one point to your saves per level


                job.BaseStats.FortitudesSaveBonus = SaveCalculator(job.JobLevel, job.FortitudeSaveProgressionType);
                job.BaseStats.ReflexSaveBonus = SaveCalculator(job.JobLevel, job.ReflexSaveProgressionType);
                job.BaseStats.WillSavesBonus = SaveCalculator(job.JobLevel, job.WillSaveProgressionType);
            }
        }

        public void CalculateJobAttackBonus(ICharacter character)
        {
            foreach (var job in character.Jobs)
            {
                job.BaseStats.AttackBonus = AttackCalculator(job.JobLevel, job.BaseAttackProgressionType);
            }
        }

        public void CalculateHitPoints(ICharacter character)
        {
            foreach (ICharacterJob job in character.Jobs)
            {


                switch (job.HitPointProgressionType)
                {
                    case ProgressionType.Bad:
                        job.BaseStats.HitPoints.Add(Dice.SixSided());
                        break;
                    case ProgressionType.Good:
                        job.BaseStats.HitPoints.Add(Dice.EightSided());
                        break;
                    case ProgressionType.Great:
                        job.BaseStats.HitPoints.Add(Dice.TenSided());
                        break;
                    case ProgressionType.Epic:
                        job.BaseStats.HitPoints.Add(Dice.TweleveSided());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void CalculateSkillRanks(ICharacter character)
        {
            foreach (ICharacterJob job in character.Jobs)
            {


                switch (job.SkillRankProgressionType)
                {
                    case ProgressionType.Bad:
                        job.BaseStats.SkillsRanks = Formulas.BadSkill(job.JobLevel);
                        break;
                    case ProgressionType.Good:
                        job.BaseStats.SkillsRanks = Formulas.GoodSkill(job.JobLevel);
                        break;
                    case ProgressionType.Great:
                        job.BaseStats.SkillsRanks = Formulas.GreatSkill(job.JobLevel);
                        break;
                    case ProgressionType.Epic:
                        job.BaseStats.SkillsRanks = Formulas.EpicSkill(job.JobLevel);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }


    public static class Dice
    {
        public static int Roll(int dieSize)
        {
            return new Random().Next(1, dieSize);
        }

        public static int FourSided()
        {
            return new Random().Next(1, 4);
        }

        public static int SixSided()
        {
            return new Random().Next(1, 6);
        }

        public static int EightSided()
        {
            return new Random().Next(1, 8);
        }

        public static int TenSided()
        {
            return new Random().Next(1, 10);
        }

        public static int TweleveSided()
        {
            return new Random().Next(1, 12);
        }

        public static int TwentySided()
        {
            return new Random().Next(1, 20);
        }

        public static int HundredSided()
        {
            return new Random().Next(1, 100);
        }
    }

    internal static class Formulas
    {

         
        public static int GetModifier(int stat)
        {
            int mod = (stat - 10)/2;
            return mod;
        }

        #region Skill Ranks

        public static int BadSkill(int jobLevel)
        {
            return 2 * jobLevel;
        }

        public static int GoodSkill(int jobLevel)
        {
            return 4 * jobLevel;
        }

        public static int GreatSkill(int jobLevel)
        {
            return 6 * jobLevel;
        }

        public static int EpicSkill(int jobLevel)
        {
            return 8 * jobLevel;
        }

        #endregion

        #region Saves

        public static int BadSave(int joblevel)
        {
            return joblevel/4;
        }

        public static int GoodSave(int joblevel)
        {
            return (2 + joblevel)/2;
        }

        #endregion

        #region BaseAttack

        public static int BadAttack(int level)
        {
            return level/2;
        }

        internal static int GoodAttack(int level)
        {
            var attack = level/1.5;
            return (int) attack;
        }

        public static int GreatAtttack(int joblevels)
        {
            return joblevels;
        }

        #endregion
    }
}