using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
namespace IamYourShild
{
    public enum StrikeCheck
    {
        NoCheck,
        StrikeCheck,
    }

    class GameStage
    {
        // メンバ変数
        private EnemyManager stageEnemyManager;
        private PlayerObject stagePlayer;
        private GameMainState prevGameMainState, nowGameMainState;
        private int[] ufoKeyList;
        private List<string> ballNameList;
        private System.Random randomObject;
        private Dictionary<string, GameObject> stageUiList;
        private ContentManager content;
        private GraphicsDeviceManager graphics;
        private GameScene owner;
        private Game game;
        private int justCount;
        private int normalCount;
        private int clearScore;
        private int ufoTargetPartsNumber;
        private int clearTime = 0;
        private float modeBallSpeed;
        private Rectangle strikeZone;
     
        public GameStage(Game game, GraphicsDeviceManager graphics, GameScene owner) 
        {
            this.game = game;
            this.graphics = graphics;
            this.content = game.Content;
            this.owner = owner;
            this.nowGameMainState = GameMainState.TargetRock;
            this.prevGameMainState = this.nowGameMainState;
            this.stagePlayer = new PlayerObject(owner, Color.White, new Rectangle(680, 430, 96, 150), "lwbg_player_normal");
            this.randomObject = new System.Random();
            this.stageUiList = new Dictionary<string, GameObject>();
        }

        public void LoadContent()
        {
            List<string> resultDrawAssetCodeList = new List<string>();
            resultDrawAssetCodeList.Add("anim_base");
            this.stageUiList.Add("resultBack", new UiObject(this.owner, new Color(0.5f, 0.5f, 0.5f, 0.7f), new Vector2(), "anim_base"));
            this.stageUiList["resultBack"].LoadContent(resultDrawAssetCodeList);
            this.stageUiList["resultBack"].rectData = new Rectangle(0, 0, 800, 600);
            this.strikeZone = new Rectangle(755, 498, 50, 60);

        }

        public void setBallSpeed(float ballSpeed)
        {
            this.modeBallSpeed = ballSpeed;
        }

        public void InitialStageData(Dictionary<string, string> stageDataList, bool loadFlg = false, bool bossFlg = false)
        {
            this.nowGameMainState = GameMainState.TargetRock;
            this.stagePlayer.PlayerStageResult = StageResult.Playing;
            this.justCount = 0;
            this.normalCount = 0;
            this.clearScore = 0;
            this.ballNameList = new List<string>();
            string stageBasicDirectory = this.content.RootDirectory + "\\Data\\stage\\Data.txt";
            if (loadFlg)
            {
                List<string> stageBackAssetCodeList = new List<string>();
                List<string> playerAssetCodeList = new List<string>();
                playerAssetCodeList.Add("iys_princess");
                this.stagePlayer.LoadContent(playerAssetCodeList);
            }

            string enemyDirectory = this.content.RootDirectory + "\\Data\\Enemy\\test.txt";
            List<Dictionary<string, string>> enemyDataList = new List<Dictionary<string, string>>();
            enemyDataList = TextBasic.LoadText(enemyDirectory).Data;
            Dictionary<int, EnemyBase> stageEnemyList = new Dictionary<int, EnemyBase>();
            int ufoKeyListCount = 0;
            List<string> randomSetList = new List<string>();
            Vector2 targetPosition = new Vector2( float.Parse(this.stagePlayer.TargetData.Center.X.ToString()), float.Parse(this.stagePlayer.TargetData.Center.Y.ToString()));
            foreach (Dictionary<string, string> ufoDataRow in enemyDataList)
            {
                stageEnemyList.Add(ufoKeyListCount, new NormalEnemy(this.owner, Color.White, new Rectangle(int.Parse(ufoDataRow["SETPOSX"]), int.Parse(ufoDataRow["SETPOSY"]), 80, 80), ufoDataRow["ENEMYASSET"], int.Parse(ufoDataRow["ENEMYLIFE"]), targetPosition));
                ufoKeyListCount++;
            }
            this.stageEnemyManager = new EnemyManager(stageEnemyList);
            this.ufoKeyList = new int[stageEnemyList.Count];
            int setkey = 0;
            foreach (int Key in  stageEnemyList.Keys) {
                this.ufoKeyList[setkey] = Key;
                setkey++;
            }
            this.stagePlayer.NowUfoCount = stageEnemyList.Count;
            this.stagePlayer.stageSelectUfoKey = randomObject.Next(0, ufoKeyList.Length - 1);
            this.stageEnemyManager.LoadContent();
            this.ufoTargetPartsNumber = 1;
     
        }

