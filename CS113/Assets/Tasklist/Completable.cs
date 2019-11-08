using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface Completable
{
    MinorTask task { get; set; }
    void InTargetVicinity(GameObject other);
    void LeftTargetVicinity(GameObject other);
    void CheckForCompletion();
    
    

}
