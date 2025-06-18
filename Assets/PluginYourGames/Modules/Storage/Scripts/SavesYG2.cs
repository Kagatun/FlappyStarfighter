namespace YG
{
    [System.Serializable]
    public partial class SavesYG
    {
        public int idSave;

        public bool IsDesktop = true;
        public bool IsLeft = true;
        public bool IsAutoAttack;

        public int MaxHitPoints;
        public int PercentageMultiShooting;
        public int CooldownLaser;
        public int CooldownShield;

        public int LevelIndex = 1;
        public int LevelNumber;
        
        public int Gold;
        public int Score;

        public float MasterVolume = 1f;
        public float MusicVolume = 1f;
        public float SoundVolume = 1f;
    }
}