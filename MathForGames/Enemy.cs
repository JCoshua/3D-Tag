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
        private bool _isTagger = false;

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

        public bool IsTagger
        {
            get {return _isTagger; }
            set { _isTagger = value; }
        }

        public Enemy(float x, float y, float z, float speed, string name = "Actor")
            : base(x, y, z, name)
        {
            _speed = speed;
        }

        public override void Start()
        {
            Actor head = new Actor(0, 0.85f, 0, "Head", Shape.SPHERE);
            head.SetScale(0.5f, 0.5f, 0.5f);
            head.Collider = new SphereCollider(head);
            head.SetColor(255, 100, 100, 255);
            AddChild(head);

            Actor body = new Actor(0, 0, 0, "Body", Shape.CUBE);
            body.SetScale(0.75f, 1, 0.75f);
            body.Collider = new AABBCollider(body);
            body.SetColor(255, 10, 10, 255);
            AddChild(body);

            base.Start();
        }
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            if (IsTagger)
            {
                Console.WriteLine(Name + " is It");
                if (GetTargetOffense())
                {
                    Vector3 moveDirection = (_target.WorldPosition - WorldPosition).Normalized;
                    Forward = moveDirection;
                    Velocity = moveDirection * Speed * deltaTime;
                    Translate(Velocity);
                }
                else
                {
                    for (int i = 0; i < Scene.Actors.Length; i++)
                        if (Scene.Actors[i] is Wall)
                        {
                            Wall checkWall = (Wall)Scene.Actors[i];
                            checkWall.CheckMovement(this);
                        }
                    Velocity = Forward * Speed * deltaTime;
                    Translate(Velocity);
                }
            }
            else if (!IsTagger)
            {
                Console.WriteLine(Name + " is It");
                if (GetTargetDefense())
                {
                    Vector3 moveDirection = new Vector3(-(_target.WorldPosition.X - WorldPosition.X), 0, -(_target.WorldPosition.Z - WorldPosition.Z)).Normalized;
                    Forward = moveDirection;
                    Velocity = moveDirection * Speed * deltaTime;
                    Translate(Velocity);
                }
                else
                {
                    for (int i = 0; i < Scene.Actors.Length; i++)
                        if (Scene.Actors[i] is Wall)
                        {
                            Wall checkWall = (Wall)Scene.Actors[i];
                            checkWall.CheckMovement(this);
                        }
                    Velocity = Forward * Speed * deltaTime;
                    Translate(Velocity);
                }
            }

            if (WorldPosition.Y < 0.5f && WorldPosition.Y != 0)
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

            if (!IsActorGrounded)
                Acceleration += new Vector3(0, -0.00981f, 0);
        }

        public override void Draw()
        {
            System.Numerics.Vector3 position = new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z);

            if (Raylib.IsKeyDown(KeyboardKey.KEY_TAB))
            {
                if (Raylib.IsKeyDown(KeyboardKey.KEY_TAB))
                {
                    System.Numerics.Vector3 endPos = new System.Numerics.Vector3(WorldPosition.X + Forward.X * 10, WorldPosition.Y + Forward.Y * 10, WorldPosition.Z + Forward.Z * 10);
                    System.Numerics.Vector3 up = new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y + 10, WorldPosition.Z);
                    System.Numerics.Vector3 left = new System.Numerics.Vector3(WorldPosition.X + 10, WorldPosition.Y, WorldPosition.Z);
                    System.Numerics.Vector3 right = new System.Numerics.Vector3(WorldPosition.X - 10, WorldPosition.Y, WorldPosition.Z);
                    System.Numerics.Vector3 ahead = new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z + 10);
                    System.Numerics.Vector3 behind = new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z - 10);
                    Raylib.DrawLine3D(position, endPos, Color.RED);
                    Raylib.DrawLine3D(position, up, Color.RED);
                    Raylib.DrawLine3D(position, left, Color.RED);
                    Raylib.DrawLine3D(position, right, Color.RED);
                    Raylib.DrawLine3D(position, ahead, Color.RED);
                    Raylib.DrawLine3D(position, behind, Color.RED);
                }
            }
                base.Draw();
        }

        public override void OnCollision(Actor actor)
        {

        }

        /// <summary>
        /// Gets the target if this Enemy is tagged
        /// </summary>
        /// <returns>If the Enemy should approach the target</returns>
        public bool GetTargetOffense()
        {
            //Gets a the closest target that can be tagged
            for (int i = 0; i < SceneManager.Allies.Length; i++)
                if (!SceneManager.Allies[i].IsTagger)
                    if (_target == null)
                        _target = SceneManager.Allies[i];
                    else if (Vector3.Distance(_target.WorldPosition, WorldPosition) > Vector3.Distance(SceneManager.Allies[i].WorldPosition, WorldPosition))
                        _target = SceneManager.Allies[i];

            //If there is a target
            if (_target != null)
            {
                //Check the direction of the target
                Vector3 directionOfTarget = (WorldPosition - _target.WorldPosition).Normalized;

                //Return if the Enemy is facing the target, and if they are close enough to the target
                return (Vector3.GetRadian(directionOfTarget, Forward) > 2 & Vector3.Distance(_target.WorldPosition, WorldPosition) < 200);
            }

            //Return false by default
            return false;
        }

        /// <summary>
        /// Gets the closet target that can tag them
        /// </summary>
        /// <returns>If the Enemy is in danger of being tagged</returns>
        public bool GetTargetDefense()
        {
            //Gets a the closest target that can be tagged
            for (int i = 0; i < SceneManager.Allies.Length; i++)
                if (SceneManager.Allies[i].IsTagger)
                    if (_target == null)
                        _target = SceneManager.Allies[i];
                    else if (Vector3.Distance(_target.WorldPosition, WorldPosition) > Vector3.Distance(SceneManager.Allies[i].WorldPosition, WorldPosition))
                        _target = SceneManager.Allies[i];

            //If there is a target
            if (_target != null)
            {
                //Check the direction of the target
                Vector3 directionOfTarget = (WorldPosition - _target.WorldPosition).Normalized;

                //Return if the Ally is facing the target, and if they are close enough to the target
                return (Vector3.GetRadian(directionOfTarget, Forward) > 2 & Vector3.Distance(_target.WorldPosition, WorldPosition) < 200);
            }

            //Return false by default
            return false;
        }
    }
}

