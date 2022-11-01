using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{
    /*
        audioSource : 背景音樂
        judgePoint : 判定點
        notePrefab : 音符的 clone
        bpm : 背景音樂的 bpm
        judgePointPosition : 判定點的位置
        generatePosition : "應"生成的位置
        span : 生成間隔
        nextNote : 下個音符生成的時間 (目前假設固定4分音符)
        offset : 第一個音符的延遲
        
    */

    AudioSource audioSource;
    GameObject judgePoint;
    public GameObject notePrefab;
    float bpm = 190f;
    float judgePointPosition;
    float generatePosition = 10f;
    float span;
    float nextNote;
    float offset = 1.357f;
    
    void Start()
    {
        span = 60 / bpm;
        audioSource = GetComponent<AudioSource>();
        judgePoint = GameObject.Find("JudgePoint");
        judgePointPosition = judgePoint.transform.position.x;
        nextNote = span + offset;
    }

    
    void Update()
    {
        //如果現在音樂播放到的時間 = nextNote，就生成下個音符，並把 nextNote 推移 span
        if (audioSource.time > nextNote) {
            GameObject go = Instantiate(notePrefab) as GameObject;

            //下面用於兩行用於調整音符的生成位置(因為 update 的時間會有誤差)
            float ratio = (span * 4 + (nextNote - audioSource.time)) / (span * 4);
            go.transform.position = new Vector3((generatePosition - judgePointPosition) * ratio + judgePointPosition, 0, 0);

            //print(((generatePosition - judgePointPosition) * ratio + judgePointPosition) + "\n" + ratio);
            nextNote += span;
        }
    }
}
