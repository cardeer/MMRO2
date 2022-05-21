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
            Enums.Monsters[] list = Settings.Gameplay.Monsters[Global.Instance.GameData.Wave];
            Enums.Monsters monster = list[random.Next(0, list.Length)];

            if (monster == Enums.Monsters.Cabbage) return new Sprites.Monsters.Cabbage(world);
            else if (monster == Enums.Monsters.Slime) return new Sprites.Monsters.Slime(world);
            //if (monster == Enums.Monsters.Rabbit) return new Sprites.Monsters.Rabbit(world);
            else if (monster == Enums.Monsters.Vanir) return new Sprites.Monsters.Vanir(world);
            else if (monster == Enums.Monsters.Frog) return new Sprites.Monsters.Frog(world);
            //if (monster == Enums.Monsters.Golem) return new Sprites.Monsters.Golem(world);

            return null;
        }
    }
}
