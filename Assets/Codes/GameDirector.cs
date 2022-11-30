using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    /*
    score : 分數
    combo : combo
    notesDisplaying : 目前出現在遊戲內的 notes
    */
    AudioSource sfx;
    float score;
    float combo;
    public Queue<GameObject> notesDisplaying;

    
    void Start()
    {
        Hp = 100;
        combo = 0f;
        score = 0f;
        notesDisplaying = new Queue<GameObject>();
        sfx = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        //如果按下F或J，就判定有沒有擊中拍子
        if (notesDisplaying.Count > 0 && (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.J))) {
            //sfx.Play();
            int hit = notesDisplaying.Peek().GetComponent<NoteController>().checkIfHit();
            if (hit == 1 || hit == 2) {
                GameObject this_note = notesDisplaying.Dequeue();
                Destroy(this_note);
                calculate_score(hit);
            }
        }
        //如果超出邊界，便自動摧毀，並判定miss
        if (notesDisplaying.Count > 0) {
            if (notesDisplaying.Peek().GetComponent<NoteController>().checkIfHit() == 3) {
                GameObject this_note = notesDisplaying.Dequeue();
                Destroy(this_note);
                calculate_score(3);
            }
            
        }
        
        
    }
    // calculate_score 用以計算擊中拍點的分數，type = 1 是 perfect，type = 2 是 great，type = 3 是 miss
    public void calculate_score(int type) {
        if (type == 1) {
            ModifyHp(10);
            score += (float) (100 * (1 + combo * 0.02));
            combo++;
        } else if (type == 2) {
            ModifyHp(5);
            score += (float) (50 * (1 + combo * 0.02));
            combo++;
        } else if (type == 3) {
            ModifyHp(-10);
            combo = 0;
        }
        //print("score : " + score + " combo : " + combo + "\ntype : "+ type);
    }
    

}
