using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MajorTask", menuName = "MajorTask")]
public class MajorTask : ScriptableObject
{
    public string majorTaskName;
    public List<string> tasksToComplete;
}
