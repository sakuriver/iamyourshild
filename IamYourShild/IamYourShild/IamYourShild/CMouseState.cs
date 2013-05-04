using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace IamYourShild
{
    static class CMouseState
    {
        static MouseState prestate, currentstate;

        public static void Update() 
        {
            prestate = currentstate;
            currentstate = Mouse.GetState();
        }

        public static int X { get { return currentstate.X; } }
        public static int Y { get { return currentstate.Y; } }
        public static Vector2 Pos { get { return new Vector2(currentstate.X, currentstate.Y); } }
        public static MouseState State { get { return currentstate; } }

        public static bool InRectangle(Rectangle Rectangle) 
        {
            return (Rectangle.Left < X) && (X < Rectangle.Right) && (Rectangle.Top < Y) && (Y < Rectangle.Bottom); 
        }

        public static bool InRectangle(Rectangle Rectangle, Point Location) 
        {
            Rectangle.Location = Location;
            return InRectangle(Rectangle); 
        }

        public static bool LeftButtonPressed
        {
            get
            {
                return (currentstate.LeftButton == ButtonState.Pressed) &&
                       (prestate.LeftButton == ButtonState.Released);
            }
        }
        public static bool LeftButtonPressing
        {
            get
            {
                return (currentstate.LeftButton == ButtonState.Pressed);
            }
        }
        public static bool LeftButtonReleased
        {
            get
            {
                return (currentstate.LeftButton == ButtonState.Released) &&
                       (prestate.LeftButton == ButtonState.Pressed);
            }
        }

        public static bool MiddleButtonPressed
        {
            get
            {
                return (currentstate.MiddleButton == ButtonState.Pressed) &&
                       (prestate.MiddleButton == ButtonState.Released);
            }
        }
        public static bool MiddleButtonPressing
        {
            get
            {
                return (currentstate.MiddleButton == ButtonState.Pressed);
            }
        }
        public static bool MiddleButtonReleased
        {
            get
            {
                return (currentstate.MiddleButton == ButtonState.Released) &&
                       (prestate.MiddleButton == ButtonState.Pressed);
            }
        }
        public static bool RightButtonPressed
        {
            get
            {
                return (currentstate.RightButton == ButtonState.Pressed) &&
                       (prestate.RightButton == ButtonState.Released);
            }
        }
        public static bool RightButtonPressing
        {
            get
            {
                return (currentstate.RightButton == ButtonState.Pressed);
            }
        }
        public static bool RightButtonReleased
        {
            get
            {
                return (currentstate.RightButton == ButtonState.Released) &&
                       (prestate.RightButton == ButtonState.Pressed);
            }
        }
        public static int CurrentScrollWheelValue
        {
            get 
            {
                return currentstate.ScrollWheelValue - prestate.ScrollWheelValue;
            }
        }
    }
}
