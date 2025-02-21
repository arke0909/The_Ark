namespace Assets.Scripts.Core.Save
{
    internal interface ISavable
    {
        SaveIDSO ID { get; }
        public string GetSaveData();
        public void RestoreData(string loadedData);
    }
}
