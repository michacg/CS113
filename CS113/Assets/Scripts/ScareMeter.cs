using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScareMeter : MonoBehaviour
{
    Sprite relax = Resources.Load<Sprite>("Bar_Relax");
    Sprite content = Resources.Load<Sprite>("Bar_Content");
    Sprite alert = Resources.Load<Sprite>("Bar_Alert");

    private float fillAmount;

    private float lerpSpeed = 2;

    [SerializeField]
    private Image meter; //Add text later? i.e. %% scare meter

    [SerializeField]
    private Image icon;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            fillAmount = ChangeVal(value, 0, MaxValue, 0, 1);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        meter.fillAmount = 1;
        meter.GetComponent<Image>().sprite = relax;
    }

    // Update is called once per frame
    void Update()
    {
        Handle();
    }

    private void Handle()
    {
        //if(fillAmount != meter.fillAmount)
        //{
        meter.fillAmount = Mathf.Lerp(meter.fillAmount, fillAmount,
            Time.deltaTime * lerpSpeed);
        //}
        if (meter.fillAmount > 0.5)
        {
            meter.GetComponent<Image>().sprite = relax;
        }else if(meter.fillAmount < 0.5 && meter.fillAmount > 0.25)
        {
            meter.GetComponent<Image>().sprite = content;
        }
        else
        {
            meter.GetComponent<Image>().sprite = alert;
        }

    }

    private float ChangeVal(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    
}
