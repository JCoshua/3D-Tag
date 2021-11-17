﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MathLibrary;
using System.Diagnostics;
using Raylib_cs;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex;
        private static Scene[] _scenes = new Scene[0];
        private Stopwatch _stopwatch = new Stopwatch();
        private static Camera _camera = new Camera(new Camera3D());

        public static Camera Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }

        /// <summary>
        /// Called to begin the application
        /// </summary>
        public void Run()
        {
            //Call Start for the entire application
            Start();

            float currentTime;
            float lastTime = 0;
            float deltaTime;
            //Loop until the application is told to close
            while(!_applicationShouldClose && !Raylib.WindowShouldClose())
            {
                //Gets how mych time has passed since application started
                currentTime = _stopwatch.ElapsedMilliseconds / 1000.0f;

                //Set delta time to be the difference in time from the last time recorded to the current time.
                deltaTime = currentTime - lastTime;

                //Updates the application
                Update(deltaTime);
                //Draw all items
                Draw();

                //Sets the last time recorded to be the current time
                lastTime = currentTime;
            }

            //Calls end for the entire application
            End();
        }

        /// <summary>
        /// Initializes that Camera
        /// </summary>
        private void InitializeCamera()
        {
            //Camera's Position
            //Camera's Focus Point
            //Camera's Up Vector (rotation towards target)
            //The Field of View on the Y Axis (Zoom)
            //The Camera's mode type
        }
        /// <summary>
        /// Called when the application starts
        /// </summary>
        private void Start()
        {
            //Begins the stopwatch
            _stopwatch.Start();

            //Create a window using RayLib
            Raylib.InitWindow(800, 450, "Math For Games");
            Raylib.SetTargetFPS(60);
            _camera = new Camera(new Camera3D(), new System.Numerics.Vector3(0, 10, 10), new System.Numerics.Vector3(0, 0, 0), new System.Numerics.Vector3(0, 1, 0), 45, CameraProjection.CAMERA_PERSPECTIVE);

            Scene scene = new Scene("Arena");
            AddScene(scene);

            SceneManager.Start();
        }

        /// <summary>
        /// Called everytime the game loops
        /// </summary>
        private void Update(float deltaTime)
        {
            SceneManager.Update(deltaTime);
            _camera.Update(deltaTime);
        }

        /// <summary>
        /// Called every time the game loops to update visuals
        /// </summary>
        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.BeginMode3D(_camera.Camera3D);

            Raylib.ClearBackground(Color.BEIGE);
            Raylib.DrawGrid(500, 50);

            //Adds all actor icons to buffer
            SceneManager.Draw();

            Raylib.EndMode3D();

            SceneManager.DrawUI();
            Raylib.EndDrawing();
        }

        /// <summary>
        /// Called when the appliication Exits
        /// </summary>
        private void End()
        {
            SceneManager.End();
            Raylib.CloseWindow(); 
        }

        /// <summary>
        /// Adds a scene to the engine's scene array
        /// </summary>
        /// <param name="scene">That scene to be added</param>
        /// <returns>The Index of the added scene</returns>
        public int AddScene(Scene scene)
        {
            //Creates a new temporary array
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy all values from the old array into the new array
            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i]; 
            }

            //Sets the last index to bee the new scene
            tempArray[_scenes.Length] = scene;

            //Sets the original array to be the new array
            _scenes = tempArray;

            //Retrun the last index
            return _scenes.Length - 1;
        }

        /// <summary>
        /// Gets the next key pressed in the input stream
        /// </summary>
        /// <returns>The key that was pressed (if any)</returns>
        public static ConsoleKey GetNextKey()
        {
            //Returns the current key being pressed
            if(Console.KeyAvailable)
            return Console.ReadKey(true).Key;

            //If there is no key being pressed
            return 0;
        }

        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }

        public static Scene GetCurrentScene()
        {
            return _scenes[_currentSceneIndex];
        }
    }
}
