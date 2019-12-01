using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject move_tut = null;
    [SerializeField] GameObject float_tut = null;
    [SerializeField] GameObject possess_tut = null;
    [SerializeField] GameObject unpossess_tut = null;
    [SerializeField] GameObject tasklist_tut = null;
    [SerializeField] GameObject goal = null;
    
    [SerializeField] GameObject movement_control = null;
    
    void Start()
    {
        StartCoroutine("tutorial");
    }
    
    IEnumerator tutorial()
    {
        // MOVE
        move_tut.SetActive(true);
        StartCoroutine(textTransition(move_tut));
        while (true)
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");
            yield return new WaitForSeconds(0.1f);
            if (v != 0 || h != 0)
            {
                yield return new WaitForSeconds(0.8f);
                break;
            }
        }
        move_tut.SetActive(false);
        yield return new WaitForSeconds(1f);
        
        // FLOAT
        float_tut.SetActive(true);
        StartCoroutine(textTransition(float_tut));
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (Input.GetKey(KeyCode.Space))
            {
                yield return new WaitForSeconds(0.8f);
                break;
            }
        }
        float_tut.SetActive(false);
        yield return new WaitForSeconds(1f);
        
        // POSSESS
        possess_tut.SetActive(true);
        StartCoroutine(textTransition(possess_tut));
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (movement_control.transform.childCount > 1)
            {
                yield return new WaitForSeconds(0.8f);
                break;
            }
        }
        possess_tut.SetActive(false);
        yield return new WaitForSeconds(1f);
        
        // UNPOSSESS
        unpossess_tut.SetActive(true);
        StartCoroutine(textTransition(unpossess_tut));
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (movement_control.transform.childCount == 1)
            {
                yield return new WaitForSeconds(0.8f);
                break;
            }
        }
        unpossess_tut.SetActive(false);
        yield return new WaitForSeconds(1f);
        
        //TASKLIST
        tasklist_tut.SetActive(true);
        StartCoroutine(textTransition(tasklist_tut));
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (TaskManager.instance.open == true)
            {
                yield return new WaitForSeconds(0.8f);
                break;
            }
        }
        tasklist_tut.SetActive(false);
        yield return new WaitForSeconds(1f);
        
        //GOAL
        goal.SetActive(true);
        StartCoroutine(textTransition(goal));
        yield return new WaitForSeconds(5f);
        StartCoroutine(textTransition2(goal, "But don't get spotted when possessing objects or you'll scare them..."));
        yield return new WaitForSeconds(8f);
        goal.SetActive(false);
        
        yield return null;
    }
    
    IEnumerator textTransition(GameObject go)
    {
        TextMeshProUGUI TEXT = go.GetComponent<TextMeshProUGUI>();
        
        string t = TEXT.text;
        TEXT.text = "";
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < t.Length; i++)
        {
            TEXT.text += t[i];
            yield return new WaitForSeconds(0.05f);
        }
        
        yield return null;
    }
    
    IEnumerator textTransition2(GameObject go, string t)
    {
        TextMeshProUGUI TEXT = go.GetComponent<TextMeshProUGUI>();
        
        TEXT.text = "";
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < t.Length; i++)
        {
            TEXT.text += t[i];
            yield return new WaitForSeconds(0.05f);
        }
        
        yield return null;
    }
}
