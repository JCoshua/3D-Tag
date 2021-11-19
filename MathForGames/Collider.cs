using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames
{
    public enum ColliderType
    {
        SPHERE,
        AABB
    }
    abstract class Collider
    {
        private Actor _owner;
        private ColliderType _colliderType;
        private Vector3 _collisionNormal;

        /// <summary>
        /// The Owner of the Collider
        /// </summary>
        public Actor Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        /// <summary>
        /// The Type of collider
        /// </summary>
        public ColliderType ColliderType
        {
            get { return _colliderType; }
        }

        /// <summary>
        /// The direction of Collision
        /// </summary>
        public Vector3 CollisionNormal
        {
            get { return _collisionNormal; }
            set { _collisionNormal = value; }
        }

        /// <summary>
        /// THe Base Colider Constructor
        /// </summary>
        /// <param name="owner">The onwer of the collider</param>
        /// <param name="colliderType">The Collider Type</param>
        public Collider(Actor owner, ColliderType colliderType)
        {
            _owner = owner;
            _colliderType = colliderType;
        }

        /// <summary>
        /// Checks the Collision between this two colliders
        /// </summary>
        /// <param name="other">The owner of the other collider</param>
        /// <returns>If Collision Occured</returns>
        public bool CheckCollision(Actor other)
        {
            if (other.Collider.ColliderType == ColliderType.SPHERE)
                return CheckCollisionSphere((SphereCollider)other.Collider);
            else if (other.Collider.ColliderType == ColliderType.AABB)
                return CheckCollisionAABB((AABBCollider)other.Collider);

            return false;
        }

        /// <summary>
        /// Checks the Collision between this collider and a Sphere Collider
        /// </summary>
        /// <param name="other">The other collider</param>
        /// <returns>If Collision Occured</returns>
        public virtual bool CheckCollisionSphere(SphereCollider other) { return false; }

        /// <summary>
        /// Checks the Collision between this collider and an AABB Collider
        /// </summary>
        /// <param name="other">The other collider</param>
        /// <returns>If Collision Occured</returns>
        public virtual bool CheckCollisionAABB(AABBCollider other) { return false; }

        public virtual void Update() { }
        public virtual void Draw() { }
    }
}
