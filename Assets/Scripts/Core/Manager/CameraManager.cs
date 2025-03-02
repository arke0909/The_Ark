using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Core.Manager
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannel cameraChannel;

        private CinemachineImpulseSource _impulseSource;

        private void Awake()
        {
            _impulseSource = GetComponent<CinemachineImpulseSource>();
            cameraChannel.AddListener<CameraShakeEvent>(HandleCameraShakeEvent);
        }

        private void OnDestroy()
        {
            cameraChannel.RemoveListener<CameraShakeEvent>(HandleCameraShakeEvent);
        }

        private void HandleCameraShakeEvent(CameraShakeEvent evt)
        {
            _impulseSource.GenerateImpulse(evt.shakePower);
        }
    }
}