﻿using System;
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
        /// The size of the collider on the z axis
        /// </summary>
        public float Length
        {
            get { return _length; }
            set { _length = value; }
        }

        /// <summary>
        /// The Lowest X Value of this collider
        /// </summary>
        public float Left
        {
            get { return Owner.LocalPosition.X - (Width / 2); }
        }

        /// <summary>
        /// The Highest X Value of this collider
        /// </summary>
        public float Right
        {
            get { return Owner.LocalPosition.X + (Width / 2); }
        }

        /// <summary>
        /// The Highest Y Value of this collider
        /// </summary>
        public float Top
        {
            get { return Owner.LocalPosition.Y + (Height / 2); }
        }

        /// <summary>
        /// The Lowest Y Value of this collider
        /// </summary>
        public float Bottom
        {
            get{ return Owner.LocalPosition.Y - (Height / 2); }
        }

        /// <summary>
        /// The Highest Z Value of this collider
        /// </summary>
        public float Front
        {
            get { return Owner.LocalPosition.Z + (Length / 2); }
        }

        /// <summary>
        /// The Lowest Z Value of this collider
        /// </summary>
        public float Back
        {
            get { return Owner.LocalPosition.Z - (Length / 2); }
        }

        /// <summary>
        /// The AABBCollider Constructor
        /// </summary>
        /// <param name="width">The Width of the The Collider</param>
        /// <param name="height">The height of the Collider</param>
        /// <param name="length">The Length of the Collider</param>
        /// <param name="owner">The Owner of the Collider</param>
        public AABBCollider(float width, float height, float length, Actor owner) : base(owner, ColliderType.AABB)
        {
            _width = width;
            _height = height;
            _length = length;
        }

        /// <summary>
        /// The AABBCollider using only the Owner
        /// </summary>
        /// <param name="owner"></param>
        public AABBCollider(Actor owner) : base(owner, ColliderType.AABB)
        {
            _width = owner.Size.X;
            _height = owner.Size.Y;
            _length = owner.Size.Z;
        }

        /// <summary>
        /// Checks the Collision between this collider and a Sphere Collider
        /// </summary>
        /// <param name="other">The other collider</param>
        /// <returns>If Collision Occured</returns>
        public override bool CheckCollisionSphere(SphereCollider other)
        {
            return other.CheckCollisionAABB(this);
        }

        /// <summary>
        /// Checks the Collision between this collider and a AABB Collider
        /// </summary>
        /// <param name="other">The other collider</param>
        /// <returns>If Collision Occured</returns>
        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //Checks if the other Collider and this collider belong to the same owner
            if (other.Owner == Owner)
                return false;

            CollisionNormal = (other.Owner.WorldPosition - Owner.WorldPosition).Normalized;
            //Returns True if there is an overlap betweens the two colliders
            return other.Left <= Right &&
                   other.Bottom <= Top &&
                   other.Back <= Front &&
                   Left <= other.Right &&
                   Bottom <= other.Top &&
                   Back <= other.Front;
        }

        /// <summary>
        /// Updates the Collider to match the Size of the Owner
        /// </summary>
        public override void Update()
        {
            _width = Owner.Size.X;
            _height = Owner.Size.Y;
            _length = Owner.Size.Z;
        }

        /// <summary>
        /// Draws the Collider
        /// </summary>
        public override void Draw()
        {
            Raylib.DrawCubeWires(new System.Numerics.Vector3(Owner.WorldPosition.X, Owner.WorldPosition.Y, Owner.WorldPosition.Z), Width, Height, Length, Color.RED);
        }
    }
}
