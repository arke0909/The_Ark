using UnityEngine;

namespace Assets.Scripts.Sound
{
    public enum AudioType
    {
        SFX, BGM
    }

    [CreateAssetMenu(fileName = "SoundSO", menuName = "SO/Sounds/SoundSO")]
    public class SoundSO : ScriptableObject
    {
        public AudioType audioType;
        public AudioClip clip;
        public bool loop = false;
        public bool randomizePitch = false;

        [Range(0, 1)]
        public float randomizePitchModifier = 0.1f;
        [Range(0, 1)]
        public float volume = 1f;
        [Range(0, 1)]
        public float basePitch = 1f;
    }
}