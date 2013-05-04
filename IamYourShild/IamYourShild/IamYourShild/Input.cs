using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace IamYourShild
{
    public class Input
    {
        private static KeyboardState CurrentKeyState;
        private static KeyboardState PrevKeyState;

        public void Update()
        {
            UpdateKeys();
        }

        private void UpdateKeys()
        {
            PrevKeyState = CurrentKeyState;
            CurrentKeyState = Keyboard.GetState();
        }

        public static KeyResultState GetKeyState(Keys key)
        {
            bool cks = CurrentKeyState.IsKeyDown(key);
            bool pks = PrevKeyState.IsKeyDown(key);

            if (cks) {
                if (pks) {
                    //前から押してる
                    return KeyResultState.HasBePushed;
                } else {
                    //今押した
                    return KeyResultState.PushedNow;
                }
            } else {
                if (pks) {
                    //今離した
                    return KeyResultState.ReleaseNow;
                } else {
                    //離した瞬間ですらない
                    return KeyResultState.NeverPushed;
                }
            }
        }
    }

    public enum KeyResultState 
    {
        PushedNow, 
        HasBePushed, 
        ReleaseNow, 
        NeverPushed 
    }
}
