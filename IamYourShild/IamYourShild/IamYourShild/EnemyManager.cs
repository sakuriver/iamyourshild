using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace IamYourShild
{
    class EnemyManager
    {
        private Dictionary<int, EnemyBase> stageEnemyList;
        public const string NORMAL_UFOTYPE_STR = "NORMAL";
        public const string PARTS_UFOTYPE_STR = "PARTS";
        public const string MISSILE_UFOTYPE_STR = "MISSILE";
        
        public EnemyManager(Dictionary<int, EnemyBase> stageEnemyList)
        {
            this.stageEnemyList = new Dictionary<int, EnemyBase>();
            this.stageEnemyList = stageEnemyList;
        }

        public void LoadContent()
        {

            foreach (KeyValuePair<int, EnemyBase> enemyObject in this.stageEnemyList)
            {

                enemyObject.Value.LoadContent(enemyObject.Value.getEnemyAssetList());
            }
        }


        public void Update(GameMainState gameMainState, StageResult stageResult) 
        {
            foreach (KeyValuePair<int, EnemyBase> enemyObject in this.stageEnemyList)
            {
                enemyObject.Value.Update(gameMainState);
            }
            
        }

        public void Draw(SpriteBatch sprite)
        {
            foreach (KeyValuePair<int, EnemyBase> enemyObject in this.stageEnemyList)
            {
                enemyObject.Value.Draw(sprite);
            }
        }

        public void damageUfo(int getStageUfoKey, int ufoTargetNumber, int ufoPartsNumber, bool extraFlg, BattingResult battingResult)
        {
            
        }

        public void afterEffect(int afterCount)
        {
            foreach (KeyValuePair<int, EnemyBase> enemyObject in this.stageEnemyList)
            {
                
            }
        }

        public int UfoNowCount
        {
            get {
                int ufoCount = 0;
                return ufoCount; 
            }
        }

        public Vector2 getUfoPos(int getStageEnemyKey, int selectPartsTarget = 1)
        {
            return new Vector2(this.stageEnemyList[getStageEnemyKey].rectData.X, this.stageEnemyList[getStageEnemyKey].rectData.Y);
        }
        public Vector2 getUfoCenterPos(int getStageEnemyKey)
        {
            return new Vector2(this.stageEnemyList[getStageEnemyKey].rectData.Center.X - 5, this.stageEnemyList[getStageEnemyKey].rectData.Center.Y - 5);
        }

        public Rectangle getUfoRectData(int getStageEnemyKey)
        {
            return this.stageEnemyList[getStageEnemyKey].rectData;
        }

    }
}
