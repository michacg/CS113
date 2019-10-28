using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance = null;

    [SerializeField] List<Phase> allTasks;
    [SerializeField] TextMeshProUGUI nameOfRoom;
    [SerializeField] Transform grid;
    [SerializeField] GameObject TaskListPrefab;

    List<TaskList> taskLists;

    int currentList = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        taskLists = new List<TaskList>();
        SetupTasks();
        ChangeList(0);
    }

    private void Update()
    {
        FinishRandomTask();
    }

    void SetupTasks()
    {
        for (int i = 0; i < allTasks.Count; ++i)
        {
            GameObject room = Instantiate(TaskListPrefab, grid);
            room.GetComponent<TaskList>().SetupGrid(allTasks[i].majorTasks);
            taskLists.Add(room.GetComponent<TaskList>());
            room.gameObject.SetActive(false);
        }
    }

    void ChangeList(int index)
    {
        taskLists[index].gameObject.SetActive(true);
        nameOfRoom.text = allTasks[index].phaseName;
        currentList = index;
    }

    public void CompletedTask(string majorTaskName, int index)
    {
        taskLists[currentList].CompletedTask(majorTaskName, index);
        if(taskLists[currentList].isCompleted())
        {
            ChangeList(currentList + 1);
        }
    }

    void FinishRandomTask()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            taskLists[currentList].CompleteRandomTask();
            if (taskLists[currentList].isCompleted())
            {
                ChangeList(currentList + 1);
            }
        }
    }
}
