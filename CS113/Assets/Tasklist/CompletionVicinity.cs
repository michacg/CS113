using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionVicinity : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Completable c = other.gameObject.GetComponent<Completable>();
        if (c != null)
        {
            c.InTargetVicinity(this.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Completable c = other.gameObject.GetComponent<Completable>();
        if (c != null)
        {
            c.LeftTargetVicinity(this.transform.parent.gameObject);
        }
    }

}
