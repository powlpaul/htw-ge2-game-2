using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{

    private static TooltipSystem TTS;


    public Tooltip tooltip;
    public void Awake()
    {
        TTS = this;
        Hide();
    }

    public static void Show(string content, string header = "")
    {
        TTS.tooltip.SetText(content, header);
        TTS.tooltip.gameObject.SetActive(true);
    }
  
    public static void Hide()
    {
        TTS.tooltip.gameObject.SetActive(false);
    }
}