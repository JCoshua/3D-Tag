using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class SceneManager
    {
        private static Scene _currentScene;
        private static Ally[] _allies = new Ally[0];
        private static Enemy[] _enemies = new Enemy[0];
        private static int taggers = 3;

        public static Scene CurrentScene
        {
            get { return _currentScene; }
            set { _currentScene = value; }
        }

        public static Ally[] Allies
        {
            get { return _allies; }
            set { _allies = value; }
        }

        public static Enemy[] Enemies
        {
            get { return _enemies; }
            set { _enemies = value; }
        }
        public static void Start()
        {
            _currentScene = Engine.GetCurrentScene();
            StartCurrentScene();
            _currentScene.Start();
        }

        public static void Update(float deltaTime)
        {
            if (_currentScene != Engine.GetCurrentScene())
            {
                _currentScene = Engine.GetCurrentScene();
                StartCurrentScene();
                _currentScene.Start();
            }

            _currentScene.Update(deltaTime);
            _currentScene.UpdateUI(deltaTime);
        }

        public static void Draw()
        {
            _currentScene.Draw();
        }

        public static void DrawUI()
        {
            _currentScene.DrawUI();
        }

        public static void End()
        {
            _currentScene.End();
        }

        private static void StartCurrentScene()
        {
            if (_currentScene.Name == "Arena")
                IntializeArena();
        }

        private static void IntializeArena()
        {
            Player player = new Player(10, 0.5f, 10, 20, "Player");
            AddAlly(player);
            _currentScene.AddActor(player);

            Actor camera = new Actor(0, 0, 0);
            camera.Collider = new SphereCollider(0.01f, camera);
            player.AddChild(camera);
            Engine.Camera.CameraTarget = camera;

            Ally ally = new Ally(10, 0.5f, 15, 20, "James");
            AddAlly(ally);
            _currentScene.AddActor(ally);

            Enemy enemy = new Enemy(-25, 1, 25, 20, "Enemy");
            AddEnemy(enemy);
            _currentScene.AddActor(enemy);

            Enemy enemy2 = new Enemy(-25, 1, 15, 20, "Enemy");
            AddEnemy(enemy2);
            _currentScene.AddActor(enemy2);

            Actor floor = new Actor(0, -0.251f, 0, "Floor", Shape.CUBE);
            floor.SetScale(500, 0.5f, 500);
            floor.Collider = new AABBCollider(floor);
            floor.SetColor(Color.DARKGREEN);
            _currentScene.AddActor(floor);

            Wall wall = new Wall(-1, 0, 2.1f, 100, 10, 10, CurrentScene);

            Wall wall2 = new Wall(1.65f, 0, 0.6f, 10, 10, 100, CurrentScene);

            while (taggers > 0)
            {
                for (int i = 0; i < Enemies.Length; i++)
                {
                    int randomTag = new Random().Next(0, 2);
                    if(randomTag == 1 && !Enemies[i].IsTagger && taggers > 0)
                    {
                        Enemies[i].IsTagger = true;
                        taggers--;
                    }
                }
                for (int i = 0; i < Allies.Length; i++)
                {
                    int randomTag = new Random().Next(0, 2);
                    if (randomTag == 1 && !Allies[i].IsTagger && taggers > 0)
                    {
                        Allies[i].IsTagger = true;
                        taggers--;
                    }
                }
            }

        }

        /// <summary>
        /// Adds an actor to the scenes list of actors
        /// </summary>
        /// <param name="ally"></param>
        public static void AddAlly(Ally ally)
        {
            //Creates a temp array larger than the original
            Ally[] tempArray = new Ally[_allies.Length + 1];

            //Copies all values from the orginal array into the temp array
            for (int i = 0; i < _allies.Length; i++)
                tempArray[i] = _allies[i];
            //Adds the new actor to the end of the new array
            tempArray[_allies.Length] = ally;

            //Merges the arrays
            _allies = tempArray;
        }

        /// <summary>
        /// Adds an actor to the scenes list of actors
        /// </summary>
        /// <param name="enemy">The Enemy to Remove</param>
        public static void AddEnemy(Enemy enemy)
        {
            //Creates a temp array larger than the original
            Enemy[] tempArray = new Enemy[_enemies.Length + 1];

            //Copies all values from the orginal array into the temp array
            for (int i = 0; i < _enemies.Length; i++)
                tempArray[i] = _enemies[i];
            //Adds the new actor to the end of the new array
            tempArray[_enemies.Length] = enemy;

            //Merges the arrays
            _enemies = tempArray;
        }

        /// <summary>
        /// Removes the child from the Actor
        /// </summary>
        /// <param name="ally">The child to remove</param>
        /// <returns>If the removal was successful</returns>
        public static bool RemoveAlly(Actor ally)
        {
            //Creates a variable to store if the removal was successful
            bool actorRemoved = false;

            //Creates a new rray that is smaller than the original
            Ally[] tempArray = new Ally[_allies.Length - 1];

            //Copies all values from the orginal array into the temp array unless it is the removed actor
            int j = 0;
            for (int i = 0; i < _allies.Length; i++)
            {
                if (_allies[i] != ally)
                {
                    tempArray[j] = _allies[i];
                    j++;
                }
                else
                    actorRemoved = true;
            }

            //Merges the arrays
            if (actorRemoved)
            {
                _allies = tempArray;
            }

            return actorRemoved;
        }

        /// <summary>
        /// Removes the child from the Actor
        /// </summary>
        /// <param name="ally">The child to remove</param>
        /// <returns>If the removal was successful</returns>
        public static bool RemoveEnemy(Enemy enemy)
        {
            //Creates a variable to store if the removal was successful
            bool actorRemoved = false;

            //Creates a new rray that is smaller than the original
            Enemy[] tempArray = new Enemy[_enemies.Length - 1];

            //Copies all values from the orginal array into the temp array unless it is the removed actor
            int j = 0;
            for (int i = 0; i < _enemies.Length; i++)
            {
                if (_enemies[i] != enemy)
                {
                    tempArray[j] = _enemies[i];
                    j++;
                }
                else
                    actorRemoved = true;
            }

            //Merges the arrays
            if (actorRemoved)
            {
                _enemies = tempArray;
            }

            return actorRemoved;
        }
    }
}
