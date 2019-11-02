using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] List<GameObject> npcList = new List<GameObject>();
    [SerializeField] float scareMeterAmount;
    [SerializeField] float coolDownSec = 5f;
    [SerializeField] float scareMeterAdder = 0.3f;
    [SerializeField] float multiplierValue = 2f;

    public float npcMultiplier;

    private bool scared = false;
    private bool multiplierOn = false;
    void Awake()
    {
        if (instance == null)
            instance = this;

        scareMeterAmount = 0f;
        npcMultiplier    = 1f;
    }

    void Update()
    {
        Debug.Log("Scare meter amount " + scareMeterAmount);
        if (multiplierOn)
            npcMultiplier = multiplierValue;
        else
            npcMultiplier = 1f;

        CheckScareMeter();

        if (!scared && scareMeterAmount > 0f)
            StartCoroutine("RefillScareMeter");
    }

    IEnumerator RefillScareMeter()
    {

        scareMeterAmount -= scareMeterAdder;
        if (scareMeterAmount < 0f)
            scareMeterAmount = 0f;

        //needs fixing
        yield return new WaitForSeconds(coolDownSec);
    }
    
    void CheckScareMeter()
    {
        if (scareMeterAmount >= 100f)
        {
            multiplierOn = true;
        }
        if (multiplierOn && scareMeterAmount == 0)
        {
            multiplierOn = false;
        }
    }
    public void IncreaseScareMeter()
    {
        //foreach (GameObject npc in npcList)
        scared = true;
        scareMeterAmount += scareMeterAdder;
        if (scareMeterAmount > 100f)
            scareMeterAmount = 100f;

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
