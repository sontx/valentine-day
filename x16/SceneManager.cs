using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Valentine.x16
{
    public abstract class SceneManager : ISceneManager
    {
        private static readonly int WAIT_EACH_SENTENCE = 10000;
        protected static readonly string TAG_SENTENCE = "sentence";
        public event EventHandler Finish;
        protected static readonly Random rand = new Random(DateTime.Now.Millisecond);
        protected readonly List<GameObject> objects = new List<GameObject>();
        protected readonly List<Image> imageRes = new List<Image>();
        protected readonly List<string> stringRes = new List<string>();
        private FileCache backgroundSoundFile;
        private bool isMajorScene = false;
        private int countOfSentenceVisiable;

        private string[] SplitSentences(string st)
        {
            return st.Replace(Environment.NewLine, "|").Split('|');
        }

        public SceneManager(byte[] backgroundSound, string sceneTitle, string sentences)
        {
            stringRes.AddRange(SplitSentences(sentences));
            countOfSentenceVisiable = stringRes.Count;
            Task.Run(() => 
            {
                backgroundSoundFile = new FileCache(backgroundSound, "mp3");
                SoundManager.GetInstance().PlayFile(backgroundSoundFile.FilePath);
            });
            MovableStyle4Object titleObj = new MovableStyle4Object(50.0f, 100.0f, sceneTitle, 
                Color.DeepPink, new Font(FontFamily.GenericSansSerif, 30.0f));
            titleObj.SpeedX = 0.4f;
            objects.Add(titleObj);
        }

        protected abstract void PopSentence(string sentence);

        protected abstract void PrepareMajor();

        protected void StartTimer()
        {
            Task.Run(async () => 
            {
                for (int i = 0; i < stringRes.Count; i++)
                {
                    string sentence = stringRes[i];
                    App.GetInstance().Surface.RunSafe((MethodInvoker)delegate { PopSentence(sentence); });
                    await Task.Delay(WAIT_EACH_SENTENCE);
                }
                stringRes.Clear();
            });
        }

        private void FireFinishEvent()
        {
            if (Finish != null)
                Finish(this, EventArgs.Empty);
        }

        protected virtual void ObjectRemoved(GameObject obj)
        {
        }

        public virtual void Update(Rectangle bound)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                GameObject obj = objects[i];
                obj.Update(bound);
                if (obj.IsDisappear(bound))
                {
                    objects.Remove(obj);
                    if (!isMajorScene)
                    {
                        isMajorScene = true;
                        PrepareMajor();
                    }
                    else
                    {
                        ObjectRemoved(obj);
                        if (obj.Tag != null && obj.Tag == TAG_SENTENCE)
                            countOfSentenceVisiable--;
                        if (countOfSentenceVisiable == 0)
                        {
                            FireFinishEvent();
                            break;
                        }
                    }
                }
            }
        }

        public void Draw(Graphics g, Rectangle bound)
        {
            foreach (GameObject obj in objects)
            {
                obj.Draw(g, bound);
            }
        }

        public virtual void Destroy()
        {
            foreach (Image image in imageRes)
            {
                image.Dispose();
            }
            SoundManager.GetInstance().Stop();
            backgroundSoundFile.Dispose();
        }

        public abstract void Init();
    }
}
