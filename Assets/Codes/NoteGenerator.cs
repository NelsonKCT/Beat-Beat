using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class NoteGenerator : MonoBehaviour
{
    /*
        fumenInfo：譜面資訊
        bgm : 背景音樂
        judgePoint : 判定點
        notePrefab : 音符的 clone
        bpm : 背景音樂的 bpm
        judgePointPosition : 判定點的位置
        generatePosition : "應"生成的位置
        nextNote : 下個音符生成的時間 (目前假設固定4分音符)
        offset : 第一個音符的延遲
        
    */
    [SerializeField] TextAsset fumenInfo;
    AudioSource bgm;
    GameObject judgePoint;
    GameObject gameDirector;
    public GameObject notePrefab;
    float bpm;
    float judgePointPosition;
    float generatePosition = 10f;
    
    float offset;

    void Start()
    {   
        var content = fumenInfo.text;
        var AllWords = content.Split("\n");
        List<string> listOfNotes = new List<string>(AllWords);
        print(fumenInfo);
        Application.targetFrameRate = 480;
        bgm = GetComponent<AudioSource>();
        gameDirector = GameObject.Find("GameDirector");
        judgePoint = GameObject.Find("JudgePoint");
        judgePointPosition = judgePoint.transform.position.x;
        int lineIndex = 4;
        //讀入此歌的 bpm 及 offset
        bpm = float.Parse(listOfNotes[1]);
        offset = float.Parse(listOfNotes[3]);
        print(bpm + " " + offset);
        //一一讀入音符，如果是 0 的話表示沒有音符
        //TODO：增加不同的音符種類
        while(!string.Equals(listOfNotes[lineIndex], "END")) {
            for (int i = 0; i < (listOfNotes[lineIndex].Length - 1); i++) {
                if (listOfNotes[lineIndex][i] == '0') continue;
                GameObject go = Instantiate(notePrefab) as GameObject;
                gameDirector.GetComponent<GameDirector>().notesDisplaying.Enqueue(go);
                print((float)lineIndex + (float)i / ((float)listOfNotes[lineIndex].Length - 1));
                go.transform.position = new Vector3(judgePointPosition + (generatePosition - judgePointPosition) * ((float)(lineIndex - 4) + (float)i / ((float)listOfNotes[lineIndex].Length - 1) + offset / (240 / bpm)), 0, 0);
            }
            lineIndex++;
        }
    }

    int cnt = 0;
    void Update()
    {
        //延遲一段時間後再播放bgm
        cnt++;
        if (cnt == 200) {
            bgm.Play();
        }
    }
}
