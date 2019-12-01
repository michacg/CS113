using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    // Start is called before the first frame update
    public float horizontal;
    public float vertical;
    public float apt;
    
    public Vector3 pos;

    void Start()
    {
        pos = transform.position;
        StartCoroutine(startWait());
    }

    IEnumerator startWait()
    {
        yield return new WaitForSeconds(1000);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float curr = Time.realtimeSinceStartup;
        if(curr < 1 || vertical == 0 || apt == 0)
        {
            pos = transform.position;
        }
        else
        {
            pos.x += horizontal;
            pos.y += Mathf.Sin(Time.realtimeSinceStartup * vertical) * apt;
        }
        transform.position = pos;
    }
}
