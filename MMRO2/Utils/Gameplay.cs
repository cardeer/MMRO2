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

        public static List<Enums.Perks> GetAvailablePerks()
        {
            List<Enums.Perks> list = new List<Enums.Perks>();

            var perks = Global.Instance.GameData.Perks;
            var keys = perks.Keys;

            foreach (var key in keys)
            {
                if (perks[key] < 3)
                {
                    list.Add(key);
                }
            }

            return list;
        }

        public static int[,] RandomUpgradePerk()
        {
            int[,] perks = new int[3,2];
            List<Enums.Perks> available = GetAvailablePerks();

            Random random = new Random();
            int ran = random.Next(0, available.Count);
            perks[0, 0] = (int)available[ran];
            perks[0, 1] = Global.Instance.GameData.Perks[available[ran]] + 1;
            available.RemoveAt(ran);

            ran = random.Next(0, available.Count);
            perks[1, 0] = (int)available[ran];
            perks[1, 1] = Global.Instance.GameData.Perks[available[ran]] + 1;
            available.RemoveAt(ran);

            ran = random.Next(0, available.Count);
            perks[2, 0] = (int)available[ran];
            perks[2, 1] = Global.Instance.GameData.Perks[available[ran]] + 1;

            return perks;
        }
    }
}
