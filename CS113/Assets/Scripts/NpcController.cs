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
    [SerializeField] float slowSpeedMulti = 0.5f;
    [SerializeField] float speed;

    private float npcMultiplier;

    //The min and max of these fields will be chosen by random during Action State.
    [SerializeField] float actionTimeSecMinimum = 3.0f;
    [SerializeField] float actionTimeSecMaximum = 8.0f;

    public State currentState;
    private int i = 0; //iterator for locations
    private Collider[] objectsAround;
    private GameObject susObject;

    void Start()
    {
        currentState = State.walking;
    }

    
    void Update()
    {
        Debug.Log("Current State" + currentState);
        Debug.Log("Location " + locations[i]);
        
        npcMultiplier = GameManager.instance.npcMultiplier;

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
        if (currentState != State.scared)
        {
            if (possessedObjIsAround())
                currentState = State.suspicious;
        }
    }

    bool possessedObjIsAround()
    {
        objectsAround = Physics.OverlapSphere(GetComponent<Transform>().position, suspciousRadius * npcMultiplier);
        for (int i = 0; i < objectsAround.Length; ++i)
        {
            if (objectsAround[i].gameObject.CompareTag("possessed"))
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
        Gizmos.DrawWireSphere(transform.position, suspciousRadius * npcMultiplier);
    }

    void WalkingUpdate()
    {
        transform.LookAt(locations[i].transform);

        Vector3 nextLoc = new Vector3(locations[i].transform.position.x, transform.position.y, locations[i].transform.position.z);

        float distance = Vector3.Distance(nextLoc, transform.position);

        if (distance > 1) //Not at Location
        {
            transform.position = Vector3.MoveTowards(transform.position, nextLoc, speed * npcMultiplier);
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
        if (!possessedObjIsAround())
        {
            currentState = State.confused;
            return;
        }

        transform.LookAt(susObject.transform);
        float distance = Vector3.Distance(susObject.transform.position, transform.position);

        if (distance > scaredRadius) //Not Scared yet
        {
            transform.position = Vector3.MoveTowards(transform.position, susObject.transform.position, speed * slowSpeedMulti);
        }
        else
        {
            currentState = State.scared;
        }
    }

    void ScaredUpdate()
    {
        if (!possessedObjIsAround())
        {
            currentState = State.confused;
            return;
        }
        //animation for scared
        //increase scare meter
        print("I'm Scared!!");
        GameManager.instance.DecreaseScareMeter();
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
