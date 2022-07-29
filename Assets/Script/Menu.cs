
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static Menu Instance { get; private set; }

    [SerializeField]private Slider slider;
    public Slider Slider => slider;

    private void Awake()
    {
        Instance = this;
    }

    public void OnClickDeletePoint()
    {
        PointMovement.Instance.ClearPoint();
    }
}
