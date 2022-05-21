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

        public static readonly Dictionary<string, int> Damages = new Dictionary<string, int>()
        {
            { "cabbage", 10 },
            { "slime", 10 }
        };
    }
}
