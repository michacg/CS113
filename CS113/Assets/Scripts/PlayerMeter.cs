using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeter : MonoBehaviour
{
    [SerializeField]
    public NpcController temp;
    public NpcController temp2;
    public NpcController temp3;

    private Time timer;

    [SerializeField]
    private Stat scareMeter;

    

    private void Awake()
    {
        scareMeter.Initialize();
        StartCoroutine(addHealth());

    }

    // Update is called once per frame
    void Update()
    {
        if (checkSus())
        {
            scareMeter.CurrentVal -= 10f;
        }

    }

    private bool checkSus()
    {
        
        if(temp.currentState == NpcController.State.scared ||
            temp2.currentState == NpcController.State.scared ||
            temp3.currentState == NpcController.State.scared)
        {
            return true;
        }

        return false;
    }

    IEnumerator addHealth()
    {
        while (true)
        {
            if(scareMeter.CurrentVal < 100)
            {
                scareMeter.CurrentVal += 1f;
                yield return new WaitForSeconds(1);
            }
            else
            {
                yield return null;
            }
        }
    }
    
}
