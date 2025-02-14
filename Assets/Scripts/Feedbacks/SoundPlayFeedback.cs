using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.Pools;
using Assets.Scripts.Sound;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Feedbacks
{
    public class SoundPlayFeedback : Feedback
    {
        [SerializeField] private GameEventChannel poolChannel;
        [SerializeField] private SoundSO soundData;

        public override void StartFeedback()
        {
            SoundPlayer soundPlayer = Pop("SoundPlayer") as SoundPlayer;
            soundPlayer.PlaySound(soundData);
        }

        public override void FinishFeedback()
        {
        }

        public IPoolable Pop(string poolName)
        {
            PoolPopEvent evt = CoreEvents.PoolPopEvent;
            evt.poolName = poolName;

            poolChannel.RaiseEvent(evt);

            if (evt.poolable == null) return null;

            return evt.poolable;
        }
    }
}