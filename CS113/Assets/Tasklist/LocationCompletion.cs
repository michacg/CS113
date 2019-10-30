using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationCompletion : MonoBehaviour, Completable
{
    [SerializeField] MinorTask _task;
    public MinorTask task { get { return _task; } set { _task = value; } }
    public bool isCompleted { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CheckForCompletion()
    {

    }
}
