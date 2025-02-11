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
        [SerializeField] private StatSO criticalChance;
        [SerializeField] private int totalRepeatCnt = 1;
        [SerializeField] private float totalTime;
        [SerializeField] private float minRandomNumber = 0.8f, maxRandomNumber = 1.2f;
        [SerializeField] private float criticalDamageMultiply;

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
        private float _attackValue;
        private float _criticalChanceValue;

        public float DamageMultiply { get; private set; }

        public void Initialize(Player player)
        {
            _player = player;

            _player.GetCompo<EntityStatComponent>().GetStat(attack).OnValueChange += HandleAttackValueChanged;
            _player.GetCompo<EntityStatComponent>().GetStat(criticalChance).OnValueChange += HandleCriticalChanceValueChanged;

            _attackValue = _player.GetCompo<EntityStatComponent>().GetStat(attack).BaseValue;
            _criticalChanceValue = _player.GetCompo<EntityStatComponent>().GetStat(criticalChance).BaseValue;
            
            _setterCompo = _player.GetPlayerCompo<ArrowSetter>();

            _player.InputCompo.BattleEvent += HandleArrowCheck;
            turnChangeChannel.AddListner<TurnChangeEvent>(HandleTurnChange);
        }

        private void OnDestroy()
        {
            _player.GetCompo<EntityStatComponent>().GetStat(attack).OnValueChange -= HandleAttackValueChanged;
            _player.GetCompo<EntityStatComponent>().GetStat(criticalChance).OnValueChange -= HandleCriticalChanceValueChanged;

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
            if (evt.nextTurn == "INPUT")
            {
                _isCheckTime = true;
                _currentTime = totalTime;
                _idx = 0;
                _currentArrow = _setterCompo.SetCurrentArrow(_idx);
            }
            else if(evt.nextTurn == "DAMAGECALC")
            {
                AttackEvent atkEvt = CombatEvents.AttackEvent;
                atkEvt.damage = ConvertDamage(_currentTime);

                attackChannel.RaiseEvent(atkEvt);
            }
        }

        private void HandleAttackValueChanged(StatSO stat, float currentValue, float prevValue) => _attackValue = currentValue;

        private void HandleCriticalChanceValueChanged(StatSO stat, float currentValue, float prevValue) => _criticalChanceValue = currentValue;

        private int ConvertDamage(float currentTime)
        {
            float randomNumber = Random.Range(minRandomNumber, maxRandomNumber);

            float damage = (currentTime * randomNumber  / 2) * _attackValue * DamageMultiply;

            bool isCritical = Random.value < (_criticalChanceValue / 100);
            if (isCritical)
                damage *= criticalDamageMultiply;

            Debug.Log($"{currentTime * randomNumber  / 2} * {_attackValue} * {DamageMultiply} = {damage} : IsCritical {isCritical}");
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
            callevt.nextTurn = "DAMAGECALC";

            turnChangeChannel.RaiseEvent(callevt);
        }

        public void SetDamageMultiply(float damageMultiply)
        {
            DamageMultiply = damageMultiply;
        }
    }
}