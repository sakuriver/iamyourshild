using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace IamYourShild
{

    public enum PlayerActionState 
    {
        Stand,
        Swing,
        SwingResult
    }

    public enum BattingResult
    {
        SwingStrike,
        NoSwing,
        SwingHit,
        SwingExciting,
    }

    public enum StageResult
    {
        Playing,
        Lose,
        Win
    }

    class PlayerObject : GameObject
    {
        private int strikeCount;
        private int playerLife;
        private Vector2 ufoListPos;
        private GameMainState nowGameState;
        private PlayerActionState nowBateerState;
        private BattingResult nowBattingResult;
        private StageResult nowStageResult;
        private int battingHitTime;
        private int swingTime;
        private int stageUfoCount;
        private Dictionary<PlayerActionState, string> actionList;
        public int stageSelectUfoKey;
        private int swingEffectTime = 0;
        private bool swingEffectFlg = false;    // スイング後のエフェクトフラグ
        private Vector2 targetPos;

        public PlayerObject(GameScene owner, Color defaultColor, Rectangle rectData, string drawKeyword)
        {
            this.owner = owner;
            this.textureList = new Dictionary<string, Texture2D>();
            this.color = defaultColor;
            this.rectData = rectData;
            this.drawKeyword = drawKeyword;
            this.playerLife = 3;
            this.strikeCount = 0;
            this.stageSelectUfoKey = 0;
            this.nowBateerState = PlayerActionState.Stand;
            this.battingHitTime = 0;
            this.stageUfoCount = 100;
            this.nowBattingResult = BattingResult.NoSwing;
            this.targetPos = new Vector2(10, 560);
        }

        public Vector2 UfoListSelectPos 
        {
            set { ufoListPos = value; }
        }

        public GameMainState resultGameState
        {
            get { return this.nowGameState; }
        }

        public int NowUfoCount
        {
            set { this.stageUfoCount = value; }
        }

        public override void LoadContent(List<string> assetCodeList)
        {
            foreach (string assetCode in assetCodeList)
            {
                this.textureList.Add(assetCode, this.owner.Content.Load<Texture2D>("Graphic\\Character\\" + assetCode));
            }
        }

        public void Update(GameMainState gameMainState, int ufoCoolTime)
        {
            this.nowGameState = gameMainState;
            if (this.nowStageResult == StageResult.Playing)
            {
                this.targetRockTurn(ufoCoolTime);
                this.buttingResult();
                this.buttingSwing();
            }
            this.stageCheckResult();
            this.swingUpdate();
            this.stageResultUpdate();
            this.drawKeyword = this.actionList[this.nowBateerState];
            base.Update(gameMainState);
        }

        public int PlayerLife
        {
            get { return this.playerLife; }
            set { this.playerLife = value; }
        }

        public StageResult PlayerStageResult
        {
            get { return this.nowStageResult; }
            set { this.nowStageResult = value; }
        }

        public override void Draw(SpriteBatch sprite)
        {
            sprite.Draw(this.textureList["iys_princess"], this.targetPos, Color.White);
        }

        public List<string> getPlayerAssetCodeList()
        {
            List<string> workAssetList = new List<string>();
            return workAssetList;
        }

        private void targetRockTurn(int ufoCoolTime)
        {
            if (this.nowGameState == GameMainState.TargetRock && Input.GetKeyState(Keys.Z) == KeyResultState.PushedNow && ufoCoolTime > 20) 
            {
                this.nowGameState = GameMainState.BallStart;
                this.battingHitTime = 0;
                this.swingTime = 0;
            }
           
            if (this.nowGameState == GameMainState.TargetRock)
            {

                if (Input.GetKeyState(Keys.Up) == KeyResultState.PushedNow)
                {
                    this.stageSelectUfoKey--;
                }

                if (Input.GetKeyState(Keys.Down) == KeyResultState.PushedNow)
                {
                    this.stageSelectUfoKey++;
                }
            }
        }

        private void swingUpdate()
        {
            if (this.swingEffectFlg)
            {
                this.swingEffectTime++;
                if (this.swingEffectTime > 40)
                {
                    this.swingEffectTime = 0;
                    this.swingEffectFlg = false;
                }
            }
        }

        private void buttingResult()
        {   
            if (this.nowGameState == GameMainState.BallStart) 
            {
                this.swingTime++;
                if (Input.GetKeyState(Keys.Z) == KeyResultState.PushedNow && swingTime > 5)
                {
                    this.nowGameState = GameMainState.BallAction;
                    this.nowBateerState = PlayerActionState.Swing;
                    this.swingTime = 0;
                }
            }
        }

        private void buttingSwing()
        {
            if (this.nowGameState == GameMainState.BallAction && this.nowBateerState == PlayerActionState.Swing)
            {
                this.battingHitTime++;
                if (this.battingHitTime >= 15)
                {
                    this.nowBateerState = PlayerActionState.SwingResult;
                    this.battingHitTime = 0;
                    this.strikeCheck();
                    this.nowBattingResult = BattingResult.NoSwing;
                }
            }
        }

        private void stageCheckResult()
        {
            if (this.nowStageResult == StageResult.Playing && this.stageUfoCount == 0)
            {

                this.nowStageResult = StageResult.Win;
            }
        }

        private void stageResultUpdate()
        {
            // todo : ステージクリア・ゲームオーバーの挙動を決めていないでござる
        }
        public void playerDamage()
        {
            this.strikeCount = 2;
            this.strikeCheck();
        }
        public void strikeCheck()
        {
            this.nowBateerState = PlayerActionState.Stand;
            this.strikeCount++;
            if (this.strikeCount >= 3)
            {
                this.strikeCount = 0;
                this.playerLife--;
                this.playerLoseCheck();
            }
        }

        public void playerLoseCheck()
        {
        }

        public int ButtingHitTime
        {
            get { return this.battingHitTime; }
        }

        public PlayerActionState NowBatterState
        {
            get { return this.nowBateerState; }
            set { this.nowBateerState = value; }
        }

        public BattingResult NowBatterResult
        {
            get { return this.nowBattingResult; }
            set { this.nowBattingResult = value; }
        }

        public Vector2 PlayerPosition
        {
            get { return this.PlayerPosition; }
        }

        public bool SwingEffectFlg
        {
            get { return this.swingEffectFlg; }
            set { this.swingEffectFlg = value; }
        }

        public Rectangle TargetData
        {
            get
            {
                return new Rectangle((int)this.targetPos.X, (int)this.targetPos.Y,
                    this.textureList["iys_princess"].Width, this.textureList["iys_princess"].Height); 
                }
        }

    }
}
