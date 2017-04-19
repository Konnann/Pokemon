﻿namespace Pokemon.Client.Screens
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Pokemon.Client.Interfaces;
    using Microsoft.Xna.Framework.Input;
    using Core;
    using Content;
    using Core.Engines;
    using Textures;
    using UI_Elements.Windows;

    public class WorldScreen : IGameScreen
    {
        private bool exitGame;

        private readonly GameScreenManager screenManager;
        public Window window;
        public WindowHandler windowHandler;
        public bool IsPaused { get; private set; }

        public WorldScreen(GameScreenManager screenManager)
        {
            this.screenManager = screenManager;
        }

        public void Initialize(ContentManager content)
        {
            WorldEngine.PopulateWildPokemon();
            WorldEngine.InitializeDrawableObjects();
            WorldEngine.InitializeUpdatableObjects();
            windowHandler = WorldEngine.WindowHandler;
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }

        public void Update(GameTime gameTime)
        {

            foreach (var p in WorldEngine.WildPokemon)
            {

                //TODO: fix collision bug - too far away but returns true
                if (Collision.CheckForCollisionBetweenCollidables(SessionEngine.Trainer, p))
                {
                    p.IsEncountered = true;
                    SessionEngine.Trainer.IsSurprised = true;

                }
            }

            foreach (IUpdatable u in WorldEngine.UpdatableObjects)
            {
                u.Update(gameTime);
            }

            windowHandler.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(WorldEngine.background, new Vector2(0, 0), new Rectangle(0, 0, SessionEngine.WindowWidth, SessionEngine.WindowHeight), Color.White);

            foreach (Interfaces.IDrawable d in WorldEngine.DrawableObjects)
            {
                d.Draw(spriteBatch);                
            }

            //Debug
            windowHandler?.Draw(spriteBatch);

        }

        public void HandleInput(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Escape))
            {
                exitGame = true;
            }

            if (keyboard.IsKeyDown(Keys.B))
            {
                screenManager.PopScreen();
            }
        }

        public void Dispose()
        {

        }
    }
}
