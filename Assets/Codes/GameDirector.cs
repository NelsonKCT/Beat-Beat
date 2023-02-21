using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    /*
    combo : combo
    notesDisplaying : 目前出現在遊戲內的 notes
    player : 玩家物件
    enemy : 敵人物件
    playerController : 操控玩家的主程式
    enemyController : 操控敵人的主程式
    */
    AudioSource sfx;
    float combo;
    float damage;
    public Queue<GameObject> notesDisplaying;

    GameObject player;
    GameObject enemy;
    GameObject comboText;
    GameObject judgeText;
    GameObject damageText;
    PlayerController playerController;
    EnemyController enemyController;
    void Start()
    {
        combo = 0f;
        damage = 0f;
        notesDisplaying = new Queue<GameObject>();
        sfx = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        playerController = player.GetComponent<PlayerController>();
        enemyController = enemy.GetComponent<EnemyController>();
        comboText = GameObject.Find("Combo");
        judgeText = GameObject.Find("Judge");
        damageText = GameObject.Find("Damage");
    }

    
    void Update()
    {
        //如果按下F或J，就判定有沒有擊中拍子
        if (notesDisplaying.Count > 0 && (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.J))) {
            //sfx.Play();
            int hit = notesDisplaying.Peek().GetComponent<NoteController>().checkIfHit();
            if (hit == 1 || hit == 2 || hit == 3) {
                GameObject this_note = notesDisplaying.Dequeue();
                Destroy(this_note);
                calculate_result(hit);
            }
        }
        //如果超出邊界，便自動摧毀，並判定miss
        if (notesDisplaying.Count > 0) {
            if (notesDisplaying.Peek().GetComponent<NoteController>().checkIfHit() == 4) {
                GameObject this_note = notesDisplaying.Dequeue();
                Destroy(this_note);
                calculate_result(4);
            }
            
        }
        
        
    }
    /* calculate_result 用以計算擊中拍點的結果，
    type = 0 是尚未進判定點
    type = 1 是 perfect(+-30ms)，打出全額傷害並且回復20%自身攻擊的血量
    type = 2 是 great(+-50ms)，打出60%傷害並且回復10%自身攻擊的血量
    type = 3 是 good(+-80ms)，打出30%傷害且受到50%敵方攻擊的傷害並使combo歸0
    type = 4 是 miss(>80ms)，受到敵方全額傷害且combo歸0
    預設每 1 combo +2% 傷害
    */
    
    
    public void calculate_result(int type) {
        if (type == 1) {
            playerController.modifyHP((float)(playerController.attack * playerController.PerfectRegen * (1 + playerController.ComboScale * combo)));
            enemyController.modifyHP((float)(-playerController.attack *playerController.PerfectDamage* (1 + playerController.ComboScale * combo)));
            damage = (float)(playerController.attack * (1 + playerController.ComboScale * combo));
            combo++;
            judgeText.GetComponent<Text>().text = "Perfect";
        } else if (type == 2) {
            playerController.modifyHP((float)(playerController.attack * playerController.GreatRegen * (1 + playerController.ComboScale * combo)));
            enemyController.modifyHP((float)(-playerController.attack * playerController.GreatDamage * (1 + playerController.ComboScale * combo)));
            playerController.modifyHP((float)(-enemyController.attack * playerController.GreatEnemyDamage));
            damage = (float)(playerController.attack * playerController.GreatDamage * (1 + playerController.ComboScale * combo));
            combo++;
            judgeText.GetComponent<Text>().text = "Great";
        } else if (type == 3) {
            enemyController.modifyHP((float)(-playerController.attack * playerController.GoodDamage));
            playerController.modifyHP((float)(-enemyController.attack * playerController.GoodEnemyDamage));
            damage = (float)(playerController.attack * playerController.GoodDamage);
            combo = 0;
            judgeText.GetComponent<Text>().text = "Good";
        } else if (type == 4) {
            enemyController.modifyHP((float)(-playerController.attack * playerController.MissDamage));
            playerController.modifyHP((float)-enemyController.attack * playerController.MissEnemyDamage);
            damage = (float)(playerController.attack * playerController.MissDamage);
            combo = 0;
            judgeText.GetComponent<Text>().text = "Miss";
        }
        damage = Mathf.Round(damage); 
        comboText.GetComponent<Text>().text = combo.ToString() + " Combo";
        damageText.GetComponent<Text>().text = damage.ToString() + " !";
        print("combo : " + combo + "\ntype : "+ type);
    }
    

}
