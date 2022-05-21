using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using tainicom.Aether.Physics2D.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MMRO2.Scenes
{
	class Story : Main.GameScene
	{
		private SpriteBatch _spriteBatch;
		private BasicEffect _spriteBatchEffect;
		private SpriteFont _font;


		// physics
		private Types.Camera _camera = new Types.Camera();


		public override void Initialize()
		{
			base.Initialize();

			_spriteBatch = new SpriteBatch(Global.Instance.GraphicsDevice);
			// We use a BasicEffect to pass our view/projection in _spriteBatch
			_spriteBatchEffect = new BasicEffect(Global.Instance.GraphicsDevice);
			_spriteBatchEffect.TextureEnabled = true;

			_font = Global.Instance.Content.Load<SpriteFont>("fonts/font");

			// Load sprites

		}

		public override void Update()
		{

		}

		public override void Draw()
		{
			// Update camera View and Projection.
			_camera.Update();
			_spriteBatchEffect.View = _camera.View;
			_spriteBatchEffect.Projection = _camera.Projection;
		}
	}
}
