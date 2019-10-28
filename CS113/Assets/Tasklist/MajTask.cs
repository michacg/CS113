using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MajTask : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject minorTaskPrefab;

    List<MinTask> minorTasks;

    bool completed = false;

    int completedCount = 0;

    private void Awake()
    {
        minorTasks = new List<MinTask>();
    }

    public void AssignTask(string majorTask, List<string> minorTasks)
    {
        text.text = "• " + majorTask;
        CreateMinorTasks(minorTasks);
    }

    void CreateMinorTasks(List<string> mTasks)
    {
        foreach(string s in mTasks)
        {
            GameObject mt = Instantiate(minorTaskPrefab, this.transform);
            MinTask m = mt.GetComponent<MinTask>();
            minorTasks.Add(m);
            m.AssignTask(s);
        }
    }

    public void CompletedMinorTask(int index)
    {
        minorTasks[index].Completed();
        ++completedCount;
        if(completedCount >= minorTasks.Count)
        {
            completed = true;
            text.fontStyle = FontStyles.Strikethrough;
        }
    }

    public void CompleteRandomTask()
    {
        int index = Random.Range(0, minorTasks.Count);
        while(minorTasks[index].isCompleted())
        {
            index = Random.Range(0, minorTasks.Count);
        }
        minorTasks[index].Completed();
        ++completedCount;
        if (completedCount >= minorTasks.Count)
        {
            completed = true;
            text.text = "<s>" + text.text + "</s>";
        }
    }

    public void ClearText()
    {
        text.text = "";
    }

    public bool isCompleted()
    {
        return completed;
    }
}
