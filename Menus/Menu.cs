using RLNET;

namespace Tetris.Menus
{
    public abstract class Menu
    {
        public int Width { get; set; }
        public int Height { get; set; }

        protected int mouseX;
        protected int mouseY;

        // set mouse position and check if buttons are hovered
        public void SetMousePos(int x, int y)
        {
            mouseX = x;
            mouseY = y;
            HandleHover();
        }

        public abstract void Draw(RLConsole console);

        // check if buttons were clicked
        public abstract void Clicked();

        // check if buttons are hovered
        protected abstract void HandleHover();
    }
}