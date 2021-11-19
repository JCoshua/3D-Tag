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
        private bool _targetOrigin = false;

        /// <summary>
        /// The RayLib Camera
        /// </summary>
        public Camera3D Camera3D
        {
            get { return _camera3D; }
            set { _camera3D = value; }
        }

        /// <summary>
        /// The Actor Target of the Camera
        /// </summary>
        public Actor CameraTarget
        {
            get { return _cameraTarget; }
            set { _cameraTarget = value; }
        }

        /// <summary>
        /// Camera's Position
        /// </summary>
        public Vector3 Position
        {
            get { return _camera3D.position; }
            set { _camera3D.position = value; }
        }

        /// <summary>
        /// Camera's Focus Point
        /// </summary>
        public Vector3 Target
        {
            get { return _camera3D.target; }
            set { _camera3D.target = value; }
        }

        /// <summary>
        /// Camera's Up Vector (rotation towards target)
        /// </summary>
        public Vector3 Up
        {
            get { return _camera3D.up; }
            set { _camera3D.up = value; }
        }

        /// <summary>
        /// The Field of View on the Y Axis(Zoom)
        /// </summary>
        public float Fovy
        {
            get { return _camera3D.fovy; }
            set { _camera3D.fovy = value; }
        }

        /// <summary>
        /// The Camera's mode type
        /// </summary>
        public CameraProjection Projection
        {
            get { return _camera3D.projection; }
            set { _camera3D.projection = value; }
        }

        /// <summary>
        /// True if the Camera should target the origin
        /// </summary>
        public bool TargetOrigin
        {
            get { return _targetOrigin; }
            set { _targetOrigin = value; }
        }

        /// <summary>
        /// The Camera Constructor using only RayLib's Camera
        /// </summary>
        /// <param name="camera"></param>
        public Camera(Camera3D camera)
        {
            _camera3D = camera;
        }

        /// <summary>
        /// The Camera Constuctor
        /// </summary>
        /// <param name="camera">The RayLib Camera</param>
        /// <param name="position">The Camera's Position</param>
        /// <param name="target">The Camera's Target</param>
        /// <param name="up">The Up Vector (rotation towards target)</param>
        /// <param name="fovy">The Field of View on the Y Axis(Zoom)</param>
        /// <param name="projection">The Camera's Projection Type</param>
        public Camera(Camera3D camera, Vector3 position, Vector3 target, Vector3 up, float fovy, CameraProjection projection)
        {
            _camera3D = camera;
            _camera3D.position = position;
            _camera3D.target = target;
            _camera3D.up = up;
            _camera3D.fovy = fovy;
            _camera3D.projection = projection;
        }

        /// <summary>
        /// Updates the Camera
        /// </summary>
        public override void Update(float deltaTime)
        {
            //If the Camera should not target the origin
            if(!TargetOrigin)
            {
                Target = new Vector3(CameraTarget.WorldPosition.X, CameraTarget.WorldPosition.Y, CameraTarget.WorldPosition.Z);
                Position = new Vector3(CameraTarget.WorldPosition.X - CameraTarget.Forward.X * 25, CameraTarget.WorldPosition.Y + 5, CameraTarget.WorldPosition.Z - CameraTarget.Forward.Z * 25);
            }
            //else Target Origin
            else if(TargetOrigin)
            {
                CameraTarget.Rotate(0, 0.01f, 0);
                Position = new Vector3(100 - CameraTarget.Forward.X * 25, 50, 100 - CameraTarget.Forward.Z * 25);
            }

            base.Update(deltaTime);
        }
    }
}
