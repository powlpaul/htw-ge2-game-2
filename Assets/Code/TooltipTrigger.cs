
using UnityEngine;
using UnityEngine.EventSystems;

class TooltipTrigger : MonoBehaviour, IPointerEnterHandler,  IPointerExitHandler
{
  
    private bool courseInArea;
    public string header;
    [TextArea]
    public string content;
    public void OnPointerEnter(PointerEventData eventData)
    {
        courseInArea = true;
        Waiter.Wait(0.5f, () => {
            if(courseInArea)TooltipSystem.Show(content, header);
        });
        
      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        courseInArea = false;
        TooltipSystem.Hide();

    }

    public void UpgradePressed()
    {
        TooltipSystem.Hide();
        courseInArea = false;
    }
}