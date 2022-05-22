using System;
using System.Collections.Generic;
using System.Text;

namespace MMRO2.Settings
{
    static class Gameplay
    {
        public const float PlayerBasePosition = 11f;

        public const float ProjectileSensitivityX = 7;
        public const float ProjectileSensitivityY = 2;
        public const float MaxProjectileForce = 15;

        public const float BaseBulletDamage = 20;
        public const float BaseFireDamage = 10;
        public const float BaseLightningDamage = 50;

        public static readonly Dictionary<string, int> Damages = new Dictionary<string, int>()
        {
            { "cabbage", 20 },
            { "slime", 30 },
            { "rabbit", 40 },
            { "vanir", 50 },
            { "crocodile", 30 },
            { "frog", 40 },
            { "golem", 80 },
            { "shogun", 100 },
        };

        public static readonly Dictionary<int, Enums.Monsters[]> MonsterList = new Dictionary<int, Enums.Monsters[]>()
        {
            { 1, new Enums.Monsters[]{ Enums.Monsters.Cabbage } },
            { 2, new Enums.Monsters[]{ Enums.Monsters.Cabbage} },
            { 3, new Enums.Monsters[]{ Enums.Monsters.Cabbage, Enums.Monsters.Slime } },
            { 4, new Enums.Monsters[]{ Enums.Monsters.Cabbage, Enums.Monsters.Slime } },
            { 5, new Enums.Monsters[]{ Enums.Monsters.Cabbage, Enums.Monsters.Slime, Enums.Monsters.Rabbit } },
            { 6, new Enums.Monsters[]{ Enums.Monsters.Cabbage, Enums.Monsters.Slime, Enums.Monsters.Rabbit } },
            { 7, new Enums.Monsters[]{ Enums.Monsters.Cabbage, Enums.Monsters.Slime, Enums.Monsters.Rabbit, Enums.Monsters.Vanir } },
            { 8, new Enums.Monsters[]{ Enums.Monsters.Cabbage, Enums.Monsters.Slime, Enums.Monsters.Rabbit, Enums.Monsters.Vanir } },
            { 9, new Enums.Monsters[]{ Enums.Monsters.SnowSlime } },
        };

        public static readonly Dictionary<Enums.BallTypes, float> ManaUsage = new Dictionary<Enums.BallTypes, float>()
        {
            { Enums.BallTypes.Normal, 0 },
            { Enums.BallTypes.Ice, 5 },
            { Enums.BallTypes.Fire, 10 },
            { Enums.BallTypes.Lightning, 15 },
        };

        public static readonly int[] MonsterCount = new int[] { 10, 20, 19, 20, 30, 29, 20, 30, 29 };

        public static readonly Dictionary<Enums.Perks, float[]> PerkStats = new Dictionary<Enums.Perks, float[]>()
        {
            { Enums.Perks.IncreaseBulletDamage, new float[]{ 1, 1.5f, 1.75f, 2f } },
            { Enums.Perks.NumberOfBullets, new float[]{ 0, 1, 2, 3 } },
            { Enums.Perks.ReduceManaUsage, new float[]{ 0, 20, 25, 30 } },
            { Enums.Perks.IncreaseMaxMana, new float[]{ 0, 20, 30, 50 } },
            { Enums.Perks.ReduceSkillCooldown, new float[]{ 0, 30, 40, 50 } },
            { Enums.Perks.ManaRegeneration, new float[]{ 0, 10, 15, 20 } },
            { Enums.Perks.IncreaseMaxHP, new float[]{ 0, 50, 75, 100 } },
            { Enums.Perks.IceBulletLevel, new float[]{ 0, 1.5f, 2, 3 } },
            { Enums.Perks.FireBulletLevel, new float[]{ 0, 10, 20, 30 } },
            { Enums.Perks.LightningBulletLevel, new float[]{ 1, 1.5f, 1.75f, 2f } },
        };
    }
}
