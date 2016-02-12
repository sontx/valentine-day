using System;
using System.Collections.Generic;
using System.Drawing;
using Valentine.Properties;

namespace Valentine.x16
{
    public class SceneManager : ISceneManager
    {
        private Random rand = new Random(DateTime.Now.Millisecond);
        private List<DrawableObject> objects = new List<DrawableObject>();
        private List<Image> imageRes = new List<Image>();
        private FileCache backgroundSoundFile = new FileCache(Resources.scene1_sound_bg, "mp3");

        public void Update(Rectangle bound)
        {
            for(int i = 0; i < objects.Count; i++)
            {
                DrawableObject obj = objects[i];
                obj.Update(bound);
                ImageObject imgObj = obj as ImageObject;
                if (imgObj != null && imgObj.IsDisappear())
                    objects.Remove(obj);
            }
        }

        public void Draw(Graphics g, Rectangle bound)
        {
            foreach (DrawableObject obj in objects)
            {
                obj.Draw(g, bound);
            }
        }

        public void GenerateTextObject(float x, float y)
        {
            objects.Add(new TextObject(x, y));
            objects.Insert(0, new ImageObject(x, y, imageRes[rand.Next(imageRes.Count)]));
        }

        public void Init()
        {
            imageRes.Add(Resources.heart1);
            imageRes.Add(Resources.heart2);
            imageRes.Add(Resources.heart3);
            GenerateTextObject(100.0f, 100.0f);
            SoundManager.GetInstance().PlayFile(backgroundSoundFile.FilePath, true);
        }

        public void Destroy()
        {
            foreach (Image image in imageRes)
            {
                image.Dispose();
            }
            SoundManager.GetInstance().Stop();
            backgroundSoundFile.Dispose();
        }
    }
}
