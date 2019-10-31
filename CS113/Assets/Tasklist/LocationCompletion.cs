using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationCompletion : MonoBehaviour, Completable
{
    [SerializeField] MinorTask _task;
    public MinorTask task { get { return _task; } set { _task = value; } }
    public bool isCompleted { get; set; }

    [SerializeField] GameObject targetLocation;


    bool reachedTarget = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void InTargetVicinity(GameObject other)
    {
        if(GameObject.ReferenceEquals(targetLocation, other))
            reachedTarget = true;
        CheckForCompletion();
    }

    public void LeftTargetVicinity(GameObject other)
    {
        if (GameObject.ReferenceEquals(targetLocation, other))
            reachedTarget = false;
    }

    public void CheckForCompletion()
    {
        if(reachedTarget)
        {
            TaskManager.instance.CompletedTask(task.MajorTaskName, task);
        }
    }
}
