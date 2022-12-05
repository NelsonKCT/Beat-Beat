using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowValue : MonoBehaviour
{
    public Slider slider;
    int value;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //把slider的值轉成文字貼到text
        value=(int)slider.value;
        text.text=value.ToString();
    }
}
