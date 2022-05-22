using System;
using System.Collections.Generic;
using System.Text;

namespace MMRO2.Utils.Scene
{
    static class Factory
    {
        public static Main.GameScene Get(Enums.Scenes type)
        {
            switch (type)
            {
                case Enums.Scenes.Playing:
                    return new Scenes.Playing();

                case Enums.Scenes.Story:
                    return new Scenes.Story();

                case Enums.Scenes.Home:
                    return new Scenes.Home();

                case Enums.Scenes.Credit:
                    return new Scenes.Credit();

                case Enums.Scenes.Gacha:
                    return new Scenes.SelectCards();

                default:
                    throw new Exception("Scene not found.");
            }
        }
    }
}