        public void Update(GameTime gametime)
        {
            // ゲームの終了条件をチェックします。
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.game.Exit();
            this.prevGameMainState = this.nowGameMainState;
           
            int beforeKey = this.stagePlayer.stageSelectUfoKey;
            this.stageEnemyManager.Update(nowGameMainState, this.stagePlayer.PlayerStageResult);
            
            this.nowGameMainState = this.stagePlayer.resultGameState;
            this.getUfoKeyCheck();
            if (this.stagePlayer.stageSelectUfoKey != -1 && this.stagePlayer.stageSelectUfoKey < this.ufoKeyList.Length && this.ufoKeyList[this.stagePlayer.stageSelectUfoKey] != -1)
            {
                this.stagePlayer.UfoListSelectPos = this.stageEnemyManager.getUfoPos(this.ufoKeyList[this.stagePlayer.stageSelectUfoKey], this.ufoTargetPartsNumber);
            }
            else if (this.stagePlayer.stageSelectUfoKey >= this.ufoKeyList.Length)
            {
                for (int checkcount = 0; checkcount < ufoKeyList.Length; checkcount++)
                {
                    if (this.ufoKeyList[checkcount] != -1)
                    {
                        this.stagePlayer.stageSelectUfoKey = this.ufoKeyList[checkcount];
                        this.stagePlayer.UfoListSelectPos = this.stageEnemyManager.getUfoPos(this.ufoKeyList[this.stagePlayer.stageSelectUfoKey], this.ufoTargetPartsNumber);
                        break;
                    }
                }
                
            }
            else if (this.stagePlayer.stageSelectUfoKey == -1 || this.ufoKeyList[this.stagePlayer.stageSelectUfoKey] == -1)
            {
                for (int checkcount = ufoKeyList.Length - 1; checkcount > -1; checkcount--)
                {
                    if (this.ufoKeyList[checkcount] != -1)
                    {
                        this.stagePlayer.stageSelectUfoKey = this.ufoKeyList[checkcount];
                        this.stagePlayer.UfoListSelectPos = this.stageEnemyManager.getUfoPos(this.ufoKeyList[this.stagePlayer.stageSelectUfoKey], this.ufoTargetPartsNumber);
                        break;
                    }
                }

            }
        }

        public StageResult StageResult
        {
            set { this.stagePlayer.PlayerStageResult = value; }
            get { return this.stagePlayer.PlayerStageResult; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           spriteBatch.Begin();
           this.stagePlayer.Draw(spriteBatch);
           this.stageEnemyManager.Draw(spriteBatch);
           this.effectDraw(spriteBatch);
           this.resultDraw(spriteBatch);
           spriteBatch.End();
        }

        private void resultDraw(SpriteBatch spriteBatch)
        {
            if ((this.stagePlayer.PlayerStageResult == StageResult.Win && this.stagePlayer.PlayerStageResult == StageResult.Lose))
            {
                stageUiList["resultBack"].DrawRect(spriteBatch);
                if (this.stagePlayer.PlayerStageResult == StageResult.Lose)
                {
                }

                if (this.stagePlayer.PlayerStageResult == StageResult.Win)
                {
                }
            }
        }

        private void effectDraw(SpriteBatch spriteBatch)
        {
                
        }

        private void getUfoKeyCheck()
        {
            if (this.stagePlayer.stageSelectUfoKey < 0)
            {
                for (int ufoCount = ufoKeyList.Length - 1; ufoCount > -1; ufoCount--)
                {
                    if (ufoKeyList[ufoCount] != -1)
                    {
                        stagePlayer.stageSelectUfoKey = this.ufoKeyList[ufoCount];
                        break;
                    }
                }
            }
        }     

    }
}