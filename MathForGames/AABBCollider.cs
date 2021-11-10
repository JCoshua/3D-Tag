using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class AABBCollider : Collider
    {
        private float _width;
        private float _height;
        private float _length;

        /// <summary>
        /// The size of the collider on the x axis
        /// </summary>
        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// The size of the collider on the y axis
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// The size of the collider on the y axis
        /// </summary>
        public float Length
        {
            get { return _length; }
            set { _length = value; }
        }

        /// <summary>
        /// The furthest Left X value of this collider
        /// </summary>
        public float Left
        {
            get
            {
                return Owner.LocalPosition.X - (Width / 2);
            }
        }

        /// <summary>
        /// The furthest right X value of this collider
        /// </summary>
        public float Right
        {
            get
            {
                return Owner.LocalPosition.X + (Width / 2);
            }
        }

        /// <summary>
        /// The Highest Y Value of this collider
        /// </summary>
        public float Top
        {
            get
            {
                return Owner.LocalPosition.Y + (Height / 2);
            }
        }

        /// <summary>
        /// The Lowest Y Value of this collider
        /// </summary>
        public float Bottom
        {
            get
            {
                return Owner.LocalPosition.Y - (Height / 2);
            }
        }

        /// <summary>
        /// The front of the Collider
        /// </summary>
        public float Front
        {
            get
            {
                return Owner.LocalPosition.Z + (Length / 2);
            }
        }

        /// <summary>
        /// The Back of the Collider
        /// </summary>
        public float Back
        {
            get
            {
                return Owner.LocalPosition.Z - (Length / 2);
            }
        }

        public AABBCollider(float width, float height, float length, Actor owner) : base(owner, ColliderType.AABB)
        {
            _width = width;
            _height = height;
            _length = length;
        }

        public AABBCollider(Actor owner) : base(owner, ColliderType.AABB)
        {
            _width = owner.Size.X;
            _height = owner.Size.Y;
            _length = owner.Size.Z;
        }

        public override bool CheckCollisionCircle(SphereCollider other)
        {
            return other.CheckCollisionAABB(this);
        }

        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //Checks if the other Collider and this collider belong to the same owner
            if (other.Owner == Owner)
                return false;

            //Returns True if there is an overlap betweens the two colliders
            return other.Left <= Right &&
                   other.Bottom <= Top &&
                   other.Back <= Front &&
                   Left <= other.Right &&
                   Bottom <= other.Top &&
                   Back <= other.Front;
        }

        public override void Update()
        {
            _width = Owner.Size.X;
            _height = Owner.Size.Y;
            _length = Owner.Size.Z;
        }

        public override void Draw()
        {
            Raylib.DrawCubeWires(new System.Numerics.Vector3(Owner.WorldPosition.X, Owner.WorldPosition.Y, Owner.WorldPosition.Z), Width, Height, Length, Color.RED);
        }
    }
}
