using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace MMRO2.Utils
{
    static class Gameplay
    {
        public static Main.Monster RandomMonster(World world)
        {
            Random random = new Random();
            Enums.Monsters[] list = Settings.Gameplay.MonsterList[Global.Instance.GameData.Wave];
            Enums.Monsters monster = list[random.Next(0, list.Length)];

            if (monster == Enums.Monsters.Cabbage) return new Sprites.Monsters.Cabbage(world);
            else if (monster == Enums.Monsters.Slime) return new Sprites.Monsters.Slime(world);
            else if (monster == Enums.Monsters.Rabbit) return new Sprites.Monsters.Rabbit(world);
            else if (monster == Enums.Monsters.Vanir) return new Sprites.Monsters.Vanir(world);
            else if (monster == Enums.Monsters.SnowSlime) return new Sprites.Monsters.SnowSlime(world);

            return null;
        }

        public static Main.Monster GetBoss(World world, int wave)
        {
            if (wave == 3) return new Sprites.Monsters.Frog(world);
            else if (wave == 6) return new Sprites.Monsters.Golem(world);
            else if (wave == 9) return new Sprites.Monsters.Shogun(world);

            return null;
        }
    }
}
