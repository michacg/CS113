using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CompletionLocation
{
    SINGLE,
    MULTIPLE
}


public class NumberCompletion : MonoBehaviour, Completable
{
    [SerializeField] MinorTask _task;
    public MinorTask task { get { return _task; } set { _task = value; } }

    [SerializeField] List<GameObject> targetLocations;
    [SerializeField] int NumberOfActions;
    [SerializeField] CompletionLocation loc;

    int actionsRemaining;

    bool reachedTarget = false;

    GameObject target;

    private void Start()
    {
        actionsRemaining = NumberOfActions;
    }

    public void InTargetVicinity(GameObject other)
    {
        if (targetLocations.Contains(other))
            reachedTarget = true;
        target = other;
    }

    public void LeftTargetVicinity(GameObject other)
    {
        if (targetLocations.Contains(other))
            reachedTarget = false;
    }

    public void CheckForCompletion()
    {
        if (reachedTarget)
        {
            if (actionsRemaining <= 0)
                TaskManager.instance.CompletedTask(task.MajorTaskName, task);
            else
            {
                if(loc == CompletionLocation.MULTIPLE)
                    targetLocations.Remove(target);
                actionsRemaining -= 1;
            }
        }
    }
}