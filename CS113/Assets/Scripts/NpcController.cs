using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
 
    //State for our Finite State Machine
    public enum State
    {
        walking,
        Action,
        suspicious,
        scared,
    };

    //Add Locations you want NPC to travel to.
    [SerializeField] List<GameObject> locations = new List<GameObject>();
    [SerializeField] bool randomlySelectLocations = false;
    [SerializeField] float lookRadius = 5f;
    [SerializeField] float speed;

    //The min and max of these fields will be chosen by random during Action State.
    [SerializeField] float actionTimeSecMinimum = 3.0f;
    [SerializeField] float actionTimeSecMaximum = 8.0f;

    private State currentState;
    private int i = 0;

    void Start()
    {
        currentState = State.walking;
    }

    
    void Update()
    {
        print(currentState);
        switch(currentState)
        {
            case State.walking:
                WalkingUpdate();
                break;

            case State.Action:
                ActionUpdate();
                break;

            case State.suspicious:
                SuspiciousUpdate();
                break;

            case State.scared:
                ScaredUpdate();
                break;
        }
    }

    void WalkingUpdate()
    {
        transform.LookAt(locations[i].transform);
        float distance = Vector3.Distance(locations[i].transform.position, transform.position);
        print("DISTANCE: "+ distance);

        if (distance > 1) //Not at Location
        {
            print("walkin");
            transform.position = Vector3.MoveTowards(transform.position, locations[i].transform.position, speed);
        }
        else //At Loc. NPC will perform action
        {
            StartCoroutine("DoAction");
        }

    }

    IEnumerator DoAction()
    {
        currentState = State.Action;
        float secondsDoingAction = Random.Range(actionTimeSecMinimum, actionTimeSecMaximum);
        yield return new WaitForSeconds(secondsDoingAction);

        if (randomlySelectLocations)
            i = Random.Range(0, locations.Count);
        else
            i = (i + 1) % locations.Count;
        currentState = State.walking;
    }

    void ActionUpdate()
    {
        //Animations / indicating the User they're distracted
    }

    void SuspiciousUpdate()
    {

    }

    void ScaredUpdate()
    {

    }
}
