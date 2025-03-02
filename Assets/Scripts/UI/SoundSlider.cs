using Assets.Scripts.Core.InGameData;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class SoundSlider : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private FloatData volumeData;
        [SerializeField] private string volumeName;

        private Slider _bar;

        private void Awake()
        {
            _bar = GetComponentInChildren<Slider>();
            _bar.value = volumeData.value;
            audioMixer.SetFloat(volumeName, Mathf.Log10(volumeData.value) * 20);

        }

        public void OnValueChange()
        {
            volumeData.value = _bar.value;
            audioMixer.SetFloat(volumeName, Mathf.Log10(volumeData.value) * 20);
        }
    }
}