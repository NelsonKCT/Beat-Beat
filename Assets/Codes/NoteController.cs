using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*

這個程式用於控制每個音符移動及判定
移動到判定點的時間預設是 4 拍 (一拍是 4 分音符)

*/

public class NoteController : MonoBehaviour
{
    /*
        judgePoint : 判定點
        noteGenerator : 生成音符的 Object
        audioSource : 背景音樂
        travelDis : 每一禎所移動的距離
        judgePointPosition : 判定點的位置
        generatePosition : "應"生成的位置
        judgeTime : 判定時間
        bpm : 背景音樂的 bpm
    */
    GameObject judgePoint; 
    GameObject gameDirector;
    GameObject noteGenerator;
    AudioSource audioSource;
    float travelDis;
    float judgePointPosition;
    float generatePosition = 10f;
    float judgeTime;
    float bpm = 190f;

    void Start()
    {
        Application.targetFrameRate = 240;
        judgePoint = GameObject.Find("JudgePoint");
        noteGenerator = GameObject.Find("NoteGenerator");
        gameDirector = GameObject.Find("GameDirector");
        audioSource = noteGenerator.GetComponent<AudioSource>();
        judgePointPosition = judgePoint.transform.position.x;
        //下面一行程式用來調整音符的判定時間，因為每個音符生成的位置不同
        judgeTime = (transform.position.x - judgePointPosition) / (generatePosition - judgePointPosition) * (240 / bpm) + audioSource.time; 
        travelDis = (judgePointPosition - generatePosition) / (Application.targetFrameRate * (240 / bpm));
    }

    void Update()
    {
        //如果按下空白鍵，就判定有沒有擊中拍子
        if (Input.GetKeyDown(KeyCode.Space)) {
            checkIfHit();
        }
        transform.Translate(travelDis, 0, 0);
        
        //如果超出邊界，便自動摧毀，並判定miss
        if (transform.position.x < -11) {
            gameDirector.GetComponent<GameDirector>().calculate_score(3);
            Destroy(gameObject);
            
        }
    }
    /*
    checkIfHit 用來判定是否擊中拍點
    若差距 0.05s 便是 perfect
    若差距 0.1s 便是 great
    */
    public void checkIfHit () {
        if (Math.Abs(judgeTime - audioSource.time) < 0.05) {
            gameDirector.GetComponent<GameDirector>().calculate_score(1);
            Destroy(gameObject);
        } else if (Math.Abs(judgeTime - audioSource.time) < 0.1) {
            gameDirector.GetComponent<GameDirector>().calculate_score(2);
            Destroy(gameObject);
        }
        return;
    }
}
