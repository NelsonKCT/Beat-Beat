using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    /*
    combo : combo
    notesDisplaying : 目前出現在遊戲內的 notes
    */
    AudioSource sfx;
    float combo;
    public Queue<GameObject> notesDisplaying;

    GameObject player;
    GameObject enemy;
    PlayerController playerController;
    EnemyController enemyController;
    void Start()
    {
        combo = 0f;
        notesDisplaying = new Queue<GameObject>();
        sfx = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        playerController = player.GetComponent<PlayerController>();
        enemyController = enemy.GetComponent<EnemyController>();
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
                calculate_result(hit);
            }
        }
        //如果超出邊界，便自動摧毀，並判定miss
        if (notesDisplaying.Count > 0) {
            if (notesDisplaying.Peek().GetComponent<NoteController>().checkIfHit() == 3) {
                GameObject this_note = notesDisplaying.Dequeue();
                Destroy(this_note);
                calculate_result(3);
            }
            
        }
        
        
    }
    // calculate_score 用以計算擊中拍點的分數，type = 1 是 perfect，type = 2 是 great，type = 3 是 miss
    public void calculate_result(int type) {
        if (type == 1) {
            playerController.modifyHP((float)(playerController.attack * 0.2 * (1 + 0.02 * combo)));
            enemyController.modifyHP((float)(-playerController.attack * (1 + 0.02 * combo)));
            combo++;
        } else if (type == 2) {
            playerController.modifyHP((float)(playerController.attack * 0.1 * (1 + 0.02 * combo)));
            enemyController.modifyHP((float)(-playerController.attack * 0.5 * (1 + 0.02 * combo)));
            combo++;
        } else if (type == 3) {
            playerController.modifyHP(-enemyController.attack);
            combo = 0;
        }
        //print("score : " + score + " combo : " + combo + "\ntype : "+ type);
    }
    

}
