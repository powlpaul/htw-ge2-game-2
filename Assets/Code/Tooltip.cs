using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]

/*this class was coded using the following tutorial:
 * https://www.youtube.com/watch?v=HXFoUGw7eKk
 * just like the camera. adding an menu item which is displayed contitionally 
 * based on mouse position is not that difficult
 * but doing it properly can prove challenging. This tutorial proved 
 * as a great ressource for exactly that
 */
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
   
    public TextMeshProUGUI contentField;
    public LayoutElement LayoutElement;
    public int characterWrapLimit;
    public RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetText(string content, string header ="")
    {
        if (string.IsNullOrEmpty(header)) headerField.gameObject.SetActive(false);
        else headerField.gameObject.SetActive(true);
        headerField.text = header;
        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        LayoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit) ? true : false;
    }
    private void Update()
    {
        Vector2 position = Input.mousePosition;
        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;
        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }
}