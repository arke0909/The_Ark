using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Combat
{
    public class Judge : MonoBehaviour
    {
        [SerializeField] private int totalRepeatCnt = 1;
        [SerializeField] private float totalTime;

        #region EventChannel Section
        [Header("Event Channel")]
        [SerializeField] private ArrowTypeEventChannel arrowTypeEvent;
        [SerializeField] private GameEventChannel attackChannel;
        #endregion

        private ArrowSetter _setterCompo;

        private ArrowType _currentArrow;
        private int _currentRepeatCnt = 0;

        private bool _isCheckTime;
        private float _currentTime;

        private int _idx;
        private int _size;

        private void Awake()
        {
            _setterCompo = GetComponent<ArrowSetter>();

            arrowTypeEvent.ValueEvent += ArrowCheck;
        }

        private void OnDestroy()
        {
            arrowTypeEvent.ValueEvent -= ArrowCheck;
        }

        private void Update()
        {
            if(_isCheckTime)
            {
                _currentTime += Time.deltaTime;
            }  
        }

        public void SetArrow(int size)
        {
            if(_isCheckTime == false)
                _isCheckTime = true;

            _setterCompo.arrows.Clear();
            _idx = 0;
            _size = size;

            _currentArrow = _setterCompo.arrows[_idx];
        }

        private void ArrowCheck(ArrowType type)
        {
            bool isRight = _currentArrow == type;

            if (isRight)
            {
                _idx++;

                if (_idx == _setterCompo.arrows.Count)
                {
                    EndCheck();
                    return;
                }
            }
            else
            {
                _idx = 0;
            }

            _currentArrow = _setterCompo.arrows[_idx];
        }

        private void EndCheck()
        {
            _currentRepeatCnt++;

            if (_currentRepeatCnt < totalRepeatCnt)
            {
                SetArrow(_size);
            }
            else
            {
                _isCheckTime = false;

                ApplyDamage(_currentTime);
                _currentTime = 0;
            }
        }

        private void ApplyDamage(float damge)
        {
            AttackEvent evt = CombatEvents.AttackEvent;
            evt.damage = damge;

            attackChannel.RaiseEvent(evt);
        }
    }
}