using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
   
    public TextMeshProUGUI contentField;
    public LayoutElement LayoutElement;
    public int characterWrapLimit;

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
        transform.position = position;
    }
}