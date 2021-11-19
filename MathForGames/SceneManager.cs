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
        private static float _timeLeft = 180;
        private static bool _gameOver = false;

        /// <summary>
        /// The Current Scene
        /// </summary>
        public static Scene CurrentScene
        {
            get { return _currentScene; }
            set { _currentScene = value; }
        }

        /// <summary>
        /// The Allies Array
        /// </summary>
        public static Ally[] Allies
        {
            get { return _allies; }
            set { _allies = value; }
        }

        /// <summary>
        /// The Enemeies Array
        /// </summary>
        public static Enemy[] Enemies
        {
            get { return _enemies; }
            set { _enemies = value; }
        }

        /// <summary>
        /// The Time Left in the game
        /// </summary>
        public static float TimeLeft
        {
            get { return _timeLeft; }
            set { _timeLeft = value; }
        }

        /// <summary>
        /// True if the game has finished
        /// </summary>
        public static bool GameOver
        {
            get { return _gameOver; }
            set { _gameOver = value; }
        }

        /// <summary>
        /// Starts the First Scene
        /// </summary>
        public static void Start()
        {
            _currentScene = Engine.GetCurrentScene();
            IntializeArena();
            _currentScene.Start();
        }

        /// <summary>
        /// Updates the Current Scene
        /// </summary>
        /// <param name="deltaTime"></param>
        public static void Update(float deltaTime)
        {
            _currentScene.Update(deltaTime);
            _currentScene.UpdateUI(deltaTime);

            TimeLeft -= deltaTime;

            if (TimeLeft <= -10)
                Engine.CloseApplication();
        }

        /// <summary>
        /// Draws the Current Scene's Actors
        /// </summary>
        public static void Draw()
        {
            _currentScene.Draw();
        }

        /// <summary>
        /// Draws the Current Scene's UI
        /// </summary>
        public static void DrawUI()
        {
            _currentScene.DrawUI();
        }

        /// <summary>
        /// Ends the Current Scene
        /// </summary>
        public static void End()
        {
            _currentScene.End();
        }

        /// <summary>
        /// Intializes the Arena Scene
        /// </summary>
        private static void IntializeArena()
        {
            //Adds the Center of the Map for Camera Purposes
            Actor origin = new Actor(0, 0, 0);
            _currentScene.AddActor(origin);

            //Adds the Player to the Scene
            Player player = new Player(0, 0.5f, -5, 10, "Player");
            AddAlly(player);
            player.Collider = new AABBCollider(player);
            _currentScene.AddActor(player);

            //Initializes the camera's Target
            Actor camera = new Actor(0, 0, 0);
            camera.Collider = new SphereCollider(0.01f, camera);
            player.AddChild(camera);
            Engine.Camera.CameraTarget = camera;

            //Adds the Allies
            Ally ally = new Ally(3, 0.5f, -5, 10, "James");
            AddAlly(ally);
            ally.Collider = new AABBCollider(ally);
            _currentScene.AddActor(ally);
            Ally ally2 = new Ally(6, 0.5f, -5, 10, "Emma");
            AddAlly(ally2);
            ally2.Collider = new AABBCollider(ally2);
            _currentScene.AddActor(ally2);
            Ally ally3 = new Ally(-3, 0.5f, -5, 10, "Robin");
            AddAlly(ally3);
            ally3.Collider = new AABBCollider(ally3);
            _currentScene.AddActor(ally3);
            Ally ally4 = new Ally(-6, 0.5f, -5, 10, "Diana");
            AddAlly(ally4);
            ally4.Collider = new AABBCollider(ally4);
            _currentScene.AddActor(ally4);

            //Adds the Enemies
            Enemy enemy = new Enemy(0, 1, 5, 10, "Charles");
            AddEnemy(enemy);
            enemy.Collider = new AABBCollider(enemy);
            _currentScene.AddActor(enemy);
            Enemy enemy2 = new Enemy(3, 1, 5, 10, "Jeffery");
            AddEnemy(enemy2);
            enemy2.Collider = new AABBCollider(enemy2);
            _currentScene.AddActor(enemy2);
            Enemy enemy3 = new Enemy(6, 1, 5, 10, "Sebastion");
            AddEnemy(enemy3);
            enemy2.Collider = new AABBCollider(enemy3);
            _currentScene.AddActor(enemy3);
            Enemy enemy4 = new Enemy(-3, 1, 5, 10, "Cynthia");
            AddEnemy(enemy4);
            enemy4.Collider = new AABBCollider(enemy4);
            _currentScene.AddActor(enemy4);
            Enemy enemy5 = new Enemy(-6, 1, 5, 20, "Lodis");
            AddEnemy(enemy5);
            enemy5.Collider = new AABBCollider(enemy5);
            _currentScene.AddActor(enemy5);

            //Creates a Floor
            Actor floor = new Actor(0, -0.251f, 0, "Floor", Shape.CUBE);
            floor.SetScale(500, 0.5f, 500);
            floor.Collider = new AABBCollider(floor);
            floor.SetColor(Color.DARKGREEN);
            _currentScene.AddActor(floor);

            //Creates the Walls
            Wall westWall = new Wall(75, 0, 0, 10, 10, 300, CurrentScene);
            Wall eastWall = new Wall(-75, 0, 0, 10, 10, 300, CurrentScene);
            Wall northWall = new Wall(0, 0, 75, 300, 10, 10, CurrentScene);
            Wall southWall = new Wall(0, 0, -75, 300, 10, 10, CurrentScene);
            Wall wall1 = new Wall(1, 0, 1.2f, 5, 10, 5, CurrentScene);
            Wall wall2 = new Wall(-1, 0, 1.2f, 5, 10, 5, CurrentScene);
            Wall wall3 = new Wall(1, 0, -1.2f, 5, 10, 5, CurrentScene);
            Wall wall4 = new Wall(-1, 0, -1.2f, 5, 10, 5, CurrentScene);
            Wall wall5 = new Wall(3, 0, 3.6f, 5, 10, 5, CurrentScene);
            Wall wall6 = new Wall(3, 0, -3.6f, 5, 10, 5, CurrentScene);
            Wall wall7 = new Wall(-3, 0, 3.6f, 5, 10, 5, CurrentScene);
            Wall wall8 = new Wall(-3, 0, -3.6f, 5, 10, 5, CurrentScene);
            Wall wall9 = new Wall(0, 0, -2, 20, 10, 5, CurrentScene);
            Wall wall10 = new Wall(0, 0, 2, 20, 10, 5, CurrentScene);

            //Randomly Places powerUps on the field
            int randomX = new Random().Next(-50, 50);
            int randomZ = new Random().Next(-50, 50);
            PowerUp ScaleUp = new PowerUp(randomX, 1, randomZ, ItemType.SIZEUP);
            ScaleUp.Collider = new AABBCollider(ScaleUp);
            _currentScene.AddActor(ScaleUp);
            randomX = new Random().Next(-50, 50);
            randomZ = new Random().Next(-50, 50);
            PowerUp ScaleDown = new PowerUp(randomX, 1, randomZ, ItemType.SIZEDOWN);
            ScaleDown.Collider = new AABBCollider(ScaleDown);
            _currentScene.AddActor(ScaleDown);
            randomX = new Random().Next(-50, 50);
            randomZ = new Random().Next(-50, 50);
            PowerUp ghost = new PowerUp(randomX, 1, randomZ, ItemType.GHOST);
            ghost.Collider = new AABBCollider(ghost);
            _currentScene.AddActor(ghost);
            randomX = new Random().Next(-50, 50);
            randomZ = new Random().Next(-50, 50);
            PowerUp speedUp = new PowerUp(randomX, 1, randomZ, ItemType.SPEEDUP);
            speedUp.Collider = new AABBCollider(speedUp);
            _currentScene.AddActor(speedUp);
            randomX = new Random().Next(-50, 50);
            randomZ = new Random().Next(-50, 50);
            PowerUp speedDown = new PowerUp(randomX, 1, randomZ, ItemType.SPEEDDOWN);
            speedDown.Collider = new AABBCollider(speedDown);
            _currentScene.AddActor(speedDown);

            //Randomly chooses each teams roles
            int randomTag = new Random().Next(0, 2);
            if (randomTag == 0)
                for (int i = 0; i < Enemies.Length; i++)
                    Enemies[i].IsTagger = true;
            if (randomTag == 1)
                for (int i = 0; i < Allies.Length; i++)
                    Allies[i].IsTagger = true;
            
            //Creates the Ui
            //The UI for Timer
            UIText timer = new UIText(177, 0, 0, "Timer", Color.WHITE, 100, 40, 20, "Time: " + TimeLeft);
            _currentScene.AddUIElement(timer);

            //The UI for Displaying each Teams Roles and the Status of the Runner's Team
            if (randomTag == 0)
            {
                UIText blueTeam = new UIText(0, 0, 0, "Blue Team", Color.BLUE, 100, 20, 10, "Run!");
                UIText redTeam = new UIText(389, 0, 0, "Red Team", Color.RED, 100, 20, 10, "Tag!");
                UIText teamRemaining = new UIText(185, 220, 0, "Team Remaining", Color.BLUE, 100, 20, 10, "5 Left!");
                _currentScene.AddUIElement(blueTeam);
                _currentScene.AddUIElement(redTeam);
                _currentScene.AddUIElement(teamRemaining);
            }
            if (randomTag == 1)
            {
                UIText blueTeam = new UIText(0, 0, 0, "Blue Team", Color.BLUE, 100, 20, 10, "Tag!");
                UIText redTeam = new UIText(389, 0, 0, "Red Team", Color.RED, 100, 20, 10, "Run!");
                UIText teamRemaining = new UIText(185, 220, 0, "Team Remaining", Color.RED, 100, 20, 10, "5 Remaining!");
                _currentScene.AddUIElement(blueTeam);
                _currentScene.AddUIElement(redTeam);
                _currentScene.AddUIElement(teamRemaining);
            }
        }

        /// <summary>
        /// Adds an allies to the Scenes list of Allies
        /// </summary>
        /// <param name="ally">The Ally to add</param>
        public static void AddAlly(Ally ally)
        {
            //Creates a temp array larger than the original
            Ally[] tempArray = new Ally[_allies.Length + 1];

            //Copies all values from the orginal array into the temp array
            for (int i = 0; i < _allies.Length; i++)
                tempArray[i] = _allies[i];
            //Adds the new ally to the end of the new array
            tempArray[_allies.Length] = ally;

            //Merges the arrays
            _allies = tempArray;
        }

        /// <summary>
        /// Adds an Enemy to the Scenes list of Enemies
        /// </summary>
        /// <param name="enemy">The Enemy to add</param>
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
        /// Removes an Ally from the Allies Array
        /// </summary>
        /// <param name="ally">The ally to remove</param>
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
                _allies = tempArray;

            return actorRemoved;
        }

        /// <summary>
        /// Removes the enemy from the Enemies Array
        /// </summary>
        /// <param name="enemy">The Enemy to remove</param>
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
                _enemies = tempArray;
            

            return actorRemoved;
        }
    }
}
