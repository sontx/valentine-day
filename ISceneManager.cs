using System.Drawing;

namespace Valentine
{
    public interface ISceneManager
    {
        void Update(Rectangle bound);
        void Draw(Graphics g, Rectangle bound);
        void Init();
        void Destroy();
    }
}
