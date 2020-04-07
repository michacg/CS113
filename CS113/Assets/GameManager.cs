using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // [SerializeField] List<GameObject> npcList = new List<GameObject>();
    [SerializeField] float scareMeterAmount;
    [SerializeField] float coolDownSec     = 5f;
    [SerializeField] float scareMeterAdder = 0.3f;
    [SerializeField] float refillWaitSec   = 1f;
    [SerializeField] float multiplierValue = 2f;

    public float npcMultiplier;

    private bool scared = false;
    private bool multiplierOn = false;
    void Awake()
    {
        StartCoroutine("RefillScareMeter");
        if (instance == null)
            instance = this;

        scareMeterAmount = 100f;
        npcMultiplier    = 1f;
    }

    void Update()
    {
//        Debug.Log("Scare meter amount " + scareMeterAmount);
        if (multiplierOn)
            npcMultiplier = multiplierValue;
        else
            npcMultiplier = 1f;

        CheckScareMeter();
    }

    IEnumerator RefillScareMeter()
    {
        while (true)
        {
            if (scareMeterAmount < 100f)
            {
                scareMeterAmount += scareMeterAdder;
                if (scareMeterAmount > 100f)
                    scareMeterAmount = 100f;
                yield return new WaitForSeconds(refillWaitSec);
            }
            else
            {
                yield return null;
            }
            
        }
            
    }
    
    void CheckScareMeter()
    {
        if (scareMeterAmount <= 0f)
        {
            multiplierOn = true;
        }
        if (multiplierOn && scareMeterAmount == 100f)
        {
            multiplierOn = false;
        }
    }
    public void DecreaseScareMeter()
    {
        scared = true;
        scareMeterAmount -= scareMeterAdder;
        if (scareMeterAmount <= 0f)
            scareMeterAmount  = 0f;

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

    public float getAmount()
    {
        return scareMeterAmount;
    }
        
}
