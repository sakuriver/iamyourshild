using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace IamYourShild
{
    abstract class GameObject
    {
        protected GameScene owner;
        protected Dictionary<String, Texture2D> textureList;
        protected Color color;
        protected Vector2 position;
        protected string drawKeyword;
        protected int heihgt, width;

        public Rectangle rectData { get { return new Rectangle((int)position.X, (int)position.Y, width, heihgt);  }
            set { position = new Vector2(value.X, value.Y); width = value.Width; heihgt = value.Height; }
        }

        public virtual void LoadContent(List<String> assetCodeList) { }
        public virtual void Update(GameMainState gamestate) { }
        public virtual void Draw(SpriteBatch sprite) { }
        public virtual void DrawRect(SpriteBatch sproite) { }
    }
}
