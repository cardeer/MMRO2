using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MMRO2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static Game1 s_game1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            s_game1 = this;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Settings.Window.Width;
            _graphics.PreferredBackBufferHeight = Settings.Window.Height;
            _graphics.ApplyChanges();

            Window.Title = Settings.Window.Title;
            //Window.AllowUserResizing = true;

            Global.Instance.GraphicsDevice = _graphics.GraphicsDevice;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Global.Instance.SpriteBatch = _spriteBatch;
            Global.Instance.Content = Content;

            Global.Instance.InvisibleRect = Utils.Sprite.Factory.CreateRectangle(1, 1, new Color(0, 0, 0, 0));

            Utils.Scene.Control.ChangeScene(Enums.Scenes.Playing);
        }


        protected override void Update(GameTime gameTime)
        {
            Global.Instance.GameTime = gameTime;

            MouseState mouseState = Mouse.GetState();
            KeyboardState keyboardState = Keyboard.GetState();

            Global.Instance.CurrentMouseState = mouseState;
            Global.Instance.CurrentKeyboardState = keyboardState;

            if (Utils.Input.IsKeyPressed(Keys.Escape))
            {
                Global.Instance.GameData.Paused = !Global.Instance.GameData.Paused;
            }

            if (Global.Instance.CurrentScene != null)
            {
                Global.Instance.CurrentScene.Update();
            }

            Global.Instance.PrevMouseState = mouseState;
            Global.Instance.PrevKeyboardState = keyboardState;
            Global.Instance.GameData.PrevPaused = Global.Instance.GameData.Paused;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (Global.Instance.CurrentScene != null)
            {
                Global.Instance.CurrentScene.Draw();
            }
            Global.Instance.SpriteBatch.Begin();

            Global.Instance.SpriteBatch.End();


            base.Draw(gameTime);
        }

        public static void Quit()
        {
            s_game1.Exit();
        }
    }
}
