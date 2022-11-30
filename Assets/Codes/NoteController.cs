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
        gameDirector : 主導遊戲的物件
        noteGenerator : 生成音符的 Object
        spriteRenderer : 控制物件顯示的時機
        bgm : 背景音樂
        judgePointPosition : 判定點的位置
        generatePosition : "應"生成的位置
        judgeTime : 判定時間
        bpm : 背景音樂的 bpm
        speed : 音符移動的速度
        travelDis : 每一禎所移動的距離
    */
    GameObject judgePoint; 
    GameObject gameDirector;
    GameObject noteGenerator;
    SpriteRenderer spriteRenderer;
    public AudioSource bgm;
    float judgePointPosition;
    float generatePosition = 10f;
    public float judgeTime;
    float bpm = 190f;
    float speed;
    float travelDis;

    
    void Start()
    {
        Application.targetFrameRate = 120;
        judgePoint = GameObject.Find("JudgePoint");
        noteGenerator = GameObject.Find("NoteGenerator");
        gameDirector = GameObject.Find("GameDirector");
        bgm = noteGenerator.GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        judgePointPosition = judgePoint.transform.position.x;
        spriteRenderer.enabled = false;
        //下面一行程式用來調整音符的判定時間，因為每個音符生成的位置不同
        judgeTime = (transform.position.x - judgePointPosition) / (generatePosition - judgePointPosition) * (240 / bpm);
        print(judgeTime);
        speed = (judgePointPosition - generatePosition) / (240 / bpm);
        //print((-8.81 - speed * judgeTime) + " " + (transform.position.x));
        travelDis = speed / 200;
        
        
    }
    
    void FixedUpdate()
    {
        if (!spriteRenderer.enabled && transform.position.x < 9) {
            spriteRenderer.enabled = true;
        }
        if (bgm.isPlaying) {
            transform.Translate(travelDis, 0, 0);
        }
        
    }
    /*
    type = 0 是尚未進判定點
    type = 1 是 perfect(+-30ms)，打出全額傷害並且回復20%自身攻擊的血量
    type = 2 是 great(+-50ms)，打出60%傷害並且回復10%自身攻擊的血量
    type = 3 是 good(+-80ms)，打出30%傷害且受到50%敵方攻擊的傷害並使combo歸0
    type = 4 是 miss(>80ms)，受到敵方全額傷害且combo歸0
    */
    public int checkIfHit () {
        //print(judgeTime + " " + bgm.time + " " + transform.position.x);
        if (Math.Abs(judgeTime - bgm.time) < 0.03) {
            return 1;
        } else if (Math.Abs(judgeTime - bgm.time) < 0.05) {
            return 2;
        } else if (Math.Abs(judgeTime - bgm.time) < 0.08) {
            return 3;
        } else if (judgeTime - bgm.time < -0.08) {
            print(judgeTime + " " + bgm.time + " " + transform.position.x);
            return 4;
        }
        return 0;
    }

    
}
