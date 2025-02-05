using UnityEngine;

namespace Assets.Scripts.Feedbacks
{
    public abstract class Feedback : MonoBehaviour
    {
        public abstract void StartFeedback();

        public abstract void FinishFeedback();
    }
}
