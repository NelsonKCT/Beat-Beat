using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkills : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    PlayerController playerController;
    EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        playerController = player.GetComponent<PlayerController>();
        enemyController = enemy.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    public void activate_heavy_notes()//劉丞浩你就繼續不做特殊音符，我直接改成每一下都有效果，打到great通通扣血
    {
        playerController.GreatDamage=0.3f;
    }
    public void disable_heavy_notes()//關掉上面的技能
    {
        playerController.GreatDamage=0f;
    }
}
