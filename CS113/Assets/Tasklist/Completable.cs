using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface Completable
{
    void InTargetVicinity(GameObject other);
    void LeftTargetVicinity(GameObject other);
    void CheckForCompletion();
    
    

}
