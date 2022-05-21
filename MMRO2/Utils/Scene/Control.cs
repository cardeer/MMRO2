using System;
using System.Collections.Generic;
using System.Text;

namespace MMRO2.Utils.Scene
{
    static class Control
    {
        public static void ChangeScene(Enums.Scenes type)
        {
            if (Global.Instance.CurrentSceneType == Enums.Scenes.None || type != Global.Instance.CurrentSceneType)
            {
                Main.GameScene scene = Factory.Get(type);
                scene.Initialize();
                scene.LoadContent();

                Global.Instance.CurrentSceneType = type;
                Global.Instance.CurrentScene = scene;
            }
        }
    }
}
