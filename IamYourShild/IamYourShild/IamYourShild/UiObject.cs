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
    class UiObject : GameObject
    {
        public Vector2 Pos 
        {
            get { return this.position; }
            set { this.position = value; }
        }

        private string set_off_name;
        private string set_on_name;
        private string drawcheckKeyword;

        public UiObject(GameScene owner, Color defaultColor, Vector2 position, string drawKeyword)
        {
            this.owner = owner;
            this.textureList = new Dictionary<string,Texture2D>();
            this.color = defaultColor;
            this.position = position;
            this.drawKeyword = drawKeyword;
        }

        public override void LoadContent(List<String> assetCodeList)
        {
            foreach (String assetCode in assetCodeList) 
            {
                textureList.Add(assetCode, this.owner.Content.Load<Texture2D>("Graphic\\UI\\" + assetCode));
            }
        }

        public void Update()
        {
        }

        public override void Draw(SpriteBatch sprite) 
        {
           
            sprite.Draw(this.textureList[this.drawKeyword], this.position, this.color);
 
        }

        public override void DrawRect(SpriteBatch sprite)
        {
            sprite.Draw(this.textureList[this.drawKeyword], this.rectData, this.color);
        }

        public Texture2D drawTexture()
        {
            return this.textureList[this.drawKeyword];
        }


        public void checkKeyWord(Vector2 checkPos)
        {
            this.drawcheckKeyword = this.drawKeyword;
            if (checkPos.X >= this.rectData.Left && this.rectData.Right >= checkPos.X && checkPos.Y >= this.rectData.Top
                && this.rectData.Bottom >= checkPos.Y)
            {
                this.drawKeyword = this.set_on_name;
            }
            else
            {
                this.drawKeyword = this.set_off_name;
            }

            if (this.drawcheckKeyword == this.set_off_name && this.drawKeyword == this.set_on_name && !(this.owner.Owner.CurrentState == "Title" && !((TitleScene)this.owner).startCheckCount()))
            {
                this.owner.Owner.selectSoundEffect("select").Play();
            }
        
        }

        public void setButtonKeyWord(string set_on_keyword, string set_off_keyword) 
        {
            this.set_off_name = set_off_keyword;
            this.set_on_name = set_on_keyword;
        }

        public Color Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public string DrawKeyWord
        {
            set { this.drawKeyword = value; }
        }
    }
}
