using Assets.Scripts.Core.EventChannel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Combat
{
    public class ArrowSetter : MonoBehaviour
    {
        [SerializeField] private Transform arrowHolder;
        [SerializeField] private Transform arrowBack;

        [SerializeField] private IntEventChannel setArrowEvent;
        [SerializeField] private List<Sprite> arrowSpriteList;

        private Dictionary<ArrowType, Sprite> _arrowSpriteDict = new Dictionary<ArrowType, Sprite>();
        private readonly int _arrowTypeCnt = Enum.GetValues(typeof(ArrowType)).Length;

        public List<ArrowType> arrows = new List<ArrowType>();
        
        private void Awake()
        {
            InitDictionary();

            setArrowEvent.ValueEvent += SetArrows;
        }

        private void OnDestroy()
        {
            setArrowEvent.ValueEvent -= SetArrows;
        }

        private void InitDictionary()
        {
            for(int i = 0; i < arrowSpriteList.Count; i++)
            {
                ArrowType arrowType = (ArrowType)i;
                Debug.Log(arrowType);
                Sprite sprite = arrowSpriteList[i];

                _arrowSpriteDict.Add(arrowType, sprite);
            }
        }

        public void SetArrows(int size)
        {
            arrows.Clear();

            for (int i = 0; i < size; i++)
            {
                if (arrowHolder.childCount == 0) break;

                Transform arrowTrm = arrowHolder.GetChild(0);
                ArrowType arrowType = (ArrowType)UnityEngine.Random.Range(0, _arrowTypeCnt);

                arrowTrm.GetComponent<Image>().sprite = _arrowSpriteDict[arrowType];

                arrowTrm.SetParent(arrowBack);

                arrows.Add(arrowType);
                
            }

        }

    }
}