using System.Collections;
using UnityEngine;
using TMPro;
using Assets.Scripts.Core.EventChannel;
using Assets.Scripts.Core.EventChannel.Events;

namespace Assets.Scripts.UI
{
    public class AreaText : MonoBehaviour
    {
        [TextArea]
        [SerializeField]private string content;
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
            if (evt.turnState == "PLAYER")
            {
                StartCoroutine(TypingCoroutine(content, duration));
            }
            else
                areaText.text = "";
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