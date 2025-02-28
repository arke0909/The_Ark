using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Sound
{
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField] private SoundSO soundSO;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();

            _audioSource.resource = soundSO.clip;

            _audioSource.Play();
        }
    }
}