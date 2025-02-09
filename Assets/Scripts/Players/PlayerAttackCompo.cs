using Assets.Scripts.Combat;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using Assets.Scripts.Entities.Stats;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class PlayerAttackCompo : MonoBehaviour, IPlayerComponent
    {
        [SerializeField] private StatSO attack;
        [SerializeField] private int totalRepeatCnt = 1;
        [SerializeField] private float totalTime;
        [SerializeField] private float minRandomNumber = 0.8f, maxRandomNumber = 1.2f;

        #region EventChannel Section
        [SerializeField] private GameEventChannel turnChangeChannel;
        [SerializeField] private GameEventChannel attackChannel;
        #endregion

        private ArrowSetter _setterCompo;
        private Player _player;

        private Arrow _currentArrow;
        private int _currentRepeatCnt;
        private int _idx;

        private bool _isCheckTime;
        private float _currentTime;
        private float _attack;

        public float DamageMultiply { get; private set; }

        public void Initialize(Player player)
        {
            _player = player;

            _attack = _player.GetCompo<EntityStatComponent>().GetStat(attack).BaseValue;
            _setterCompo = _player.GetPlayerCompo<ArrowSetter>();
            _player.InputCompo.BattleEvent += HandleArrowCheck;
            turnChangeChannel.AddListner<TurnChangeEvent>(HandleTurnChange);
        }

        private void OnDestroy()
        {
            _player.InputCompo.BattleEvent -= HandleArrowCheck;
            turnChangeChannel.RemoveListner<TurnChangeEvent>(HandleTurnChange);
        }

        private void Update()
        {
            if (_isCheckTime)
            {
                _currentTime -= Time.deltaTime;
            }
        }

        private void HandleArrowCheck(ArrowType type)
        {
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
            if (evt.turnState == "INPUT")
            {
                _isCheckTime = true;
                _currentTime = totalTime;
                _idx = 0;
                _currentArrow = _setterCompo.SetCurrentArrow(_idx);

            }
        }

        private int ConvertDamage(float currentTime)
        {
            float randomNumber = Random.Range(minRandomNumber, maxRandomNumber);

            float damage = currentTime * randomNumber * _attack * DamageMultiply;

            Debug.Log(damage);
            return currentTime <= 0 ? 0 : (int)damage;
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
                _currentRepeatCnt = 0;

                DamageCalc();
            }
        }

        private void DamageCalc()
        {
            TurnChangeCallingEvent callevt = TurnEvents.TurnChangeCallingEvent;
            callevt.turnState = "DAMAGECALC";

            turnChangeChannel.RaiseEvent(callevt);

            AttackEvent atkEvt = CombatEvents.AttackEvent;
            atkEvt.damage = ConvertDamage(_currentTime);

            attackChannel.RaiseEvent(atkEvt);
        }

        public void SetDamageMultiply(float damageMultiply)
        {
            DamageMultiply = damageMultiply;
        }
    }
}