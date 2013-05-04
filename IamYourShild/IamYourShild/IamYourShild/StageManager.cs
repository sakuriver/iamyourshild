using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace IamYourShild
{
    public enum StageModeState
    {
        Normal,
        Hard
    }

    class StageManager
    {

        private int nowPlayingStage;
        private StageModeState nowPlayingStageMode;
        private int playerScore = 0;
        private int clearScore = 0;
        private string checkKey;
        private Dictionary<string, GameStage> gameStageList;
        private GameScene owner;
        private Game game;
        private GraphicsDeviceManager graphics;

        public StageManager(Game game, GraphicsDeviceManager graphics, GameScene owner)
        {
            this.game = game;
            this.graphics = graphics;
            this.owner = owner;
            this.clearScore = 0;
            gameStageList = new Dictionary<string, GameStage>();
            this.checkKey = "test";
        }

        public void StageLoadData(int playStageNum, StageModeState playStageMode, List<Dictionary<string, string>> stageDataList, bool bossFlg)
        {
            this.checkKey = "test";
            this.gameStageList.Add(this.checkKey, new GameStage(this.game, this.graphics, this.owner));
            this.gameStageList[this.checkKey].LoadContent();
            Dictionary<string, string> stageParameterList = new Dictionary<string, string>();
            this.gameStageList[this.checkKey].InitialStageData(stageParameterList, true, bossFlg);
            
           // this.checkKey = playStageNum.ToString() + StageManager.ConvertModeString(playStageMode);
            
            
            if ( this.gameStageList.ContainsKey(this.checkKey))
            {
                this.gameStageList[this.checkKey].InitialStageData(stageParameterList, false, bossFlg);
            }
            else
            {
                this.gameStageList.Add(this.checkKey, new GameStage(this.game, this.graphics, this.owner));
                this.gameStageList[this.checkKey].LoadContent();
                this.gameStageList[this.checkKey].InitialStageData(stageParameterList, true, bossFlg);
            }

            this.nowPlayingStage = playStageNum;
            this.nowPlayingStageMode = playStageMode; 
        }

        public int PlayerScore
        {
            set { this.playerScore = value; }
            get { return this.playerScore; }
        }

        public void Update(GameTime gameTime)
        {
            if (this.gameStageList.ContainsKey(this.checkKey))
            {
                this.gameStageList[this.checkKey].Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.gameStageList.ContainsKey(this.checkKey))
            {
                this.gameStageList[this.checkKey].Draw(spriteBatch);
            }
        }

        public StageResult stageResult
        {
            get {
                if (this.gameStageList.ContainsKey(this.checkKey))
                {
                    return this.gameStageList[this.checkKey].StageResult;
                }
                else 
                {
                    return StageResult.Playing;
                }
            }
        }

    }
}
