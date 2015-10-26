using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PortableClassLibrary1
{
    public struct Point 
    {
        int y {get;set;}
        int x {get;set;}
        public Point() { }
        public Point(int y,int x)
        {
            this.y = y;
            this.x = x;
        }
    }
    public abstract class Character
    {
        //base stats for all characters, sticking to DND mechanics with a littl bit of flair
        //these stats will be the base for most if not all the mechanics in this game
        int Strength { get; set; }
        int Dexterity { get; set; }
        int Vitatlity { get; set; }
        int Mind { get; set; }
        int Intelligence { get; set; }
        int Charisma { get; set; }
        int Luck { get; set; }

        //Secondary Stats
        // these stats are base upon other stast plus skills and actions
        // I believe the best way to show them, therefore is by making functions that 
        // define them rather than hold them as data when they only need to be known when 
        // as a full value when you look at the characters stats
        public virtual int ArmorClass(int armor,int bonus)
        {
            int result = 0;

            return result;
        }
        public virtual int AttackBonus()
        { return 0; }
        public virtual int Dodgechance()
        { return 0; }
        public virtual Point Damage(Point weaponRange, int bonus)
        {
            return new Point();
        }
        public virtual int TreasureBonus()
        { return 0; }


    }
}
