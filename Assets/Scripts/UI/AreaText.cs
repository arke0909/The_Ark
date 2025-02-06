using System.Collections;
using UnityEngine;
using TMPro;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;
using System;

namespace Assets.Scripts.UI
{
    public class AreaText : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private GameEventChannel turnChangeChannel;

        private TextMeshProUGUI areaText;
        private string content;

        private void Awake()
        {
            areaText = GetComponent<TextMeshProUGUI>();
            content = areaText.text;

            turnChangeChannel.AddListner<TurnChangeEvent>(HandleTurnChange);
        }

        private void OnDestroy()
        {
            turnChangeChannel.RemoveListner<TurnChangeEvent>(HandleTurnChange);
        }

        private void HandleTurnChange(TurnChangeEvent evt)
        {
            if (evt.turnState == "PLAYER")
            {
                StartCoroutine(TypingCoroutine(content, duration));
            }
        }

        private IEnumerator TypingCoroutine(string text, float duration)
        {
            float perCharTime = duration / text.Length;
            string result = string.Empty;

           for(int i = 0; i < text.Length; i++)
            {
                result += text[i];
                areaText.text = result;

                yield return new WaitForSeconds(perCharTime);
            }

            yield return null;
        }
    }
}