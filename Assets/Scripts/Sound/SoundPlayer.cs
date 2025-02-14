using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.Pools;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : MonoBehaviour, IPoolable
    {
        [SerializeField] private GameEventChannel poolChannel;
        [SerializeField] private AudioMixerGroup SFX, BGM;
        [SerializeField] private string poolName;

        private Tween _delayCallbackTween;
        private AudioSource _audioSource;

        public GameObject PoolObject => gameObject;

        public string PoolName => poolName;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        private void OnDestroy()
        {
            if (_delayCallbackTween.IsActive())
            {
                _delayCallbackTween.Kill();
            }
        }

        public void PlaySound(SoundSO data)
        {
            switch(data.audioType)
            {
                case AudioType.SFX:
                _audioSource.outputAudioMixerGroup = SFX;
                break;

                case AudioType.BGM:
                _audioSource.outputAudioMixerGroup = BGM;
                break;
            }

            _audioSource.volume = data.volume;
            _audioSource.pitch = data.basePitch;

            if(data.randomizePitch)
            {
                _audioSource.pitch += Random.Range(-data.randomizePitchModifier, data.randomizePitchModifier);
            }

            _audioSource.clip = data.clip;
            _audioSource.loop = data.loop;

            if(!data.loop)
            {
                float time = _audioSource.clip.length + 0.2f;
                _delayCallbackTween = DOVirtual.DelayedCall(time, () => Push());
            }

            _audioSource.Play();
        }

        private void Push()
        {
            PoolPushEvent evt = CoreEvents.PoolPushEvent;
            evt.poolable = this;

            poolChannel.RaiseEvent(evt);
        }

        public void ResetItem()
        {
        }
    }
}