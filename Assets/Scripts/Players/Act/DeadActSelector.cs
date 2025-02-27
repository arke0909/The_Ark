namespace Assets.Scripts.Players.Act
{
    public class DeadActSelector : ActSelector
    {
        protected override void Awake()
        {
            base.Awake();

            HandleValueChange(false);
        }

        protected override void HandleValueChange(bool value)
        {
            base.HandleValueChange(value);

            _canvasGroup.alpha = value ? 1 : 0;
            _canvasGroup.interactable = value;
            _canvasGroup.blocksRaycasts = value;
        }
    }
}