using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MMRO2.Utils
{
    static class Input
    {
        public static bool IsLeftMouseClicked()
        {
            return Global.Instance.CurrentMouseState.LeftButton == ButtonState.Pressed && Global.Instance.CurrentMouseState.LeftButton != Global.Instance.PrevMouseState.LeftButton;
        }

        public static bool IsRightMouseClicked()
        {
            return Global.Instance.CurrentMouseState.RightButton == ButtonState.Pressed && Global.Instance.CurrentMouseState.RightButton != Global.Instance.PrevMouseState.RightButton;
        }

        public static bool IsKeyPressed(Keys key)
        {
            return Global.Instance.CurrentKeyboardState.IsKeyDown(key) && Global.Instance.PrevKeyboardState.IsKeyUp(key);
        }
    }
}
