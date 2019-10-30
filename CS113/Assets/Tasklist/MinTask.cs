using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinTask : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject completedLine;
    [SerializeField] float lineInc = 100f; 
    [SerializeField] float maxWidth = 250f;
    [SerializeField] float xOffset;


    RectTransform rt;
    LayoutElement le;

    bool completed = false;

    private void Awake()
    {
        rt = this.GetComponent<RectTransform>();
        le = this.GetComponent<LayoutElement>();
    }

    public float AssignTask(string task, float initialY)
    {

        text.text = "• " + task;
        rt.anchoredPosition = new Vector2(xOffset, initialY);
        if(rt.rect.width > maxWidth)
        {
            le.enabled = true;
            le.preferredHeight = maxWidth;
        }
        return text.preferredHeight;

    }

    public void Completed(System.Func<bool> tm, System.Func<bool> tl, System.Func<System.Func<bool>, System.Func<bool>, bool> mt)
    {
        StartCoroutine(DrawLine(tm, tl, mt));
    }

    IEnumerator DrawLine(System.Func< bool> tm ,System.Func<bool> tl, System.Func<System.Func<bool>, System.Func<bool>, bool> mt)
    {
        RectTransform lrt = completedLine.GetComponent<RectTransform>();
        while(lrt.sizeDelta.x < text.preferredWidth)
        {
            lrt.sizeDelta += Vector2.right *lineInc * Time.deltaTime;
            yield return null;
        }
        lrt.sizeDelta = new Vector2(text.preferredWidth, lrt.sizeDelta.y);
        completed = true;
        mt(tm, tl);
    }

    public bool isCompleted()
    {
        return completed;
    }
}
