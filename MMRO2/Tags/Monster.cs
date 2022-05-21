using System;
using System.Collections.Generic;
using System.Text;

namespace MMRO2.Tags
{
    class Monster : Main.Tag
    {
        public float Health = 100;
        public float MaxHealth = 100;
        public float Speed = 1;

        public Monster()
        {
            Name = Settings.Collision.Monster;
        }
    }
}
