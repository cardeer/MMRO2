using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace MMRO2.Main
{
    public abstract class GameScene
    {
        protected BasicEffect SpriteEffect;

        protected GameScene Scene { get; }

        public virtual void Initialize()
        {
            SpriteEffect = new BasicEffect(Global.Instance.GraphicsDevice);
            SpriteEffect.TextureEnabled = true;
        }

        public virtual void LoadContent()
        {
            
        }

        public virtual void Update()
        {
            Global.Instance.PrevMouseState = Mouse.GetState();
            Global.Instance.PrevKeyboardState = Keyboard.GetState();
        }

        public virtual void Draw()
        {

        }

        protected void BeginSprite(bool physicsDrawing = false)
        {
            if (!physicsDrawing)
            {
                Global.Instance.SpriteBatch.Begin();
                return;
            }

            // For physics drawing
            // View/Projection requires RasterizerState.CullClockwise and SpriteEffects.FlipVertically.
            Global.Instance.SpriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, RasterizerState.CullClockwise, SpriteEffect);
        }

        protected void EndSprite()
        {
            Global.Instance.SpriteBatch.End();
        }
    }
}
