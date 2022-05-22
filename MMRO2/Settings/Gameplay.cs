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
            { "cabbage", 10 },
            { "slime", 10 },
            { "frog", 50 },
            { "golem", 80 },
            { "shogun", 100 }
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

        public static readonly int[] MonsterCount = new int[] { 10, 12, 1, 17, 20, 23, 25, 28, 30 };
    }
}
