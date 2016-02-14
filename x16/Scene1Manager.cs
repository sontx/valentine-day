using System.Collections.Generic;
using System.Drawing;
using Valentine.Properties;

namespace Valentine.x16
{
    public class Scene1Manager : SceneManager
    {
        private readonly List<Image> angels = new List<Image>();

        public override void Init()
        {
            imageRes.Add(Resources.heart1);
            imageRes.Add(Resources.heart2);
            imageRes.Add(Resources.heart3);
            angels.Add(Resources.angel1);
            angels.Add(Resources.angel2);
            angels.Add(Resources.angel3);
            angels.Add(Resources.angel4);
        }

        protected override void PrepareMajor()
        {
            for (int i = 0; i < 15; i++)
            {
                ISurface surface = App.GetInstance().Surface;
                float x = rand.Next(10, surface.ScreenWidth - 200);
                float y = rand.Next(100, surface.ScreenHeight - 100);
                float scale = rand.Next(50, 200) / 100.0f;
                Image texture = imageRes[rand.Next(imageRes.Count)];
                objects.Add(new MovableStyle1Object(x, y, scale, texture, null, Color.Black, null));
            }
            StartTimer();
        }

        protected override void PopSentence(string sentence)
        {
            ISurface surface = App.GetInstance().Surface;
            float x = surface.ScreenWidth / 2.0f + rand.Next(-300, 50);
            float y = surface.ScreenHeight - 10.0f;
            Color color = Color.FromArgb(rand.Next(100, 255), 0, rand.Next(50, 255));
            Font font = App.GetInstance().InternalFont.getFont(0, rand.Next(25, 30));
            Image angel = angels[rand.Next(angels.Count)];
            GameObject obj = new MovableStyle2Object(x, y, 1.0f, angel, sentence, color, font);
            obj.Tag = TAG_SENTENCE;
            obj.SpeedY = -0.5f;
            objects.Add(obj);
        }

        public Scene1Manager() 
            : base(Resources.scene1_sound_bg, "How did I fall in love with you :\")", Resources.sentences1)
        {
        }

        public override void Destroy()
        {
            base.Destroy();
            foreach (Image image in angels)
            {
                image.Dispose();
            }
        }
    }
}
