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
        for(int i = 0; i < majorTasks.Count; ++i )
        {
            GameObject t = Instantiate(MajorTaskPrefab, this.transform);
            MajTask ta = t.GetComponent<MajTask>();
            ta.AssignTask(majorTasks[i].majorTaskName, majorTasks[i].tasksToComplete);
            tasksToComplete.Add(ta);
            MajorTaskIndex.Add(majorTasks[i].majorTaskName, i);
        }
    }

    public void CompletedTask(string majorTask, int minorIndex)
    {
        tasksToComplete[MajorTaskIndex[majorTask]].CompletedMinorTask(minorIndex);
        if(tasksToComplete[MajorTaskIndex[majorTask]].isCompleted())
        {
            completedCount += 1;
            if(completedCount == tasksToComplete.Count)
            {
                completed = true;
            }
        }
    }

    public bool isCompleted()
    {
        return completed;
    }

    public void CompleteRandomTask()
    {
        int index = Random.Range(0, tasksToComplete.Count);
        while(tasksToComplete[index].isCompleted())
        {
            index = Random.Range(0, tasksToComplete.Count);
        }
        tasksToComplete[index].CompleteRandomTask();
        if (tasksToComplete[index].isCompleted())
        {
            completedCount += 1;
            if (completedCount == tasksToComplete.Count)
            {
                completed = true;
            }
        }
    }
}
