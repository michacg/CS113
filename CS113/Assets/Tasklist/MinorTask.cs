using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinorTask", menuName = "MinorTask")]
public class MinorTask : ScriptableObject
{
    public int phase;
    public string MajorTaskName;
    public string task;

    public override bool Equals(object other)
    {
        MinorTask otherTask = (MinorTask)other;
        return task == otherTask.task;
    }
}
