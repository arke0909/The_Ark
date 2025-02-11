using System.Collections;
using UnityEngine;
using TMPro;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;

namespace Assets.Scripts.UI
{
    public class AreaText : MonoBehaviour
    {
        [SerializeField] private string playerTurnContent;
        [SerializeField] private string healTurnContent;
        [SerializeField] private string buffTurnContent;
        [SerializeField] private float duration;
        [SerializeField] private GameEventChannel turnChangeChannel;

        private TextMeshProUGUI areaText;

        private void Awake()
        {
            areaText = GetComponent<TextMeshProUGUI>();

            turnChangeChannel.AddListner<PriorityTurnChangeEvent>(HandleTurnChange);
        }

        private void OnDestroy()
        {
            turnChangeChannel.RemoveListner<PriorityTurnChangeEvent>(HandleTurnChange);
        }

        private void HandleTurnChange(PriorityTurnChangeEvent evt)
        {
            if (evt.nextTurn == "PLAYER")
            {
                StartCoroutine(TypingCoroutine(playerTurnContent, evt.nextTurn, duration));
            }
            else if (evt.nextTurn == "HEAL")
            {
                StartCoroutine(TypingCoroutine(healTurnContent, evt.nextTurn, duration));
            }
            else if (evt.nextTurn == "BUFF")
            {
                StartCoroutine(TypingCoroutine(buffTurnContent, evt.nextTurn, duration));
            }
            else
            {
                StopAllCoroutines();
            }
        }

        private IEnumerator TypingCoroutine(string text, string nextTurn, float duration)
        {
            float perCharTime = duration / text.Length;
            string result = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                result += text[i];
                areaText.text = result;

                yield return new WaitForSeconds(perCharTime);
            }

            TurnChange(nextTurn);

            yield return null;
        }

        private void TurnChange(string nextTurn)
        {
            TurnChangeCallingEvent evt = new TurnChangeCallingEvent();
            evt.isPriority = false;
            evt.nextTurn = nextTurn;

            turnChangeChannel.RaiseEvent(evt);
        }
    }
}