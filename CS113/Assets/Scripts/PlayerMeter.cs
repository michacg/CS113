using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeter : MonoBehaviour
{
    [SerializeField]
    public GameObject scare;
    //public GameManager scare;

    private Time timer;

    [SerializeField]
    private Stat scareMeter;

    

    private void Awake()
    {
        scareMeter.Initialize();
        //StartCoroutine(addHealth());
        scare = GetComponent<GameManager>().ins

    }

    // Update is called once per frame
    void Update()
    {
        scareMeter.CurrentVal = scare.GetComponent<GameManager>().getAmount();

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
