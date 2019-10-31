﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfCompletion
{
    LOCATION,
    NUMBEROFACTIONS
}

public interface Completable
{
    MinorTask task { get; set; }
    bool isCompleted { get; set; }
    void InTargetVicinity(GameObject other);
    void LeftTargetVicinity(GameObject other);
    void CheckForCompletion();
    
    

}
