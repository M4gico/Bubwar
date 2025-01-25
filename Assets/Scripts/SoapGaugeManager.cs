using UnityEngine;
using UnityEngine.UI;

public class SoapGaugeManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform maskRectTransform;

    private float maskHeight;


    void Start()
    {
        maskHeight = maskRectTransform.sizeDelta.y;
    }

    public void SetCrop(float percentage)
    {
        maskRectTransform.sizeDelta = new Vector2(maskRectTransform.sizeDelta.x, maskHeight * percentage);
    }
}
