using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace IamYourShild
{
    /// <summary>
    /// クレジットシーンクラス
    /// </summary>
    class CreditsScene : GameScene
    {

        private Dictionary<string, UiObject> creditBackObjectList;
        private string drawBackKeyword;
        private SpriteFont spriteFont, titleFont;

        public CreditsScene(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SceneManager owner)
            : base(game, graphics, spriteBatch, owner)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = game.Content;
            this.spriteBatch = spriteBatch;
            this.owner = owner;
            this.param = new Parameter();
            this.drawBackKeyword = "iys_credit_back";
        }

        public override void LoadContent()
        {
            // タイトル画像の読込み
            this.creditBackObjectList = new Dictionary<string, UiObject>();
            this.creditBackObjectList.Add("iys_credit_back", new UiObject(this, Color.White, Vector2.Zero, "iys_credit_back"));
            List<string> creditList = new List<string>();
            creditList.Add("iys_credit_back");
            this.creditBackObjectList["iys_credit_back"].LoadContent(creditList);
         //   this.spriteFont = content.Load<SpriteFont>("CreditFont");
         //   this.titleFont = content.Load<SpriteFont>("TitleFont");
        }

        public override void unLoadContent()
        {
            base.unLoadContent();
        }

        public override void Update(GameTime gametime) 
        {
            // ゲームの終了条件をチェックします。
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.game.Exit();


            this.owner.buttonList["exit"].checkKeyWord(CMouseState.Pos);
            // ゲームのメイン画面へ遷移する
            if (CMouseState.LeftButtonPressed && CMouseState.InRectangle(this.owner.buttonList["exit"].rectData))
            {
                this.owner.selectSoundEffect("decide").Play();
                this.owner.CurrentState = "Title";
                this.owner.currentScene.parameter["titleCheck"] = "TRUE";
            }

        }

        public override void Draw()
        {
            spriteBatch.Begin();
            this.creditBackObjectList[this.drawBackKeyword].Draw(spriteBatch);
        //    this.spriteBatch.DrawString(titleFont, "提供  ゲーム制作コミュニティ  Kawaz", new Vector2(120, 80), Color.White);
        //    this.spriteBatch.DrawString(spriteFont, "■  ディレクター/プログラマー  サク・リバー", new Vector2(180, 150), Color.White);
            this.owner.buttonList["exit"].DrawRect(spriteBatch);
            
            spriteBatch.End();
        }
    }
}
