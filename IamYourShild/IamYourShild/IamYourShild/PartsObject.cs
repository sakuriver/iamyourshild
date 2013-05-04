using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IamYourShild
{
    // パーツオブジェクト
    // 今回の作成思想では、親である本体クラスが画像を持っているため、
    // パーツ側では、自分を証明するキーと座標、描画キーを持っていればいい物とする
    class PartsObject
    {
        public string   drawKeyword;
        public Vector2 drawPosition;
        public int partsNumber;
        public Color drawColor;

        public PartsObject(string drawKeyword, Vector2 drawPosition, int partsNumber, Color drawColor)
        {
            this.drawKeyword = drawKeyword;
            this.drawPosition = drawPosition;
            this.partsNumber = partsNumber;
            this.drawColor = drawColor;
        }

    }
}
