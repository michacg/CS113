using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance = null;

    [SerializeField] float heightIncrease = 0.1f;
    [SerializeField] List<Phase> allTasks;
    [SerializeField] TextMeshProUGUI nameOfRoom;
    [SerializeField] Transform grid;
    [SerializeField] GameObject TaskListPrefab;

    List<TaskList> taskLists;

    RectTransform rt;

    int currentList = 0;

    bool openingPanel = false;

    float startingY;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        rt = this.GetComponent<RectTransform>();
        startingY = rt.anchoredPosition.y;
        taskLists = new List<TaskList>();
        SetupTasks();
        ChangeList(0);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        //FinishRandomTask();
    }

    void SetupTasks()
    {
        for (int i = 0; i < allTasks.Count; ++i)
        {
            GameObject room = Instantiate(TaskListPrefab, grid);
            room.GetComponent<TaskList>().SetupGrid(allTasks[i].majorTasks);
            taskLists.Add(room.GetComponent<TaskList>());
        }
    }
    public void CompletedTask(int phase, string majorTaskName, MinorTask minTask)
    {
        phase -= 1;
        if (!openingPanel && !IfTaskLineCompleted(phase, majorTaskName, minTask))
        {
            StartCoroutine(BringUpPanel(phase, majorTaskName, minTask));
        }
    }

    bool IfTaskLineCompleted(int phase, string majorTaskName, MinorTask minTask)
    {
        return taskLists[phase].isMinorTaskCompleted(majorTaskName, minTask);
    }

    //void FinishRandomTask()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if(!openingPanel)
    //        {
    //            StartCoroutine(BringUpPanel());
    //        }
    //    }
    //}

    bool CompletedFinishing()
    {
        StartCoroutine(BringDownPanel());
        return true;
    }

    IEnumerator BringUpPanel(int phase, string majorTaskName, MinorTask minTask)
    {
        ChangeList(phase);
        openingPanel = true;
        while(rt.anchoredPosition.y  < 0)
        {
            rt.anchoredPosition += (Vector2.up * heightIncrease * Time.deltaTime);
            yield return null;
        }
        rt.anchoredPosition = Vector3.zero;
        yield return new WaitForSeconds(0.2f);
        taskLists[currentList].CompletedTask(majorTaskName, minTask, CompletedFinishing);
    }

    IEnumerator BringDownPanel()
    {
        while (rt.anchoredPosition.y > startingY)
        {
            rt.anchoredPosition -= (Vector2.up * heightIncrease * Time.deltaTime);
            yield return null;
        }
        rt.anchoredPosition = Vector3.up * startingY;
        if (taskLists[currentList].isCompleted())
        {
            ChangeList(currentList + 1);
        }
        openingPanel = false;
    }

    void ChangeList(int index)
    {
        taskLists[index].gameObject.transform.SetAsLastSibling();
        nameOfRoom.text = allTasks[index].phaseName;
        currentList = index;
    }



}
