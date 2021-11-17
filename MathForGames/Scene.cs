﻿using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace MathForGames
{
    class Scene
    {
        /// <summary>
        /// An Array containing all actors in a scene
        /// </summary>
        private static Actor[] _actors;
        private static UIText[] _UIElements;
        private string _name;

        public static Actor[] Actors
        {
            get { return _actors; }
        }

        public string Name
        {
            get { return _name; }
        }
        public Scene(string name)
        {
            _name = name;
            _actors = new Actor[0];
            _UIElements = new UIText[0];
        }

        /// <summary>
        /// Calls Start for every actor in the scene
        /// </summary>
        public virtual void Start()
        {

        }

        /// <summary>
        /// Updates the actors in the scene
        /// Calls start for an actor if it hasn't already been called
        /// </summary>
        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                {
                    _actors[i].Start();
                    for (int c = 0; c < _actors[i].Children.Length; c++)
                    {
                        AddActor(_actors[i].Children[c]);
                    }
                }

                _actors[i].Update(deltaTime);

                //Checks for collision
                for (int j = 0; j < _actors.Length; j++)
                    if (_actors[i].CheckCollision(_actors[j]) && i != j)
                        _actors[i].OnCollision(_actors[j]);
            }
        }

        public virtual void UpdateUI(float deltaTime)
        {
            for (int i = 0; i < _UIElements.Length; i++)
            {
                if (!_UIElements[i].Started)
                    _UIElements[i].Start();

                _UIElements[i].Update(deltaTime);
            }
        }

        /// <summary>
        /// Draws every actor in the scene
        /// </summary>
        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Draw();
        }

        public virtual void DrawUI()
        {
            for (int i = 0; i < _UIElements.Length; i++)
                _UIElements[i].Draw();
        }

        public virtual void End()
        {

        }

        /// <summary>
        /// Adds an actor to the scenes list of actors
        /// </summary>
        /// <param name="actor"></param>
        public void AddActor(Actor actor)
        {
            //Creates a temp array larger than the original
            Actor[] tempArray = new Actor[_actors.Length + 1];

            //Copies all values from the orginal array into the temp array
            for (int i = 0; i < _actors.Length; i++)
                tempArray[i] = _actors[i];
            //Adds the new actor to the end of the new array
            tempArray[_actors.Length] = actor;

            Actor[] childArray = new Actor[tempArray.Length + actor.Children.Length];
            for (int i = 0; i < tempArray.Length; i++)
                childArray[i] = tempArray[i];

            for (int i = 0; i < actor.Children.Length; i++)
                childArray[tempArray.Length + i] = actor.Children[i];

            //Merges the arrays
            _actors = childArray;
        }

        /// <summary>
        /// Adds an UI to the scenes list of UI Elements
        /// </summary>
        /// <param name="actor"></param>
        public void AddUIElement(UIText UI)
        {
            //Creates a temp array larger than the original
            UIText[] tempArray = new UIText[_UIElements.Length + 1];

            //Copies all values from the orginal array into the temp array
            for (int i = 0; i < _UIElements.Length; i++)
                tempArray[i] = _UIElements[i];
            //Adds the new actor to the end of the new array
            tempArray[_UIElements.Length] = UI;

            //Merges the arrays
            _UIElements = tempArray;
        }

        /// <summary>
        /// Removes the actor from the scene
        /// </summary>
        /// <param name="actor">The actor to remove</param>
        /// <returns>If the removal was successful</returns>
        public bool RemoveActor(Actor actor)
        {
            //Creates a variable to store if the removal was successful
            bool actorRemoved = false;

            //Creates a new rray that is smaller than the original
            Actor[] tempArray = new Actor[_actors.Length - 1];

            //Copies all values from the orginal array into the temp array unless it is the removed actor
            int j = 0;
            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i] != actor)
                {
                    tempArray[j] = _actors[i];
                    j++;
                }
                else
                    actorRemoved = true;
            }

            //Merges the arrays
<<<<<<< HEAD
            if (actorRemoved)
            {
                Actor[] childArray = new Actor[tempArray.Length - actor.Children.Length];
                j = 0;
                for (int i = 0; i < tempArray.Length; i++)
                {
                        if (tempArray[i].Parent != actor)
                        {
                            childArray[j] = tempArray[i];
                            j++;
                        }
                }
                _actors = childArray;
            }
=======
            if (actorRemoved)
            {
                Actor[] childArray = new Actor[tempArray.Length - actor.Children.Length];
                j = 0;
                for (int i = 0; i < tempArray.Length; i++)
                {
                    for(int c = 0;c<actor.Children.Length;c++)
                        if (tempArray[i] != actor.Children[c])
                        {
                            childArray[j] = tempArray[i];
                            j++;
                        }
                }
                _actors = childArray;
            }
                

>>>>>>> RayLib3D
            return actorRemoved;
        }

        /// <summary>
        /// Removes the actor from the scene
        /// </summary>
        /// <param name="actor">The actor to remove</param>
        /// <returns>If the removal was successful</returns>
        public bool RemoveUIElement(UIText actor)
        {
            //Creates a variable to store if the removal was successful
            bool actorRemoved = false;

            //Creates a new rray that is smaller than the original
            UIText[] tempArray = new UIText[_UIElements.Length - 1];

            //Copies all values from the orginal array into the temp array unless it is the removed actor
            int j = 0;
            for (int i = 0; i < _UIElements.Length; i++)
            {
                if (_UIElements[i] != actor)
                {
                    tempArray[j] = _UIElements[i];
                    j++;
                }
                else
                    actorRemoved = true;
            }

            if (actorRemoved)
            {
                //Merges the Arrays
                _UIElements = tempArray;
            }
            return actorRemoved;
        }


    }
}
