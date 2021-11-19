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
        private bool _hasPowerUp = false;
        private float powerUpTimer = 0f;

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

        /// <summary>
        /// The base Enemy Collider
        /// </summary>
        /// <param name="speed">The Spped of the enemy</param>
        public Enemy(float x, float y, float z, float speed, string name = "Actor")
            : base(x, y, z, name)
        {
            _speed = speed;
        }

        /// <summary>
        /// Initializes the Enemy
        /// </summary>
        public override void Start()
        {
            Actor head = new Actor(0, 0.85f, 0, "Head", Shape.SPHERE);
            head.SetScale(0.5f, 0.5f, 0.5f);
            head.Collider = new SphereCollider(head);
            head.SetColor(255, 100, 100, 255);
            AddChild(head);

            Actor body = new Actor(0, 0, 0, "Body", Shape.CUBE);
            body.SetScale(0.75f, 1, 0.75f);
            Collider = new AABBCollider(this);
            body.SetColor(255, 10, 10, 255);
            AddChild(body);

            base.Start();
        }

        /// <summary>
        /// Updates the Enemy
        /// </summary>
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            //If they are to far away from the play area
            if (WorldPosition.X >= 145 || WorldPosition.Z >= 145 || WorldPosition.X <= -145 || WorldPosition.Z <= -145)
                //Return them to the center of the map
                SetTranslation(0, 0.5f, 0);

            //If the Enemy is a tagger
            if (IsTagger)
            {
                //If a target is close enough to tag
                if (GetTargetOffense())
                {
                    //Chase them
                    Vector3 moveDirection = (_target.WorldPosition - WorldPosition).Normalized;
                    Forward = moveDirection;
                    Velocity = moveDirection * Speed * deltaTime;
                    Translate(Velocity);
                }
                else
                {
                    //Wander and check if running into wall
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
            //If the Enemy is not a Tagger
            else if (!IsTagger)
            {
                //Check if they are in danger of being tagged
                if (GetTargetDefense())
                {
                    //Check if running into wall
                    for (int i = 0; i < Scene.Actors.Length; i++)
                        if (Scene.Actors[i] is Wall)
                        {
                            Wall checkWall = (Wall)Scene.Actors[i];
                            checkWall.CheckMovement(this);
                        }
                    //Flee from ally
                    Vector3 moveDirection = new Vector3(-(_target.WorldPosition.X - WorldPosition.X), 0, -(_target.WorldPosition.Z - WorldPosition.Z)).Normalized;
                    Forward = moveDirection;
                    Velocity = moveDirection * Speed * deltaTime;
                    Translate(Velocity);
                }
                else
                {
                    //Wander and check if running into wall
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

            //If the Enemy Picked up a power up
            if (_hasPowerUp)
            {
                //Check how long they have been powered up for
                powerUpTimer += deltaTime;
                //If an enemy has powered up for too long
                if (powerUpTimer >= 20)
                {
                    //Reset them
                    SetScale(1, 1, 1);
                    Children[0].SetScale(0.5f, 0.5f, 0.5f);
                    Children[0].SetColor(255, 100, 100, 255);
                    Children[1].SetScale(0.75f, 1, 0.75f);
                    Children[1].SetColor(255, 10, 10, 255);
                    Speed = 20;
                    powerUpTimer = 0;
                    _hasPowerUp = false;
                }
            }

            //If the Actor falls below the Ground
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

            //If enemy in in the Air
            if (!IsActorGrounded)
                //Apply Gravity
                Acceleration -= new Vector3(0, 0.00981f, 0);
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

        public override void OnCollision(Actor actor)
        {
            if (actor is Ally && WorldPosition.Y != 0 && !IsTagger)
            {
                SceneManager.CurrentScene.RemoveActor(this);
                SceneManager.RemoveEnemy(this);
            }
            else if (actor is Wall && WorldPosition.Y != 0)
            {
                Translate(-Forward.X * 2, 0, -Forward.Z * 2);
                Forward = new Vector3(-Forward.X, 0, -Forward.Z);
            }

            else if (actor is PowerUp && WorldPosition.Y != 0)
                _hasPowerUp = true;
        }
    }
}

