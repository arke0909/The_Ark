using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using UnityEngine;

namespace Assets.Scripts.Players.Act
{
    public class Attack : Act
    {
        [SerializeField] GameEventChannel attackChannel;
        [SerializeField] IntEventChannel setArrowChannel;
        [SerializeField] BoolEventChannel changeIsCheckChannel;
        [SerializeField] int arrowSize = 1;
        [SerializeField] private Vector2 _areaSize;
        public override void OnClick()
        {
            changeIsCheckChannel.RaiseEvent(true);
            setArrowChannel.RaiseEvent(arrowSize);

            ChangeAreaSizeEvent evt = CombatEvents.ChangeAreaSizeEvent;
            evt.size = _areaSize;

            attackChannel.RaiseEvent(evt);

            _player.InputCompo.Battle();
        }
    }
}