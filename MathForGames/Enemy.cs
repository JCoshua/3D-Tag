using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Enemy : Actor
    {
        private float _speed;
        private Vector3 _velocity;
        private Ally _target;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Enemy(float x, float y, float z, float speed, string name = "Actor")
            : base(x, y, z, name)
        {
            _speed = speed;
            Forward = new Vector3(0, 0, 1);
        }

        public override void Start()
        {
            Actor head = new Actor(0, 0.85f, 0, "Head", Shape.SPHERE);
            head.SetScale(0.5f, 0.5f, 0.5f);
            head.Collider = new SphereCollider(head);
            head.SetColor(255, 100, 100, 255);
            AddChild(head);

            Actor body = new Actor(0, 0, 0, "Body", Shape.CUBE);
            body.SetScale(0.75f, 1, 0.5f);
            body.Collider = new AABBCollider(body);
            body.SetColor(255, 10, 10, 255);
            AddChild(body);

            SetTranslation(-25, 0, 25);
            base.Start();
        }
        public override void Update(float deltaTime)
        {
            GetTarget();
            Forward = _target.WorldPosition.Normalized;
            Vector3 moveDirection = (_target.WorldPosition - WorldPosition).Normalized;
            Velocity = moveDirection.Normalized * Speed * deltaTime;

            //Translate(Velocity);

            if (WorldPosition.Y < 0.5f)
            {
                //Sets the Vertical Acceleration to 0
                Acceleration = new Vector3(Acceleration.X, 0, Acceleration.Z);
                //Set their position to be on the Ground
                SetTranslation(WorldPosition.X, 0.5f, WorldPosition.Z);
                //Declare them grounded
                IsActorGrounded = true;
            }
            else
                //They are not grounded
                IsActorGrounded = false;

            base.Update(deltaTime);
        }

        public override void Draw()
        {
            base.Draw();
        }

        public bool GetTargetInSight(AABBCollider actorCollider)
        {
            Vector3 directionOfTarget = (WorldPosition - _target.WorldPosition).Normalized;

            return (Vector3.GetRadian(directionOfTarget, Forward) > 2 & Vector3.Distance(_target.WorldPosition, WorldPosition) < 200);
        }

        public override void OnCollision(Actor actor)
        {
            
        }

        public void GetTarget()
        {
            _target = SceneManager.Allies[0];
            for (int i = 0; i < SceneManager.Allies.Length; i++)
                if (Vector3.Distance(_target.WorldPosition, WorldPosition) > Vector3.Distance(SceneManager.Allies[i].WorldPosition, WorldPosition))
                    _target = SceneManager.Allies[i];
        }
    }
}
