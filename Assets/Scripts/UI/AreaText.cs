using System.Collections;
using UnityEngine;
using TMPro;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;

namespace Assets.Scripts.UI
{
    public class AreaText : MonoBehaviour
    {
        [SerializeField]private string playerTurnContent;
        [SerializeField]private string healTurnContent;
        [SerializeField]private string buffTurnContent;
        [SerializeField] private float duration;
        [SerializeField] private GameEventChannel turnChangeChannel;

        private TextMeshProUGUI areaText;

        private void Awake()
        {
            areaText = GetComponent<TextMeshProUGUI>();

            turnChangeChannel.AddListner<TurnChangeEvent>(HandleTurnChange);
        }

        private void OnDestroy()
        {
            turnChangeChannel.RemoveListner<TurnChangeEvent>(HandleTurnChange);
        }

        private void HandleTurnChange(TurnChangeEvent evt)
        {
            if (evt.nextTurn == "PLAYER")
            {
                StartCoroutine(TypingCoroutine(playerTurnContent, duration));
            }
            else if(evt.nextTurn == "HEAL")
            {
                Debug.Log(1);
                StartCoroutine(TypingCoroutine(healTurnContent, duration, false));
            }
            else if (evt.nextTurn == "BUFF")
            {
                StartCoroutine(TypingCoroutine(buffTurnContent, duration, false));
            }
            else
            {
                StopAllCoroutines();
            }
        }

        private IEnumerator TypingCoroutine(string text, float duration, bool isPlayerTurn = true)
        {
            float perCharTime = duration / text.Length;
            string result = string.Empty;

           for(int i = 0; i < text.Length; i++)
            {
                result += text[i];
                areaText.text = result;

                yield return new WaitForSeconds(perCharTime);
            }

            if (!isPlayerTurn)
            {
                TurnChangeCallingEvent evt = TurnEvents.TurnChangeCallingEvent;
                evt.nextTurn = "ENEMY";
                turnChangeChannel.RaiseEvent(evt);
            }

            yield return null;
        }
    }
}