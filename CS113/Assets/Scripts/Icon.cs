using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    public Sprite relax, content, alert;

    [SerializeField]
    private ScareMeter meter;

    

    // Start is called before the first frame update
    void Start()
    {
        icon.GetComponent<Image>().overrideSprite = relax;
        meter = meter.GetComponent<ScareMeter>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(meter.getType());
        if(meter.getType() == 0)
        {
            icon.GetComponent<Image>().overrideSprite = relax;
        }
        else if(meter.getType() == 1)
        {
            icon.GetComponent<Image>().overrideSprite = content;

        }
        else
        {
            icon.GetComponent<Image>().overrideSprite = alert;

        }
    }
}
