using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class SphereCollider : Collider
    {
        private float _collisionRadius;

        /// <summary>
        /// The Radius of the Collider
        /// </summary>
        public float CollisionRadius
        {
            get { return _collisionRadius; }
            set { _collisionRadius = value; }
        }

        /// <summary>
        /// The Base Sphere Collider Constructor
        /// </summary>
        /// <param name="collisionRadius">The Radius of the Colliders</param>
        /// <param name="owner">The Onwer of the Collider</param>
        public SphereCollider(float collisionRadius, Actor owner) : base(owner, ColliderType.SPHERE)
        {
            CollisionRadius = collisionRadius;
        }

        /// <summary>
        /// The SphereCollider Constructor using only the Owner
        /// </summary>
        /// <param name="owner">The Owner of the Collider</param>
        public SphereCollider (Actor owner) : base(owner, ColliderType.SPHERE)
        {
            CollisionRadius = Owner.Size.X;
        }

        /// <summary>
        /// Checks the Collision between this collider and a Sphere Collider
        /// </summary>
        /// <param name="other">The other collider</param>
        /// <returns>If Collision Occured</returns>
        public override bool CheckCollisionSphere(SphereCollider other)
        {
            //Checks if the other Collider and this collider belong to the same owner
            if (other.Owner == Owner)
                return false;

            //Finds the distatnce between the two actors
            float distance = Vector3.Distance(other.Owner.LocalPosition, Owner.LocalPosition);

            //Find the length of the raddi combined
            float combinedRadii = other.CollisionRadius + CollisionRadius;

            return distance <= combinedRadii;
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

            //Gets the direction from the collider to the AABB
            Vector3 direction = Owner.WorldPosition - other.Owner.WorldPosition;

            //Clamps the direction to be within the AABB Collider
            direction.X = Math.Clamp(direction.X, -other.Width / 2, other.Width / 2);
            direction.Y = Math.Clamp(direction.Y, -other.Height / 2, other.Height / 2);
            direction.Z = Math.Clamp(direction.Z, -other.Length / 2, other.Length / 2);

            //Finds the closest point by adding the direction vector to the AABB Center
            Vector3 closestPoint = other.Owner.WorldPosition + direction;

            CollisionNormal = (closestPoint - Owner.WorldPosition).Normalized;
            other.CollisionNormal = (Owner.WorldPosition - closestPoint).Normalized;
            //Finds the distance from the sphere's center to the closest point
            float distanceFromClosestPoint = Vector3.Distance(Owner.WorldPosition, closestPoint);

            //Returns if the distance from the closest point is less than the Collision Radius
            return distanceFromClosestPoint <= CollisionRadius;
        }

        /// <summary>
        /// Draws the Collider
        /// </summary>
        public override void Draw()
        {
            Raylib.DrawSphere(new System.Numerics.Vector3(Owner.WorldPosition.X, Owner.WorldPosition.Y, Owner.WorldPosition.Z), CollisionRadius, Color.RED);
        }
    }
}
