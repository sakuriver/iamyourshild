using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace IamYourShild
{

    public enum GameMainState 
    {
        TargetRock,
        BallStart,
        BallAction,
        ButtingResult,
    }

    class MainScene : GameScene
    {

        private GameMainState prevGameMainState,nowGameMainState;
        private StageManager stageManager;
        private StageModeState nowSelectStageMode;
        private int nowStageNumber;
        public int clearScore;
        private Dictionary<string, int> modeClearNumberList;
        private List<Dictionary<string, string>> stageBasicDataList;

        public MainScene(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SceneManager owner)
            : base(game, graphics, spriteBatch, owner)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = game.Content;
            this.spriteBatch = spriteBatch;
            this.owner = owner;
            this.param = new Parameter();
            this.nowGameMainState = GameMainState.TargetRock;
            this.prevGameMainState = this.nowGameMainState;
            this.stageManager = new StageManager(this.game, this.graphics, this);
            this.nowStageNumber = 1;
            this.clearScore = 0;
            this.nowSelectStageMode = StageModeState.Normal;
            this.modeClearNumberList = new Dictionary<string, int>();
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
            this.mainStateCheck();
            this.stageManager.Update(gametime);
        }

        public void mainStateCheck()
        {
            bool bossCheckFlg = false;
            if (this.parameter["stageChange"] == "TRUE")
            {
                this.parameter["stageChange"] = "FALSE";
                this.clearScore = 0;
                this.nowStageNumber = 1;
            //    this.nowStageNumber = int.Parse(this.parameter["stageNum"]);
                this.stageManager.StageLoadData(this.nowStageNumber, this.nowSelectStageMode, this.stageBasicDataList, bossCheckFlg);
            }
            
  
            if (this.stageManager.stageResult == StageResult.Win || this.stageManager.stageResult == StageResult.Lose)
            {

                if (Input.GetKeyState(Keys.C) == KeyResultState.PushedNow)
                {
                    this.nowStageNumber = 1;
                    this.clearScore = 0;
                    this.owner.CurrentState = "Title";
                    this.owner.currentScene.parameter["titleCheck"] = "TRUE";
                }

                if (Input.GetKeyState(Keys.Z) == KeyResultState.PushedNow)
                {
                    if (this.stageManager.stageResult == StageResult.Win)
                    {
                        this.nowStageNumber++;
                        this.stageManager.StageLoadData(this.nowStageNumber, this.nowSelectStageMode, this.stageBasicDataList, bossCheckFlg);
                    }
                    else
                    {
                        this.clearScore = this.clearScore / 2;
                        this.stageManager.StageLoadData(this.nowStageNumber, this.nowSelectStageMode, this.stageBasicDataList, bossCheckFlg);
                    }
                }
            }
        }

        public override void Draw()
        {
           this.stageManager.Draw(spriteBatch);
        }

        private void execGameClear()
        {
            this.nowStageNumber = 1;
            this.owner.CurrentState = "GameClear";
            Dictionary<string, string> setParameterList = new Dictionary<string, string>();
            setParameterList.Add("scoreCheck", "TRUE");
            setParameterList.Add("totalScore", this.clearScore.ToString());
            this.owner.currentScene.parameter.SetParameter(setParameterList);
        }

    }
}
