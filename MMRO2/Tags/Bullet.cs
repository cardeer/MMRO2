using System;
using System.Collections.Generic;
using System.Text;

namespace MMRO2.Tags
{
    class Bullet : Main.Tag
    {
        public Enums.BallTypes Type;

        public Bullet()
        {
            Name = Settings.Collision.Bullet;
        }
    }
}
