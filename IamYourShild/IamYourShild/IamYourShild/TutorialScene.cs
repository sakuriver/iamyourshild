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
    class TutorialScene : GameScene
    {

        private Dictionary<string, UiObject> tutorialBackObjectList;
        private int checkCount;
        private string drawBackKeyword;

        public TutorialScene(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SceneManager owner)
            : base(game, graphics, spriteBatch, owner)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = game.Content;
            this.spriteBatch = spriteBatch;
            this.owner = owner;
            this.param = new Parameter();
            this.checkCount = 0;
            this.drawBackKeyword = "lwbg_tutorial_back";
        }

        public override void LoadContent()
        {
 
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

            this.owner.buttonList["next"].checkKeyWord(CMouseState.Pos);
            this.owner.buttonList["back"].checkKeyWord(CMouseState.Pos);
            this.owner.buttonList["exit"].checkKeyWord(CMouseState.Pos);
            if (CMouseState.LeftButtonPressed && CMouseState.InRectangle(this.owner.buttonList["back"].rectData))
            {
                this.owner.selectSoundEffect("decide").Play();
                this.tutorialBackObjectList[this.drawBackKeyword].DrawKeyWord = "lwbg_tutorial_back_first";
            }
            if (CMouseState.LeftButtonPressed && CMouseState.InRectangle(this.owner.buttonList["next"].rectData))
            {
                this.owner.selectSoundEffect("decide").Play();
                this.tutorialBackObjectList[this.drawBackKeyword].DrawKeyWord = "lwbg_tutorial_back_second";
            }
            // ゲームのメイン画面へ遷移する
            if (CMouseState.LeftButtonPressing && CMouseState.InRectangle(this.owner.buttonList["exit"].rectData))
            {
                this.owner.selectSoundEffect("decide").Play();
                owner.CurrentState = "Title";
                this.owner.currentScene.parameter["titleCheck"] = "TRUE";
            }

        }

        public override void Draw()
        {
            spriteBatch.Begin();
            this.tutorialBackObjectList[this.drawBackKeyword].Draw(spriteBatch);
            this.owner.buttonList["next"].DrawRect(spriteBatch);
            this.owner.buttonList["back"].DrawRect(spriteBatch);
            this.owner.buttonList["exit"].DrawRect(spriteBatch);
            spriteBatch.End();
        }


    }
}
