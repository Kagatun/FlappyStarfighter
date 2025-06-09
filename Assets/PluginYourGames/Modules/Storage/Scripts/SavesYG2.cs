
namespace YG
{
    [System.Serializable]
    public partial class SavesYG
    {
        public int idSave;

        public bool IsDesktop = true;
        public int MaxHitPoints;
        public int Score;
        public int LevelIndex = 1;
        public float MasterVolume = 1;
        public float MusicVolume = 1;
        public float SoundVolume = 1;
    }
}
