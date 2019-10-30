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
    }
    // Start is called before the first frame update
    void Start()
    {
        rt = this.GetComponent<RectTransform>();
        startingY = rt.anchoredPosition.y;
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
    public void CompletedTask(string majorTaskName, int index)
    {
        taskLists[currentList].CompletedTask(majorTaskName, index, CompletedFinishing);
        if(taskLists[currentList].isCompleted())
        {
            ChangeList(currentList + 1);
        }
    }

    void FinishRandomTask()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!openingPanel)
            {
                StartCoroutine(BringUpPanel());
            }
        }
    }

    bool CompletedFinishing()
    {
        StartCoroutine(BringDownPanel());
        return true;
    }

    IEnumerator BringUpPanel()
    {
        openingPanel = true;
        while(rt.anchoredPosition.y  < 0)
        {
            rt.anchoredPosition += (Vector2.up * heightIncrease * Time.deltaTime);
            yield return null;
        }
        rt.anchoredPosition = Vector3.zero;
        yield return new WaitForSeconds(0.2f);
        taskLists[currentList].CompleteRandomTask(CompletedFinishing);
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
            ChangeList(1);
        }
        openingPanel = false;
    }

    void ChangeList(int index)
    {
        taskLists[index].gameObject.SetActive(true);
        nameOfRoom.text = allTasks[index].phaseName;
        currentList = index;
    }



}
