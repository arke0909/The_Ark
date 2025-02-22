using Assets.Scripts.Entities.Stats;
using Assets.Scripts.Players;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Entities.Stats.EntityStatComponent;

namespace Assets.Scripts.Core.Save.SaveComponents
{
    public class PlayerStatSaveCompo : MonoBehaviour, IPlayerComponent, ISavable
    {
        [field : SerializeField] public SaveIDSO ID { get; private set; }

        private List<StatSaveData> _stats;
        private Player _player;

        public void Initialize(Player player)
        {
            _player = player;
        }

        public string GetSaveData()
        {
            _stats = _player.GetCompo<EntityStatComponent>().GetSaveData();

            return JsonUtility.ToJson(_stats);
        }


        public void RestoreData(string loadedData)
        {
            List<StatSaveData> loadData = JsonUtility.FromJson<List<StatSaveData>>(loadedData);

            if(_stats != null)
                _player.GetCompo<EntityStatComponent>().RestoreData(loadData);
        }
    }
}