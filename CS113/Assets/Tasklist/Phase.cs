using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Phase", menuName = "Phase")]
public class Phase : ScriptableObject
{
    public string phaseName;
    public List<MajorTask> majorTasks;
}
