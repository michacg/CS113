using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MinTask : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject completedLine;

    bool completed = false;

    public void AssignTask(string task)
    {
        text.text = "• " + task;

    }

    public void Completed()
    {
        completed = true;
        completedLine.SetActive(true);
    }

    public bool isCompleted()
    {
        return completed;
    }
}
