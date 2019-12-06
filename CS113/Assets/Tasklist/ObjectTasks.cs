using System.Collections;
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
                Debug.Log("leaving");
                reachedTarget = false;
            }
        }
    }

    void ReleaseOrDestroy(int curr)
    {
        Debug.Log("here");
        StartCoroutine(RandD(curr));
    }

    IEnumerator RandD(int curr)
    {
        if (tasksToComplete[curr].shouldBeDroppedOnCompletion)
        {
            PossessionController.instance.ReleaseAndPlace(target.transform.position + Vector3.up * 0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        if (tasksToComplete[curr].shouldDestroyOnCompletion)
        {
            Destroy(this.gameObject);
        }
    }

    public void CheckForCompletion()
    {
        if (reachedTarget && currentTask < tasksToComplete.Count)
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
            tasksToComplete[currentTask].NumberOfActions = actionsRemaining;
            if (actionsRemaining <= 0)
            {
                ReleaseOrDestroy(currentTask);
                if (currentTask + 1 < tasksToComplete.Count)
                    actionsRemaining = tasksToComplete[currentTask + 1].NumberOfActions;
                foreach (MinorTask task in tasksToComplete[currentTask].tasks)
                {
                    TaskManager.instance.CompletedTask(task.phase, task.MajorTaskName, task);
                }
                currentTask += 1;
            }
        }
    }
}