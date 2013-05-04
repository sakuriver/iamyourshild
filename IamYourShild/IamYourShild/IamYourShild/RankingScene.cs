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
    /// ランキングシーンクラス
    /// </summary>
    class RankingScene : GameScene
    {

        private Dictionary<string, UiObject> rankingBackObjectList;
        private List<Texture2D> rankList;
        private string drawBackKeyword;
        private List<Dictionary<string, string>> rankingDataList;
        SpriteFont scoreFont;
        public const int Normal_Rank_PosX = 50;
        public const int Normal_Rank_PosY = 200;
        public const int Hard_Rank_PosX = 430;
        public const int Hard_Rank_PosY = 200;
        public RankingScene(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SceneManager owner)
            : base(game, graphics, spriteBatch, owner)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = game.Content;
            this.spriteBatch = spriteBatch;
            this.owner = owner;
            this.param = new Parameter();
            this.rankingDataList = new List<Dictionary<string, string>>();
            this.drawBackKeyword = "iys_ranking_back";
        }

        public override void LoadContent()
        {
            // タイトル画像の読込み
            this.rankingBackObjectList = new Dictionary<string, UiObject>();
            this.rankingBackObjectList.Add("iys_ranking_back", new UiObject(this, Color.White, Vector2.Zero, "iys_ranking_back"));
            List<string> assetCodeList = new List<string>();
            assetCodeList.Add("iys_ranking_back");
            this.rankingBackObjectList["iys_ranking_back"].LoadContent(assetCodeList);
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

            // タイトル画面から遷移してきたら、データを読み込む
            if (parameter["sceneCheck"] == "true") 
            {
                string rankingDataDirectory = content.RootDirectory + "\\Data\\config\\RankData.txt";
             //   this.rankingDataList = TextBasic.LoadText(rankingDataDirectory).Data;
                parameter["sceneCheck"] = "false";
                this.owner.buttonList["normal"].rectData = new Rectangle(150, 65, 171, 45);
                this.owner.buttonList["hard"].rectData = new Rectangle(520, 65, 131, 45);
            }

            // ゲームのメイン画面へ遷移する 
            if (CMouseState.LeftButtonPressed && CMouseState.InRectangle(this.owner.buttonList["exit"].rectData))
            {
                this.owner.selectSoundEffect("decide").Play();
                this.owner.CurrentState = "Title";
                this.owner.currentScene.parameter["titleCheck"] = "TRUE";
            }
            this.owner.buttonList["exit"].checkKeyWord(CMouseState.Pos);
        }

        public override void Draw()
        {
            spriteBatch.Begin();
            this.rankingBackObjectList[this.drawBackKeyword].Draw(spriteBatch);
        /*    this.owner.buttonList["normal"].DrawRect(spriteBatch);
            this.owner.buttonList["hard"].DrawRect(spriteBatch);
            int rankingCount = 0;
            int baseRankPosX = 0;
            foreach (Dictionary<string, string> rankingDatarow in rankingDataList)
            {
                if (rankingDatarow["MODE"] == "normal")
                {
                    baseRankPosX = RankingScene.Normal_Rank_PosX;
                }
                else
                {
                    baseRankPosX = RankingScene.Hard_Rank_PosX;
                }
                int drawscore = int.Parse(rankingDatarow["SCORE"]);
                Vector2 rankdrawPos = new Vector2((float)baseRankPosX, (float)int.Parse(rankingDatarow["RANK"])*70 + 100);
                spriteBatch.Draw(rankList[int.Parse(rankingDatarow["RANK"]) - 1], new Vector2( rankdrawPos.X , rankdrawPos.Y - 10), Color.White);
                int checkNumberValue = 1;
                int addX = int.Parse(MathHelper.Min((float)4, (float)(drawscore.ToString().Length - 1)).ToString());
                int numberMax = addX;
                int drawnumber = 1;
                while (drawscore > checkNumberValue - 1 && addX >= 0)
                {
                    drawnumber = int.Parse(drawscore.ToString().Substring(addX, 1));
                    spriteBatch.DrawString(scoreFont, drawnumber.ToString(), new Vector2(rankdrawPos.X - 40 + (5 + (addX - numberMax)) * 30 + 100, rankdrawPos.Y - 25), Color.White);
                    checkNumberValue = checkNumberValue * 10;
                    addX--;
                }
                rankingCount++;
            }*/
            this.owner.buttonList["exit"].DrawRect(spriteBatch);
            spriteBatch.End();
        }
    }
}
