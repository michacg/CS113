using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MajTask : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject completedLine;
    [SerializeField] float lineInc = 100f;
    [SerializeField] GameObject minorTaskPrefab;

    RectTransform rt;

    Dictionary<MinorTask, MinTask> minorTasks;
    //List<MinTask> minorTasks;

    bool completed = false;

    int completedCount = 0;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        minorTasks = new Dictionary<MinorTask, MinTask>();
    }

    public float AssignTask(string majorTask, List<MinorTask> minorTasks)
    {
        text.text = "• " + majorTask;
        return CreateMinorTasks(minorTasks);
    }

    float CreateMinorTasks(List<MinorTask> mTasks)
    {
        float initialY = text.preferredHeight * -1;
        foreach (MinorTask s in mTasks)
        {
            GameObject mt = Instantiate(minorTaskPrefab, this.transform);
            MinTask m = mt.GetComponent<MinTask>();
            minorTasks.Add(s, m);
            initialY -= m.AssignTask(s.task, initialY);
        }
        return initialY;
    }

    public void CompletedMinorTask(MinorTask mTask, System.Func<bool> tm, System.Func<bool> tl)
    {
        if(!minorTasks[mTask].isCompleted())
            minorTasks[mTask].Completed(tm, tl, Completion);
    }

    public void CompleteRandomTask(System.Func<bool> tm, System.Func<bool> tl)
    {
       // int index = Random.Range(0, minorTasks.Count);
       // while(minorTasks[index].isCompleted())
       // {
       //     index = Random.Range(0, minorTasks.Count);
       // } 
       // minorTasks[index].Completed(tm, tl, Completion);
    }

    bool Completion(System.Func<bool> tm, System.Func<bool> tl)
    {
        StartCoroutine(DrawLine(tm, tl));
        return true;
    }

    IEnumerator DrawLine(System.Func<bool> tm, System.Func<bool> tl)
    {
        ++completedCount;
        if (completedCount >= minorTasks.Count)
        {
            RectTransform lrt = completedLine.GetComponent<RectTransform>();
            while (lrt.sizeDelta.x < text.preferredWidth)
            {
                lrt.sizeDelta += Vector2.right * lineInc * Time.deltaTime;
                yield return null;
            }
            lrt.sizeDelta = new Vector2(text.preferredWidth, lrt.sizeDelta.y);
            completed = true;
            tl();
        }
        yield return new WaitForSeconds(0.3f);
        tm();

    }

    public bool isMinorTaskCompleted(MinorTask mTask)
    {
        if(!completed)
        {
            return minorTasks[mTask].isCompleted();
        }
        return completed;
    }

    public bool isCompleted()
    {
        return completed;
    }
}
