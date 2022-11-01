using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    /*
    score : 分數
    combo : combo
    */
    float score;
    float combo;
    
    void Start()
    {
        combo = 0f;
        score = 0f;
    }

    
    void Update()
    {

    }
    // calculate_score 用以計算擊中拍點的分數，type = 1 是 perfect，type = 2 是 great，type = 3 是 miss
    public void calculate_score(int type) {
        if (type == 1) {
            score += (float) (100 * (1 + combo * 0.02));
            combo++;
        } else if (type == 2) {
            score += (float) (50 * (1 + combo * 0.02));
            combo++;
        } else if (type == 3) {
            combo = 0;
        }
        print("score : " + score + " combo : " + combo + "\ntype : "+ type);
    }


}
