using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Raylib_cs;

namespace MathForGames
{
    class Camera : Actor
    {
        private Camera3D _camera3D;
        private Actor _cameraTarget;

        public Camera3D Camera3D
        {
            get { return _camera3D; }
            set { _camera3D = value; }
        }

        public Actor CameraTarget
        {
            get { return _cameraTarget; }
            set { _cameraTarget = value; }
        }
        public Vector3 Position
        {
            get { return _camera3D.position; }
            set { _camera3D.position = value; }
        }

        public Vector3 Target
        {
            get { return _camera3D.target; }
            set { _camera3D.target = value; }
        }

        public Vector3 Up
        {
            get { return _camera3D.up; }
            set { _camera3D.up = value; }
        }

        public float Fovy
        {
            get { return _camera3D.fovy; }
            set { _camera3D.fovy = value; }
        }

        public CameraProjection Projection
        {
            get { return _camera3D.projection; }
            set { _camera3D.projection = value; }
        }

        public Camera(Camera3D camera)
        {
            _camera3D = camera;
        }
        public Camera(Camera3D camera, Vector3 position, Vector3 target, Vector3 up, float fovy, CameraProjection projection)
        {
            _camera3D = camera;
            _camera3D.position = position;
            _camera3D.target = target;
            _camera3D.up = up;
            _camera3D.fovy = fovy;
            _camera3D.projection = projection;
        }

        public Camera(Vector3 position, Actor target, Vector3 up, float fovy, CameraProjection projection, Camera3D camera) :
            this(camera, position, new Vector3(target.WorldPosition.X, target.WorldPosition.Y, target.WorldPosition.Z), up, fovy, projection)
        {
            _cameraTarget = target;
        }

        public void FollowTarget()
        {
            Target = new Vector3(CameraTarget.WorldPosition.X, CameraTarget.WorldPosition.Y, CameraTarget.WorldPosition.Z);
            Position = new Vector3(CameraTarget.WorldPosition.X, CameraTarget.WorldPosition.Y + 5, CameraTarget.WorldPosition.Z + 10);
        }
    }
}
