using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Utils.Sprite
{
    class Factory
    {
        public static Texture2D CreateRectangle(int width, int height, Color color)
        {
            Texture2D temp = new Texture2D(Global.Instance.GraphicsDevice, width, height);
            Color[] tempColor = new Color[width * height];
            for (int i = 0; i < tempColor.Length; i++) tempColor[i] = color;
            temp.SetData(tempColor);

            return temp;
        }
    }
}
