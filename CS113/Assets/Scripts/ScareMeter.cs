using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScareMeter : MonoBehaviour
{
    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private Image meter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Handle();
    }

    private void Handle()
    {
        meter.fillAmount = fillAmount;
    }

    private float ChangeVal(float value, float min, float max, float mmin, float mmax)
    {
        return (value - min) * (mmax - mmin) / (max - min) + mmin;
    }
}
