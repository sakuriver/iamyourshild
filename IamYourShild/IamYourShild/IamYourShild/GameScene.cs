using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace IamYourShild
{
    abstract class GameScene
    {
        protected SceneManager owner;
        protected Game game;
        protected GraphicsDeviceManager graphics;
        protected ContentManager content;
        protected SpriteBatch spriteBatch;
        protected Parameter param;

        public GameScene(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SceneManager owner)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = game.Content;
            this.spriteBatch = spriteBatch;
            this.owner = owner;
        }

        public SceneManager Owner
        {
            get { return this.owner;}
        }

        public ContentManager Content
        {
            get { return this.content; }
        }

        public Parameter parameter
        {
            get { return param; }
        }

        public virtual void LoadContent()
        { 
        }

        public virtual void unLoadContent()
        { 
        }

        public virtual void Update(GameTime gameTime)
        { 
        }

        public virtual void Draw() 
        { }

    }
}
