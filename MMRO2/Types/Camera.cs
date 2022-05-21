using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Types
{
    class Camera
    {
        public Vector3 Position = new Vector3(0, 0, 0);

        public Matrix View;
        public Matrix Projection;

        public float Width = Settings.Camera.CameraWidth;
        public float Height;

        private Matrix _normalView;
        private Matrix _normalProjection;

        public Camera()
        {
            Initialize();
        }

        public Camera(Vector3 InitialPosition)
        {
            Position = InitialPosition;
            Initialize();
        }

        public void Initialize()
        {
            float aspectRatio = Global.Instance.GraphicsDevice.Viewport.AspectRatio;
            Height = Width / aspectRatio;

            Vector3 normalPosition = new Vector3(0, 0, 0);
            _normalView = Matrix.CreateLookAt(normalPosition, normalPosition + Vector3.Forward, Vector3.Up);
            _normalProjection = Matrix.CreateOrthographic(Width, Height, 0f, -1f);

            Update();
        }

        public void Update()
        {
            float aspectRatio = Global.Instance.GraphicsDevice.Viewport.AspectRatio;
            Height = Width / aspectRatio;

            View = Matrix.CreateLookAt(Position, Position + Vector3.Forward, Vector3.Up);
            Projection = Matrix.CreateOrthographic(Width, Height, 0f, -1f);
        }

        public Vector2 ConvertScreenToWorld(int x, int y)
        {
            Vector3 temp = Global.Instance.GraphicsDevice.Viewport.Unproject(new Vector3(x, y, 0), Projection, View, Matrix.Identity);
            return new Vector2(temp.X, temp.Y);
        }

        public Vector2 ConvertScreenToWorld(Vector2 position)
        {
            Vector3 temp = Global.Instance.GraphicsDevice.Viewport.Unproject(new Vector3(position.X, position.Y, 0), Projection, View, Matrix.Identity);
            return new Vector2(temp.X, temp.Y);
        }

        public Vector2 ConvertScreenToWorld(Point point)
        {
            Vector3 temp = Global.Instance.GraphicsDevice.Viewport.Unproject(new Vector3(point.X, point.Y, 0), Projection, View, Matrix.Identity);
            return new Vector2(temp.X, temp.Y);
        }

        public Vector2 ConvertWorldToScreen(Vector2 position)
        {
            Vector3 temp = Global.Instance.GraphicsDevice.Viewport.Project(new Vector3(position, 0), Projection, View, Matrix.Identity);
            return new Vector2(temp.X, temp.Y);
        }

        public float ConvertWidthToWorld(float width)
        {
            Vector3 temp = Global.Instance.GraphicsDevice.Viewport.Unproject(new Vector3(width + Settings.Window.Width / 2, 0, 0), _normalProjection, _normalView, Matrix.Identity);
            return temp.X;
        }

        public float ConvertHeightToWorld(float height)
        {
            Vector3 temp = Global.Instance.GraphicsDevice.Viewport.Unproject(new Vector3(0, Settings.Window.Height / 2 - height, 0), _normalProjection, _normalView, Matrix.Identity);
            return temp.Y;
        }
    }
}
