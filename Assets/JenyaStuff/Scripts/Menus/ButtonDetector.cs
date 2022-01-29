using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image bImage;
    private Sprite defaultSprite;



    private void Awake()
    {
        bImage = GetComponent<Image>();
        defaultSprite = bImage.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        bImage.sprite = InterfaceHandler.GetInstance.OnHoverSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bImage.sprite = defaultSprite;
    }
}
