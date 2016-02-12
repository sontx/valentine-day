using System;
using System.Drawing;

namespace Valentine
{
    public interface ISceneManager
    {
        event EventHandler Finish;
        void Update(Rectangle bound);
        void Draw(Graphics g, Rectangle bound);
        void Init();
        void Destroy();
    }
}
