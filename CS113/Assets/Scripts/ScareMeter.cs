using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScareMeter : MonoBehaviour
{
    public Sprite relax, content, alert;
    int state = 0;

    private float fillAmount;
    
    private float lerpSpeed = 2;

    [SerializeField]
    private Image meter; //Add text later? i.e. %% scare meter

    //private Image icon;

    public float MaxValue { get; set; }

    [SerializeField]
    private Stat scareMeter;

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
        meter.GetComponent<Image>().overrideSprite = relax;

        scareMeter.Initialize();
        scareMeter.CurrentVal = 100;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(relax);
        scareMeter.CurrentVal = GameManager.instance.getAmount();
        Handle();
    }

    private void Handle()
    {
        //if(fillAmount != meter.fillAmount)
        //{
        meter.fillAmount = Mathf.Lerp(meter.fillAmount, fillAmount,
            Time.deltaTime * lerpSpeed);
        //}
        if (meter.fillAmount > 0.5 && meter.GetComponent<Image>().sprite != relax)
        {

            meter.GetComponent<Image>().overrideSprite = relax;
            state = 0;
        }
        else if(meter.fillAmount <= 0.5 && meter.fillAmount > 0.25 &&
            meter.GetComponent<Image>().sprite != content)
        {
            meter.GetComponent<Image>().overrideSprite = content;
            state = 1;
        }
        else if(meter.fillAmount <= 0.25 && meter.GetComponent<Image>().sprite != alert)
        {
            meter.GetComponent<Image>().overrideSprite = alert;
            state = 2;
        }

    }

    private float ChangeVal(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public int getType()
    {
        return state;
    }
    
}
