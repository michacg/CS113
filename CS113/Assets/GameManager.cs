using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] List<GameObject> npcList = new List<GameObject>();
    [SerializeField] float scareMeterAmount;
    [SerializeField] float scareMeterAdder = 0.3f;

    [SerializeField] float coolDownSec = 5f;

    private bool scared = false;
    void Awake()
    {
        if (instance == null)
            instance = this;

        scareMeterAmount = 0f;
    }

    void Update()
    {
        print("Scare meter amount " + scareMeterAmount);
        if (!scared && scareMeterAmount > 0f)
            StartCoroutine("RefillScareMeter");
    }

    IEnumerator RefillScareMeter()
    {
        scareMeterAmount -= scareMeterAdder;
        if (scareMeterAmount < 0f)
            scareMeterAmount = 0f;

        yield return new WaitForSeconds(coolDownSec);
    }
    
    public void IncreaseScareMeter()
    {
        //foreach (GameObject npc in npcList)
        scared = true;
        scareMeterAmount += scareMeterAdder;
        StartCoroutine("TriggerScaredBool");
    }

    IEnumerator TriggerScaredBool()
    {
        if (scared)
        {
            yield return new WaitForSeconds(coolDownSec);
            scared = false;
        }
    }
        
}
