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

        public Actor Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public ColliderType ColliderType
        {
            get { return _colliderType; }
        }

        public Vector3 CollisionNormal
        {
            get { return _collisionNormal; }
            set { _collisionNormal = value; }
        }
        public Collider(Actor owner, ColliderType colliderType)
        {
            _owner = owner;
            _colliderType = colliderType;
        }

        public bool CheckCollision(Actor other)
        {
            if (other.Collider.ColliderType == ColliderType.SPHERE)
                return CheckCollisionCircle((SphereCollider)other.Collider);
            else if (other.Collider.ColliderType == ColliderType.AABB)
                return CheckCollisionAABB((AABBCollider)other.Collider);

            return false;
        }

        public virtual bool CheckCollisionCircle(SphereCollider other) { return false; }

        public virtual bool CheckCollisionAABB(AABBCollider other) { return false; }

        public virtual void Update() { }
        public virtual void Draw() { }
    }
}
