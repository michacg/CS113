﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CompletionType
{
    ONETIME,
    SINGLE,
    MULTIPLE
}


public class ObjectTasks : MonoBehaviour, Completable
{
    [SerializeField] List<Completion> tasksToComplete;
    [SerializeField] List<GameObject> locations;

    int currentTask = 0;

    GameObject target;

    bool reachedTarget = false;

    int actionsRemaining;

    public void InTargetVicinity(GameObject other)
    {
        Debug.Log("HIHI");
        if (currentTask < tasksToComplete.Count)
        {
            actionsRemaining = tasksToComplete[currentTask].NumberOfActions;
            target = other;
            if (locations.Contains(other))
                reachedTarget = true;

            if (tasksToComplete[currentTask].type == CompletionType.ONETIME)
                CheckForCompletion();
        }
    }

    public void LeftTargetVicinity(GameObject other)
    {
        if (currentTask < tasksToComplete.Count)
        {
            if (locations.Contains(other))
            {
                tasksToComplete[currentTask].NumberOfActions = actionsRemaining;
                reachedTarget = false;
            }
        }
    }

    public void CheckForCompletion()
    {
        Debug.Log("HOHO");
        if (reachedTarget)
        {
            switch (tasksToComplete[currentTask].type)
            {
                case CompletionType.MULTIPLE:
                    Destroy(target);
                    actionsRemaining -= 1;
                    break;
                default:
                    actionsRemaining -= 1;
                    break;

            }
            if (actionsRemaining == 0)
            {
                TaskManager.instance.CompletedTask(tasksToComplete[currentTask].task.phase,tasksToComplete[currentTask].task.MajorTaskName, tasksToComplete[currentTask].task);
                currentTask += 1;
            }
        }
    }
}