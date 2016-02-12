using System.Collections.Generic;
using System.Drawing;
using Valentine.Properties;

namespace Valentine.x16
{
    public class Scene2Manager : SceneManager
    {
        private static readonly int MAX_OBJ = 20;
        private readonly List<Image> girlPhotos = new List<Image>();

        public override void Init()
        {
            imageRes.Add(Resources.heart1);
            imageRes.Add(Resources.heart2);
            imageRes.Add(Resources.heart3);
            imageRes.Add(Resources.angel1);
            imageRes.Add(Resources.angel2);
            imageRes.Add(Resources.angel3);
            imageRes.Add(Resources.angel4);
            girlPhotos.Add(Resources.placeholder1);
            girlPhotos.Add(Resources.placeholder2);
            girlPhotos.Add(Resources.placeholder3);
            girlPhotos.Add(Resources.placeholder4);
            girlPhotos.Add(Resources.placeholder5);
            girlPhotos.Add(Resources.placeholder6);
            girlPhotos.Add(Resources.placeholder7);
            girlPhotos.Add(Resources.placeholder8);
        }

        private void GenerateObjects(bool first = false)
        {
            ISurface surface = App.GetInstance().Surface;
            int count = objects.Count;
            for (int i = 0; i < MAX_OBJ - count; i++)
            {
                float x = rand.Next(0, surface.ScreenWidth + 50);
                float y = surface.ScreenHeight + rand.Next(100, 600) * (first ? -1.0f : 1.0f);
                float scale = rand.Next(50, 100) / 100.0f;
                Image texture = imageRes[rand.Next(imageRes.Count)];
                objects.Insert(0, new MovableStyle2Object(x, y, scale, texture, null, Color.Black, null));
            }
        }

        protected override void PrepareMajor()
        {
            GenerateObjects(true);
            StartTimer();
        }

        protected override void ObjectRemoved(GameObject obj)
        {
            GenerateObjects();
        }

        protected override void PopSentence(string sentence)
        {
            ISurface surface = App.GetInstance().Surface;
            float x = -150.0f;
            float y = surface.ScreenHeight / 2.0f - 150.0f;
            Color color = Color.FromArgb(rand.Next(100, 255), 0, rand.Next(50, 255));
            Font font = new Font(FontFamily.GenericSansSerif, rand.Next(25, 30));
            Image angel = girlPhotos[rand.Next(girlPhotos.Count)];
            GameObject obj = new MovableStyle3Object(x, y, 1.0f, angel, sentence, color, font);
            obj.Tag = TAG_SENTENCE;
            obj.SpeedX = 0.4f;
            objects.Add(obj);
        }

        public Scene2Manager() 
            : base(Resources.scene2_sound_bg, "You! my love and my life!", Resources.sentences2)
        {
        }

        public override void Destroy()
        {
            base.Destroy();
            foreach (Image image in girlPhotos)
            {
                image.Dispose();
            }
        }
    }
}
