using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    [SerializeField] GameObject MajorTaskPrefab;

    List<MajTask> tasksToComplete;

    bool completed = false;
    int completedCount = 0;

    Dictionary<string, int> MajorTaskIndex;

    private void Awake()
    {
        MajorTaskIndex = new Dictionary<string, int>();
        tasksToComplete = new List<MajTask>();
    }

    public void SetupGrid(List<MajorTask> majorTasks)
    {
        float yVal = 0;
        for(int i = 0; i < majorTasks.Count; ++i )
        {
            GameObject t = Instantiate(MajorTaskPrefab, this.transform);
            MajTask ta = t.GetComponent<MajTask>();
            ta.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.up * yVal;
            yVal += ta.AssignTask(majorTasks[i].majorTaskName, majorTasks[i].tasksToComplete);
            tasksToComplete.Add(ta);
            MajorTaskIndex.Add(majorTasks[i].majorTaskName, i);
        }
    }

    public void CompletedTask(string majorTask, MinorTask mTask, System.Func<bool> tm)
    {
        if(!tasksToComplete[MajorTaskIndex[majorTask]].isMinorTaskCompleted(mTask))
            tasksToComplete[MajorTaskIndex[majorTask]].CompletedMinorTask(mTask, tm, CompletedMajTask);
    }

    bool CompletedMajTask()
    {
        completedCount += 1;
        if (completedCount >= tasksToComplete.Count)
        {
            completed = true;
        }
        return true;
    }

    public bool isCompleted()
    {
        return completed;
    }

    public bool isMinorTaskCompleted(string majorTaskName, MinorTask minTask)
    {
        foreach(KeyValuePair<string, int> k in MajorTaskIndex)
        {
            Debug.Log(k.Key);
        }
        if(!completed)
        {
            return tasksToComplete[MajorTaskIndex[majorTaskName]].isMinorTaskCompleted(minTask);
        }
        return true;
    }

    public void CompleteRandomTask(System.Func<bool>tm)
    {
        //int index = Random.Range(0, tasksToComplete.Count);
        //while(tasksToComplete[index].isCompleted())
        //{
        //    index = Random.Range(0, tasksToComplete.Count);
        //}
        //tasksToComplete[index].CompleteRandomTask(tm, CompletedMajTask);
    }
}
