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
        public int EnemiesSpawned = 0;
        public bool BossDied = false;
        public bool BossSpawned = false;
        public bool Failed = false;

        public float PlayerHP = 500f;
        public float PlayerMaxHP = 500f;

        public float PlayerMana = 200f;
        public float PlayerMaxMana = 200f;

        public List<Sprites.Player.Bullet> Bullets = new List<Sprites.Player.Bullet>();
        public List<Main.Monster> Monsters = new List<Main.Monster>();
        public List<Sprites.EffectArea> BulletEffects = new List<Sprites.EffectArea>();
        public List<Sprites.Effect> StaticEffects = new List<Sprites.Effect>();

        public World World;
        public Types.Camera Camera;

        public Dictionary<Enums.BallTypes, float[]> SkillCooldown = new Dictionary<Enums.BallTypes, float[]>()
        {
            { Enums.BallTypes.Ice, new float[]{ 3, 3 } },
            { Enums.BallTypes.Fire, new float[]{ 5, 5 } },
            { Enums.BallTypes.Lightning, new float[]{ 15, 15 } },
            { Enums.BallTypes.Explosion, new float[]{ 30, 30 } }
        };

        public Dictionary<Enums.Perks, float> Perks = new Dictionary<Enums.Perks, float>()
        {
            { Enums.Perks.IncreaseBulletDamage, 0 },
            { Enums.Perks.ReduceManaUsage, 0 },
            { Enums.Perks.IncreaseMaxMana, 0 },
            { Enums.Perks.ReduceSkillCooldown, 0 },
            { Enums.Perks.IncreaseMaxHP, 0 },
        };

        public void Reset() {
            foreach (var bullet in Bullets)
            {
                World.Remove(bullet.Body);
            }

            foreach (var monster in Monsters)
            {
                World.Remove(monster.Body);
            }

            foreach (var effect in BulletEffects)
            {
                World.Remove(effect.Body);
            }

            Paused = false;
            PrevPaused = true;

            EnemiesSpawned = 0;
            BossDied = false;
            BossSpawned = false;
            Failed = false;

            PlayerHP = 500f;
            PlayerMaxHP = 500f;

            PlayerMana = 100f;
            PlayerMaxMana = 100f;

            Bullets.Clear();
            Monsters.Clear();
            BulletEffects.Clear();
            StaticEffects.Clear();
        }
    }
}
