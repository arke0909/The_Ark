using Assets.Scripts.Core.EventChannel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Combat
{
    public class ArrowSetter : MonoBehaviour
    {
        [SerializeField] private Transform arrowHolder;
        [SerializeField] private Transform arrowBack;

        [SerializeField] private List<Sprite> arrowSpriteList;
        [SerializeField] private IntEventChannel setArrowEvent;

        private Dictionary<ArrowType, Sprite> arrowSpriteDict = new Dictionary<ArrowType, Sprite>();
        private readonly int _arrowTypeCnt = Enum.GetValues(typeof(ArrowType)).Length;

        public List<ArrowType> arrows = new List<ArrowType>();
        
        private void Awake()
        {
            setArrowEvent.ValueEvent += SetArrows;
        }

        private void OnDestroy()
        {
            setArrowEvent.ValueEvent -= SetArrows;
        }


        public void SetArrows(int size)
        {
            arrows.Clear();

            for (int i = 0; i < size; i++)
            {
                ArrowType arrow = (ArrowType)UnityEngine.Random.Range(0, _arrowTypeCnt);
                arrows.Add(arrow);
            }

        }

    }
}