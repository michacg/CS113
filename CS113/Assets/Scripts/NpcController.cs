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
        confused
    };

    //Add Locations you want NPC to travel to.
    [SerializeField] List<GameObject> locations = new List<GameObject>();
    [SerializeField] bool randomlySelectLocations = false;
    [SerializeField] float suspciousRadius = 5f;
    [SerializeField] float scaredRadius = 2f;
    [SerializeField] float speed;
    [SerializeField] float slowSpeedMulti = 0.5f;

    //The min and max of these fields will be chosen by random during Action State.
    [SerializeField] float actionTimeSecMinimum = 3.0f;
    [SerializeField] float actionTimeSecMaximum = 8.0f;

    private State currentState;
    private int i = 0;
    private Collider[] objectsAround;
    private GameObject susObject;

    void Start()
    {
        currentState = State.walking;
    }

    
    void Update()
    {
        Debug.Log(currentState);
        LookAround();
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

            case State.confused:
                ConfusedUpdate();
                break;
        }
    }

    void LookAround()
    {
        if (currentState != State.suspicious && currentState != State.scared)
        {
            if (possessedObjIsAround())
                currentState = State.suspicious;
        }
        else if ((currentState == State.suspicious || currentState == State.scared) && !possessedObjIsAround())
        {
            currentState = State.confused;
        }
    }

    bool possessedObjIsAround()
    {
        objectsAround = Physics.OverlapSphere(GetComponent<Transform>().position, suspciousRadius);
        for (int i = 0; i < objectsAround.Length; ++i)
        {
            if (currentState != State.suspicious && currentState != State.scared && objectsAround[i].gameObject.CompareTag("possessed"))
            {
                susObject = objectsAround[i].gameObject;
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, suspciousRadius);
    }

    void WalkingUpdate()
    {
        transform.LookAt(locations[i].transform);
        float distance = Vector3.Distance(locations[i].transform.position, transform.position);

        if (distance > 1) //Not at Location
        {
            transform.position = Vector3.MoveTowards(transform.position, locations[i].transform.position, speed);
        }
        else //At Location. NPC will perform action
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
        //Animations / indicating the User they're distracted.
    }

    void SuspiciousUpdate()
    {
        transform.LookAt(susObject.transform);
        float distance = Vector3.Distance(susObject.transform.position, transform.position);

        if (distance > scaredRadius) //Not Scared yet
        {
            transform.position = Vector3.MoveTowards(transform.position, susObject.transform.position, speed * slowSpeedMulti);
        }
        else if (susObject == null)
        {
            print("howdy");
        }
        else
        {
            currentState = State.scared;
        }
    }

    void ScaredUpdate()
    {
        //animation for scared
        //increase scare meter
        print("I'm Scared!!");
    }

    void ConfusedUpdate()
    {
        transform.LookAt(susObject.transform);
        float distance = Vector3.Distance(susObject.transform.position, transform.position);

        if (distance > scaredRadius) //Not Scared yet
        {
            transform.position = Vector3.MoveTowards(transform.position, susObject.transform.position, speed * slowSpeedMulti);
        }
        else
        {
            StartCoroutine("DoAction");
            print("I'm confused!");
            //animation for suspicion
        }
    }
}
