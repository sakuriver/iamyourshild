using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IamYourShild
{
    class NormalEnemy : EnemyBase
    {
        public NormalEnemy(GameScene owner, Color defaultColor, Rectangle rectData, string drawKeyword, int enemyLife, Vector2 targetPosition)
            : base(owner, defaultColor, rectData, drawKeyword, enemyLife, targetPosition)
        {
            this.enemyTypeCode = EnemyManager.NORMAL_UFOTYPE_STR;
        }

        public override List<string> getEnemyAssetList()
        {
            List<string> enemyAssetList = new List<string>();
            enemyAssetList.Add(this.drawKeyword);
            return enemyAssetList;
        }

    }
}
