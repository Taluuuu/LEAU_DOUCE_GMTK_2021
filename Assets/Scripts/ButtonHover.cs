using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{

    private Button pb;
    public Sprite newSprite;
    public Sprite oldSprite;

    void Start()
    {
        pb = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pb.image.sprite = newSprite; ;
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pb.image.sprite = oldSprite;
    }

}
