using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;
using Microsoft.Xna.Framework;

namespace MMRO2.Main
{
    class PhysicsScene : GameScene
    {
        protected World World;
        protected Types.Camera Camera;

        public override void Initialize()
        {
            World = new World(new Vector2(0, Settings.Physics.Gravity));
            Camera = new Types.Camera();

            base.Initialize();
        }

        public override void Update()
        {
            World.Step((float)Global.Instance.GameTime.ElapsedGameTime.TotalSeconds);

            Camera.Update();

            SpriteEffect.View = Camera.View;
            SpriteEffect.Projection = Camera.Projection;

            base.Update();
        }
    }
}
