
using UnityEngine;
using UnityEngine.EventSystems;

class TooltipTrigger : MonoBehaviour, IPointerEnterHandler,  IPointerExitHandler
{
    private float TimeOverElement;
    public string header;
    [TextArea]
    public string content;
    public void OnPointerEnter(PointerEventData eventData)
    {
  
        TooltipSystem.Show(content, header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }


}