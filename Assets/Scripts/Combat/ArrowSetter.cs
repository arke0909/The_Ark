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
        private int _size;

        private readonly int _arrowTypeCnt = Enum.GetValues(typeof(ArrowType)).Length;

        public List<Arrow> Arrows { get; private set; } = new List<Arrow>();

        private void Awake()
        {
            InitDictionary();

            setArrowEvent.ValueEvent += HandleSetArrows;
        }

        private void OnDestroy()
        {
            setArrowEvent.ValueEvent -= HandleSetArrows;
        }

        private void InitDictionary()
        {
            for (int i = 0; i < arrowSpriteList.Count; i++)
            {
                ArrowType arrowType = (ArrowType)i;
                Sprite sprite = arrowSpriteList[i];

                _arrowSpriteDict.Add(arrowType, sprite);
            }
        }

        public void HandleSetArrows(int size)
        {
            ResetArrows();

            _size = size;

            if (arrowHolder.childCount == 0) return;

            Arrows.Clear();

            for (int i = 0; i < size; i++)
            {

                ArrowType arrowType = (ArrowType)UnityEngine.Random.Range(0, _arrowTypeCnt);
                
                Transform arrowTrm = arrowHolder.GetChild(0);
                Debug.Assert(arrowTrm != null, $"{arrowHolder.name} has not children");

                Arrow arrow = arrowTrm.GetComponent<Arrow>();
                Debug.Assert(arrowTrm != null, $"{arrowTrm.name} has not Arrow Compo");

                arrow.SetArrowType(arrowType);

                arrowTrm.GetComponent<Image>().sprite = _arrowSpriteDict[arrowType];
                arrowTrm.SetParent(arrowBack);

                Arrows.Add(arrow);

            }
        }

        private void ResetArrows()
        {
            int childCnt = arrowBack.childCount;

            for (int i = 0; i < childCnt; i++)
            {
                arrowBack.GetChild(0).SetParent(arrowHolder);
            }
        }

        public Arrow SetCurrentArrow(int idx) => Arrows[idx];

        public void OnFailArrowSet()
        {
            foreach (Arrow arrow in Arrows)
            {
                if (arrow.isClear)
                    arrow.Open();
            }
        }

        public int GetSize()
        {
            return _size;
        }

    }
}