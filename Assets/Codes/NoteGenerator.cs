using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class NoteGenerator : MonoBehaviour
{
    /*
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
        bpm = float.Parse(listOfNotes[1]);
        offset = float.Parse(listOfNotes[3]);
        print(bpm + " " + offset);
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
        //預先生成100個等間隔的音符
        /*
        for (float i = 0f; i < 100; i++) {
            GameObject go = Instantiate(notePrefab) as GameObject;
            gameDirector.GetComponent<GameDirector>().notesDisplaying.Enqueue(go);
            //print((generatePosition - judgePointPosition) * (i / 4 + 1 + offset / (240 / bpm)) + judgePointPosition);
            go.transform.position = new Vector3((generatePosition - judgePointPosition) * (i / 4 + offset / (240 / bpm)), 0, 0);
        }
        */
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
