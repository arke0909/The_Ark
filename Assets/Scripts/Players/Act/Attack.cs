using Scripts.Core.EventChannel;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public class Attack : Act
    {
        [SerializeField] IntEventChannel setArrowEvent;
        [SerializeField] int size    = 1;
        public override void OnClick()
        {
            setArrowEvent.RaiseEvent(size);

            _player.InputCompo.Battle();
        }
    }
}