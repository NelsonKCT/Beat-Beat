using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    private static  readonly string FirstPlay="FirstPlay";
    private static  readonly string BgmPref="BgmPref";
    private static  readonly string SfxPref="SfxPref";

    private int FirstPlayInt;
    public Slider BgmSlider,SfxSlider;
    private float BgmValue,SfxValue;
    public AudioSource BgmAudio;
    public AudioSource[] SfxAudio;
    void Start()
    {
        //看是不是第一次開遊戲
        FirstPlayInt=PlayerPrefs.GetInt(FirstPlay);
        if(FirstPlayInt==0)//是的話就初始化
        {
            BgmValue=1.0f;
            SfxValue=1.0f;
            BgmSlider.value=BgmValue*100;
            SfxSlider.value=SfxValue*100;
            //把設定存到電腦裡面
            PlayerPrefs.SetFloat(BgmPref, BgmValue);
            PlayerPrefs.SetFloat(SfxPref,SfxValue);
            PlayerPrefs.SetInt(FirstPlay,-1);
        }
        else//不是的話就從電腦抓之前存的數值
        {
            BgmValue=PlayerPrefs.GetFloat(BgmPref);
            BgmSlider.value=BgmValue*100;
            SfxValue=PlayerPrefs.GetFloat(SfxPref);
            SfxSlider.value=SfxValue*100;
        }
        
    }

    public void SaveSoundSettings()//存動過的設定
    {
        PlayerPrefs.SetFloat(BgmPref, BgmSlider.value/100);
        PlayerPrefs.SetFloat(SfxPref,SfxSlider.value/100);
    }

    void OnApplicatoinFocus(bool InFocus)//切走畫面或是關掉會觸發
    {
        if(!InFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()//slider動的時候要觸發這個(slider要記得選on value change會做這個)
    {
        BgmAudio.volume=BgmSlider.value/100;
        for(int i=0;i<SfxAudio.Length;i++)
        {
            SfxAudio[i].volume=SfxSlider.value/100;
        }
        SaveSoundSettings();//因為上面的OnApplicatoinFocus不屌我所以這裡也放一個儲存更新設定
    }
}
