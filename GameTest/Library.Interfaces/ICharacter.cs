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
        int HitPoints { get; set; }
        Abilities Abilities { get; set; }
        List<ICharacterJob> Jobs { get; set; }
        CharacterBaseStats BaseStats { get; set; }
        Race Race { get; set; }
        
    }

    public class Abilities
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Wisdom { get; set; }
        public int Intelligence { get; set; }
        public int Charisma { get; set; }

    }

    public interface ILevelingServce
    {
        void CalculateSave(ICharacter character);

        void CalculateJobAttackBonus(ICharacter character);

        void CalculateHitPoints(ICharacter character);

        void CalculateSkillRanks(ICharacter character);

        
    }

    public class CharacterLevelingService : ILevelingServce
    {
        JobLevelingService jobLevelingService = new JobLevelingService();

        public void CalculateSave(ICharacter character)
        {
          
            jobLevelingService.CalculateSave(character);
            character.BaseStats.ReflexSaveBonus = character.Jobs.Sum(job => job.BaseStats.ReflexSaveBonus) + Formulas.GetModifier(character.Abilities.Dexterity);
            character.BaseStats.FortitudesSaveBonus = character.Jobs.Sum(job => job.BaseStats.FortitudesSaveBonus) + Formulas.GetModifier(character.Abilities.Constitution); 
            character.BaseStats.WillSavesBonus = character.Jobs.Sum(job => job.BaseStats.WillSavesBonus) + Formulas.GetModifier(character.Abilities.Wisdom); 
        }

        public void CalculateJobAttackBonus(ICharacter character)
        {
            int attackBonus;
            jobLevelingService.CalculateJobAttackBonus(character);
            attackBonus += character.Jobs.Sum(job => job.BaseStats.AttackBonus);
            character.BaseStats.AttackBonus = attackBonus;
        }
        
        public void CalculateHitPoints(ICharacter character)
        {
            jobLevelingService.CalculateHitPoints(character);
            
            //character.HitPoints = character.Jobs.Sum(job => job.HitPoints)
        }

        public void CalculateSkillRanks(ICharacter character)
        {
            throw new NotImplementedException();
        }

        public void LevelUp(ICharacterJob job)
        {
            job.JobLevel++;

        }
    }
}
