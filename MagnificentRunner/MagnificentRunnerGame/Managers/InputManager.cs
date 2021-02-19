using Microsoft.Xna.Framework.Input;

namespace MagnificentRunnerGame.Managers
{
    public class InputManager
    {
        private static InputManager _instance;
        public static InputManager Instance
        {
            get 
            {
                if (_instance is null)
                    _instance = new InputManager();

                return _instance;
            }
        }

        public bool IsExiting {get; private set;} = false;
        public KeyboardState PreviousKeyboardState { get; private set; }
        public KeyboardState CurrentKeyboardState { get; private set; }


        public void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
        }

        public bool KeyPressed(params Keys[] keys) 
        {
            foreach (var key in keys) 
                if (CurrentKeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key))
                    return true;
            
            return false;
        }

        public bool KeyReleased(params Keys[] keys)
        {
            foreach (var key in keys)
                if (CurrentKeyboardState.IsKeyUp(key) && PreviousKeyboardState.IsKeyDown(key))
                    return true;
            
            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (var key in keys)
                if (CurrentKeyboardState.IsKeyDown(key))
                    return true;
            
            return false;
        }
    }
}