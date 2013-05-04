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
    /// ゲームクリアシーンクラス
    /// </summary>
    class GameClearScene : GameScene
    {

        private Dictionary<string, UiObject> titleBackObjectList;
        private int checkCount;
        private string drawBackKeyword;
        private SpriteFont resultFont;
        private List<Dictionary<string, string>> rankingDataList;
        private Texture2D gameClearBack;
        private bool newrecordFlg = false;
        private int rankScore = 0;
        public GameClearScene(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SceneManager owner)
            : base(game, graphics, spriteBatch, owner)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = game.Content;
            this.spriteBatch = spriteBatch;
            this.owner = owner;
            this.param = new Parameter();
        }

        public override void LoadContent()
        {
            // 素材の読み込み
       //     this.resultFont = content.Load<SpriteFont>("GameClearFont");
       //     this.gameClearBack = content.Load<Texture2D>("lwbg_gameclear_back");
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

            if (this.param["scoreCheck"] == "TRUE") 
            {
                if (this.owner.selectSoundEffectInstance("playball").State == SoundState.Playing)
                {
                    this.owner.selectSoundEffectInstance("playball").Stop();
                }
                if (this.owner.selectSoundEffectInstance("boss").State == SoundState.Playing)
                {
                    this.owner.selectSoundEffectInstance("boss").Stop();
                }
                this.owner.buttonList["exit"].rectData = new Rectangle(60, 500, this.owner.buttonList["exit"].rectData.Width, this.owner.buttonList["exit"].rectData.Height);
                string rankingDataDirectory = content.RootDirectory + "\\Data\\config\\RankData.txt";
                this.newrecordFlg = false;
           //     this.rankingDataList = TextBasic.LoadText(rankingDataDirectory).Data;
                Dictionary<string, string> setData = new Dictionary<string, string>();
                setData.Add("MODE", this.param["selectMode"]);
                setData.Add("SCORE", this.param["totalScore"]);
                setData.Add("RANK", "6");
                this.rankScore = 0;
                List<int> rankCheckList = new List<int>();
                if (this.owner.selectSoundEffectInstance("stageclear").State == SoundState.Playing)
                {
                    this.owner.selectSoundEffectInstance("stageclear").Stop();
                }
                this.owner.selectSoundEffectInstance("clear").Play();
                rankCheckList.Add(int.Parse(this.param["totalScore"]));
                List<Dictionary<string, string>> writeRankingDataList  =  new List<Dictionary<string, string>>();
                foreach (Dictionary<string, string> score in rankingDataList)
                {
                    if (setData["MODE"] == score["MODE"]) 
                    {
                        rankCheckList.Add(int.Parse(score["SCORE"]));
                    } 
                    else 
                    {
                        writeRankingDataList.Add(score);
                    }
                }
                rankCheckList.Sort();
                rankCheckList.Reverse();
                List<Dictionary<string, string>> sortRankList = new List<Dictionary<string, string>>();
                for (int ranknumber = 1; ranknumber < 6; ranknumber++)
                {
                    if (setData["SCORE"] == rankCheckList[ranknumber - 1].ToString())
                    {
                        this.rankScore = ranknumber;
                        this.newrecordFlg = true;
                    }

                    sortRankList.Add(new Dictionary<string, string>());
                    sortRankList[sortRankList.Count - 1].Add("MODE", this.param["selectMode"]);
                    sortRankList[sortRankList.Count - 1].Add("RANK", ranknumber.ToString());
                    sortRankList[sortRankList.Count - 1].Add("SCORE", rankCheckList[ranknumber - 1].ToString());
                    writeRankingDataList.Add(sortRankList[sortRankList.Count - 1]);
                }
                this.param["scoreCheck"] = "FALSE";
                this.rankingDataList.Add(setData);
           //     TextBasic.WriteData(rankingDataDirectory, writeRankingDataList);
            }

            this.owner.buttonList["exit"].checkKeyWord(CMouseState.Pos);

            // ゲームのメイン画面へ遷移する
            if (CMouseState.LeftButtonPressing && CMouseState.InRectangle(this.owner.buttonList["exit"].rectData))
            {
                if (this.owner.selectSoundEffectInstance("clear").State == SoundState.Playing)
                {
                    this.owner.selectSoundEffectInstance("clear").Stop();
                }
                this.owner.selectSoundEffect("decide").Play();
                this.owner.CurrentState = "Title";
                this.owner.currentScene.parameter["titleCheck"] = "TRUE";
            }

        }

        public override void Draw()
        {
            this.spriteBatch.Begin();
            this.spriteBatch.Draw(gameClearBack, Vector2.Zero, Color.White);
            this.spriteBatch.DrawString(resultFont, this.param["totalScore"], new Vector2(183, 250), Color.Black);
            this.newRecordDraw();
            this.owner.buttonList["exit"].DrawRect(spriteBatch);
            this.spriteBatch.End();
        }

        private void newRecordDraw()
        {
            if (this.newrecordFlg)
            {
                this.spriteBatch.DrawString(resultFont, "ランキング" + this.rankScore.ToString() + "位に", new Vector2(60, 330), Color.Black);
                this.spriteBatch.DrawString(resultFont, "ランクイン!!", new Vector2(60, 370), Color.Black);
            }

        }
    }
}
