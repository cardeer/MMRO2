using System;
using System.Collections.Generic;
using System.Text;

namespace MMRO2.Settings
{
    static class CollisionList
    {
        public static readonly string[] Player = { Collision.Ground, Collision.Edge };
        public static readonly string[] Bullet = { Collision.Monster, Collision.Ground, Collision.Tower };
    }
}
