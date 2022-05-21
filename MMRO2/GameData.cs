using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;

namespace MMRO2
{
    class GameData
    {
        public bool Paused = false;
        public bool PrevPaused = true;

        public int Wave = 1;

        public float PlayerHP = 500f;
        public float PlayerMaxHP = 500f;

        public float PlayerMana = 100f;
        public float PlayerMaxMana = 100f;

        public List<Sprites.Player.Bullet> Bullets = new List<Sprites.Player.Bullet>();
        public List<Main.Monster> Monsters = new List<Main.Monster>();

        public World World;
        public Types.Camera Camera;

        public GameData Reset() {
            foreach (var bullet in Bullets)
            {
                World.Remove(bullet.Body);
            }

            foreach (var monster in Monsters)
            {
                World.Remove(monster.Body);
            }

            GameData newData = new GameData();
            newData.World = World;
            newData.Camera = Camera;

            return newData;
        }
    }
}
