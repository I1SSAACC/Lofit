using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class OutlineOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        if (_outline == null)
            Debug.LogError($"[{nameof(OutlineOnHover)}] Не найден компонент Outline на объекте «{gameObject.name}»", gameObject);
        else
            _outline.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_outline != null)
            _outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_outline != null)
            _outline.enabled = false;
    }
}
