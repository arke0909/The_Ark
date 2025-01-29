using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Core.InGameData;
using Assets.Scripts.Core.Manager;
using System;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class Judge : MonoBehaviour
    {
        [SerializeField] private int totalRepeatCnt = 1;
        [SerializeField] private float totalTime;

        #region EventChannel Section
        [Header("Event Channel")]
        [SerializeField] private ArrowTypeEventChannel arrowCheckChannel;
        [SerializeField] private GameEventChannel turnChangeChannel;
        [SerializeField] private GameEventChannel attackChannel;
        #endregion

        private ArrowSetter _setterCompo;

        private Arrow _currentArrow;
        private int _currentRepeatCnt = 0;

        private bool _isCheckTime;
        private float _currentTime;

        private int _idx;

        private void Awake()
        {
            _setterCompo = GetComponent<ArrowSetter>();

            arrowCheckChannel.ValueEvent += HandleArrowCheck;
            turnChangeChannel.AddListner<TurnChangeEvent>(HandleTurnChange);
        }

        private void OnDestroy()
        {
            arrowCheckChannel.ValueEvent -= HandleArrowCheck;
            turnChangeChannel.RemoveListner<TurnChangeEvent>(HandleTurnChange);
        }

        private void Update()
        {
            if(_isCheckTime)
            {
                _currentTime += Time.deltaTime;
            }  
        }

        private void HandleArrowCheck(ArrowType type)
        {
            if (_currentArrow == null)
               _currentArrow = _setterCompo.SetCurrentArrow(_idx);

            if (_currentArrow.IsEqual(type))
            {
                _idx++;
                _currentArrow.Close();

                if (_idx == _setterCompo.Arrows.Count)
                {
                    EndCheck();
                    return;
                }
            }
            else
            {
                _idx = 0;
                _setterCompo.OnFailArrowSet();
            }

            _currentArrow = _setterCompo.SetCurrentArrow(_idx);
        }

        private void HandleTurnChange(TurnChangeEvent evt)
        {
            if(evt.turnState == "INPUT")
                _isCheckTime = true;
        }

        private void EndCheck()
        {
            _currentRepeatCnt++;

            if (_currentRepeatCnt < totalRepeatCnt)
            {
                int size = _setterCompo.GetSize();
                _setterCompo.HandleSetArrows(size);
                _idx = 0;
                _currentArrow = _setterCompo.SetCurrentArrow(_idx);
            }
            else
            {
                _isCheckTime = false;

                ApplyDamage(_currentTime);

                _currentRepeatCnt = 0;
                _currentTime = 0;
            }
        }

        private void ApplyDamage(float damge)
        {
            AttackEvent atkEvt = CombatEvents.AttackEvent;
            atkEvt.damage = damge;

            attackChannel.RaiseEvent(atkEvt);
        }
    }
}