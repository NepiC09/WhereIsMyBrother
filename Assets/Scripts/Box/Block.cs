using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("Clamps")]
    [SerializeField] private RectTransform upClamp;
    [SerializeField] private RectTransform downClamp;

    private RectTransform rectTransform;
    public UnityEvent onDragEnded;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log(gameObject.name + "Begin drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(gameObject.name + "drag");
        Vector2 newPos = rectTransform.anchoredPosition + new Vector2(0, eventData.delta.y);        

        if (newPos.y < upClamp.anchoredPosition.y && newPos.y > downClamp.anchoredPosition.y + rectTransform.sizeDelta.y)
        {
            rectTransform.anchoredPosition = newPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onDragEnded?.Invoke();
        //Debug.Log(gameObject.name + "End drag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log(gameObject.name + " on pointer down");
    }
}
