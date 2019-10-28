using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    List<Transform> locations = new List<Transform>();
    //State for our Finite State Machine
    public enum State
    {
        normal,
        suspicious,
        scared,
    };

    [SerializeField] float lookRadius = 5f;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
