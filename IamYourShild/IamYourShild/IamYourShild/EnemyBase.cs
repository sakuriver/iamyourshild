using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IamYourShild
{
    abstract class EnemyBase : GameObject
    {

        protected int enemyLife;
        protected int enemyInitLife;
        protected string enemyTypeCode;
        protected int animCount = 0;
        protected bool animFlg = false;
        protected Vector2 moveSpeed;

        public EnemyBase(GameScene owner, Color defaultColor, Rectangle rectData, string drawKeyword, int enemyLife, Vector2 targetPosition)
        {
            this.owner = owner;
            this.textureList = new Dictionary<string, Texture2D>();
            this.color = defaultColor;
            this.rectData = rectData;
            this.drawKeyword = drawKeyword;
            this.enemyLife = enemyLife;
            this.enemyInitLife = this.enemyLife;
            this.animCount = 0;
            this.animFlg = false;
            double rotation = Math.Atan2(targetPosition.Y - this.rectData.Center.Y, targetPosition.X - this.rectData.Center.X);
            this.moveSpeed = new Vector2(float.Parse((5 * Math.Cos(rotation)).ToString()), float.Parse((5*Math.Sin(rotation)).ToString()));
        }

        public override void LoadContent(List<string> assetCodeList)
        {
            foreach (string assetCode in assetCodeList) 
            {
                textureList.Add(assetCode, this.owner.Content.Load<Texture2D>("Graphic\\Character\\" + assetCode));
            }
        }

        public override void Update(GameMainState gameMainState)
        {
            this.position.X += this.moveSpeed.X;
            this.position.Y += this.moveSpeed.Y;
            base.Update(gameMainState);
        }

        private void animUpdate()
        {
        }

        public void Draw(SpriteBatch sprite) 
        {
            sprite.Draw(this.textureList[this.drawKeyword], this.position, this.color);
 
        }

        public virtual void AfterUpdate(int afterCount) 
        {
            
        }

        public int EnemyLife 
        {
            get { return this.enemyLife; }
            set { this.enemyLife = value; }
        }

        public string UfoTypeCode
        {
            get { return this.enemyTypeCode; }
        }

        public virtual List<string> getEnemyAssetList() { return new List<string>(); }

    }
}
