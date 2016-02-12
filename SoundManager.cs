namespace Valentine
{
    public sealed class SoundManager
    {
        private static SoundManager instance;

        public static SoundManager GetInstance()
        {
            if (instance == null)
                instance = new SoundManager();
            return instance;
        }

        public static void DestroyInstance()
        {
            if (instance != null)
            {
                instance.player.close();
                instance = null;
            }
        }

        private SoundManager()
        {
            player = new WMPLib.WindowsMediaPlayer();
        }

        private WMPLib.WindowsMediaPlayer player;

        public void PlayFile(string url, bool loop = false)
        {
            Stop();
            player.settings.setMode("loop", loop);
            player.URL = url;
            player.controls.play();
        }

        public void Stop()
        {
            player.controls.stop();
        }
    }
}
