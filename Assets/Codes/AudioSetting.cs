using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetting : MonoBehaviour
{
    private static  readonly string BgmPref="BgmPref";
    private static  readonly string SfxPref="SfxPref";
    private float BgmValue,SfxValue;
    public AudioSource BgmAudio;
    public AudioSource[] SfxAudio;
    void Awake()//起床讀設定
    {
        ContinueSettings();
    }

    private void ContinueSettings()//把電腦裡的數據讀出來，就可以跨scene保存設定
    {
        BgmValue=PlayerPrefs.GetFloat(BgmPref);
        SfxValue=PlayerPrefs.GetFloat(SfxPref);
        BgmAudio.volume=BgmValue;
        for(int i=0;i<SfxAudio.Length;i++)
        {
            SfxAudio[i].volume=SfxValue;
        }
    }
}
