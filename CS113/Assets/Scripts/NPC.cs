using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    walking,
    Action,
    suspicious,
    scared,
    confused
};
public class NPC
{
    public NPC(float npcSpeed, float npcSuspiciousRadius, float npcScaredRadius)
    {
        currentState = State.walking;

        speed = npcSpeed;
        suspiciousRadius = npcSuspiciousRadius;
        scaredRadius = npcScaredRadius;   
    }

    public State currentState { get; }
    public float speed { get; }
    public float suspiciousRadius { get; }
    public float scaredRadius { get; }
}
