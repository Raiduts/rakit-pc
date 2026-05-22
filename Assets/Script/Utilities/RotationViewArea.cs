using UnityEngine;
using UnityEngine.EventSystems;

public class RotationViewerArea : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public static bool IsHovering = true;

    private void Start()
    {
        IsHovering = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHovering = false;
    }

    private void OnDestroy()
    {
        IsHovering = true;
    }
}