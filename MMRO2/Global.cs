using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MMRO2
{
    class Global
    {
        // stored data
        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;
        public ContentManager Content;
        public GameTime GameTime;

        public Enums.Scenes CurrentSceneType = Enums.Scenes.None;
        public Main.GameScene CurrentScene;

        public MouseState CurrentMouseState = Mouse.GetState();
        public MouseState PrevMouseState = Mouse.GetState();

        public KeyboardState CurrentKeyboardState = Keyboard.GetState();
        public KeyboardState PrevKeyboardState = Keyboard.GetState();

        // gameplay data
        public Texture2D InvisibleRect;
        public GameData GameData = new GameData();

        // create global instance and return
        private static Global s_current;
        private static Global Get()
        {
            if (s_current == null)
            {
                s_current = new Global();
                return s_current;
            }

            return s_current;
        }

        public static Global Instance
        {
            get { return Get(); }
        }
    }
}
