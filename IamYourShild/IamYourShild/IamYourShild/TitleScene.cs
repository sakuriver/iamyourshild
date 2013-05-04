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
    /// タイトルシーンクラス
    /// </summary>
    class TitleScene : GameScene
    {

        private Dictionary<string, UiObject> titleBackObjectList;
        private int checkCount;
        private string drawBackKeyword;

        public TitleScene(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SceneManager owner)
            : base(game, graphics, spriteBatch, owner)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = game.Content;
            this.spriteBatch = spriteBatch;
            this.owner = owner;
            this.param = new Parameter();
            this.checkCount = 0;
            this.drawBackKeyword = "kawaz";
        }

        public override void LoadContent()
        {
            // タイトル画像の読込み
            titleBackObjectList = new Dictionary<string, UiObject>();
            titleBackObjectList.Add("game", new UiObject(this, Color.White, Vector2.Zero, "iys_title_back"));
            titleBackObjectList.Add("kawaz", new UiObject(this, Color.White, new Vector2(80, 210), "kawaz"));
            titleBackObjectList.Add("start", new UiObject(this, Color.White, new Vector2(300, 400), "start"));
            titleBackObjectList.Add("highscore", new UiObject(this, Color.White, new Vector2(280, 450), "highscore"));
            titleBackObjectList.Add("credit", new UiObject(this, Color.White, new Vector2(280, 510), "credit"));
            List<string> titleList = new List<string>();
            List<string> kawazList = new List<string>();
            List<string> startList = new List<string>();
            List<string> highscoreList = new List<string>();
            List<string> creditList = new List<string>();
            titleList.Add("iys_title_back");
            kawazList.Add("kawaz");
            startList.Add("start");
            startList.Add("start-o");
            highscoreList.Add("highscore");
            highscoreList.Add("highscore-o");
            creditList.Add("credit");
            creditList.Add("credit-o");
            titleBackObjectList["kawaz"].LoadContent(kawazList);
            titleBackObjectList["game"].LoadContent(titleList);
            titleBackObjectList["start"].LoadContent(startList);
            titleBackObjectList["start"].setButtonKeyWord(startList[1], startList[0]);
            titleBackObjectList["start"].rectData = new Rectangle(titleBackObjectList["start"].rectData.X, titleBackObjectList["start"].rectData.Y, 173, 46);
            titleBackObjectList["highscore"].LoadContent(highscoreList);
            titleBackObjectList["highscore"].setButtonKeyWord(highscoreList[1], highscoreList[0]);
            titleBackObjectList["highscore"].rectData = new Rectangle(titleBackObjectList["highscore"].rectData.X, titleBackObjectList["highscore"].rectData.Y, 224, 45);
            titleBackObjectList["credit"].LoadContent(creditList);
            titleBackObjectList["credit"].setButtonKeyWord(creditList[1], creditList[0]);
            titleBackObjectList["credit"].rectData = new Rectangle(titleBackObjectList["credit"].rectData.X, titleBackObjectList["credit"].rectData.Y, 217, 46);
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
            titleBackObjectList["start"].checkKeyWord(CMouseState.Pos);
            titleBackObjectList["highscore"].checkKeyWord(CMouseState.Pos);
            titleBackObjectList["credit"].checkKeyWord(CMouseState.Pos);
            // ゲームのメイン画面へ遷移する
            if (CMouseState.LeftButtonPressing && CMouseState.InRectangle(titleBackObjectList["start"].rectData) && this.startCheckCount())
            {

                this.owner.CurrentState = "Main";
                Dictionary<string, string> setList = new Dictionary<string, string>();
                if (setList.ContainsKey("stageChange"))
                {
                    setList["stageChange"] = "TRUE";
                }
                else
                {
                    setList.Add("stageChange", "TRUE");
                }

                this.owner.currentScene.parameter.SetParameter(setList);
            }

            if (CMouseState.LeftButtonPressing && CMouseState.InRectangle(titleBackObjectList["credit"].rectData) && this.startCheckCount())
            {
                this.owner.CurrentState = "Credits";
            }

            if (CMouseState.LeftButtonPressing && CMouseState.InRectangle(titleBackObjectList["highscore"].rectData) && this.startCheckCount())
            {
                this.owner.selectSoundEffect("decide").Play();
                this.owner.CurrentState = "Ranking";
                Dictionary<string, string> setList = new Dictionary<string, string>();
                if (setList.ContainsKey("sceneCheck"))
                {
                    setList["sceneCheck"] = "true";
                }
                else 
                {
                    setList.Add("sceneCheck", "true"); 
                }
                this.owner.currentScene.parameter.SetParameter(setList);
            }

            if (this.param["titleCheck"] == "TRUE" && this.startCheckCount())
            {
                this.param["titleCheck"] = "FALSE";
            }

            if (this.owner.CurrentState != "Title")
            {
                this.owner.buttonList["exit"].rectData = new Rectangle(650, 540, this.owner.buttonList["exit"].rectData.Width, this.owner.buttonList["exit"].rectData.Height);
            }


            if (this.startCheckCount())
            {
                this.drawBackKeyword = "game";
            }
            this.checkCount++;
            

        }

        public bool startCheckCount()
        {
            return this.checkCount >= 180 ? true : false;
        }

        public override void Draw()
        {
            this.spriteBatch.Begin();
            titleBackObjectList[this.drawBackKeyword].Draw(spriteBatch);
            this.uiDraw();
            this.spriteBatch.End();
        }

        private void uiDraw()
        {
            if (this.checkCount >= 180)
            {
                titleBackObjectList["start"].DrawRect(spriteBatch);
                titleBackObjectList["highscore"].DrawRect(spriteBatch);
                titleBackObjectList["credit"].DrawRect(spriteBatch);
            }
        }
    }
}
