using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    public ArrowsType arrowsType;

    private void Awake()
    {
        _inputReader.ArrowEvent += Check;
    }

    public void Check(ArrowsType arrowsType)
    {
        Debug.Log($"���ϴ� �� : {this.arrowsType}, �Էµ� �� : {arrowsType}");
    }
}
