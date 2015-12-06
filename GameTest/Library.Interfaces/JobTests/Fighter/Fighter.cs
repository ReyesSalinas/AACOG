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

        public ProgressionType HitPointProgressionType { get; } = ProgressionType.Great;
        public ProgressionType BaseAttackProgressionType { get; } = ProgressionType.Great; 
        public ProgressionType FortitudeSaveProgressionType { get; }=ProgressionType.Good;
        public ProgressionType ReflexSaveProgressionType { get; }= ProgressionType.Bad;
        public ProgressionType WillSaveProgressionType { get; }= ProgressionType.Bad;
        public ProgressionType SkillRankProgressionType { get;}= ProgressionType.Bad; 
        public BaseStats BaseStats { get; set; }
        public IBonusSet JobBonuses { get; set; }
    }

    public class BaseStats
    {
        public List<int> HitPoints { get; set; }
        public List<int> SkillsRanks { get; set; } 
        public int AttackBonus { get; set; }
        public int FortitudesSaveBonus { get; set; }
        public int ReflexSaveBonus { get; set; }
        public int WillSaverBonus { get; set; }
    }

    public class LevelingService
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

            foreach (var job in character.Job)
            {
                // after reaching epic levels( > 20) you gain one point to your saves per level


                job.BaseStats.FortitudesSaveBonus = SaveCalculator(job.JobLevel, job.FortitudeSaveProgressionType);
                job.BaseStats.ReflexSaveBonus = SaveCalculator(job.JobLevel, job.ReflexSaveProgressionType);
                job.BaseStats.WillSaverBonus = SaveCalculator(job.JobLevel, job.WillSaveProgressionType);
            }
        }

        public void CalculateJobAttackBonus(ICharacter character)
        {
            foreach (var job in character.Job)
            {
                job.BaseStats.AttackBonus = AttackCalculator(job.JobLevel, job.BaseAttackProgressionType);
            }
        }

        public void CalculateHitPoints(ICharacterJob job)
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

        public void CalculateSkillRanks(ICharacterJob job,int intelMod)
        {
            switch (job.SkillRankProgressionType)
            {
                case ProgressionType.Bad:
                    job.BaseStats.SkillsRanks.Add(Formulas.BadSkill(intelMod));
                    break;
                case ProgressionType.Good:
                    job.BaseStats.SkillsRanks.Add(Formulas.GoodSkill(intelMod));
                    break;
                case ProgressionType.Great:
                    job.BaseStats.SkillsRanks.Add(Formulas.GreatSkill(intelMod));
                    break;
                case ProgressionType.Epic:
                    job.BaseStats.SkillsRanks.Add(Formulas.EpicSkill(intelMod));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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
        #region Skill Ranks

        public static int BadSkill(int mod)
        {
            return 2 + mod;
        }

        public static int GoodSkill(int mod)
        {
            return 4 + mod;
        }

        public static int GreatSkill(int mod)
        {
            return 6 + mod;
        }

        public static int EpicSkill(int mod)
        {
            return 8 + mod;
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