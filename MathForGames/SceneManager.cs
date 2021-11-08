using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class SceneManager
    {
        private Scene _currentScene;

        public Scene CurrentScene
        {
            get { return _currentScene; }
            set { _currentScene = value; }
        }

        public void Start()
        {
            _currentScene = Engine.GetCurrentScene();
            StartCurrentScene();
            _currentScene.Start();
        }

        public void Update(float deltaTime)
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

        public void Draw()
        {
            _currentScene.Draw();
            _currentScene.DrawUI();
        }

        public void End()
        {
            _currentScene.End();
        }

        private void StartCurrentScene()
        {
            if (_currentScene.Name == "Arena")
                IntializeArena();
        }

        private void IntializeArena()
        {
            Player player = new Player(0, 1, 0, 50, "Player");
            Actor head = new Actor(0, 2, 0, "Head", Shape.SPHERE);
            Actor body = new Actor(0, 1, 0, "Body", Shape.CUBE);
            Actor camera = new Actor(0, 0, 0);

            player.SetScale(1, 1, 1);
            head.SetScale(0.5f, 0.5f, 0.5f);
            head.Collider = new SphereCollider(head);
            body.SetScale(0.5f, 1, 0.75f);
            body.Collider = new AABBCollider(0.5f, 1, 0.75f, body);
            head.SetColor(255, 100, 100, 255);
            body.SetColor(10, 10, 255, 255);
            player.AddChild(head);
            player.AddChild(body);
            player.AddChild(camera);
            Engine.Camera.CameraTarget = camera;
            _currentScene.AddActor(player);

        }
    }
}
