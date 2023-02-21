using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    GameObject player;
    GameObject enemy;
    PlayerController playerController;
    EnemyController enemyController;
    void start()
    {
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        playerController = player.GetComponent<PlayerController>();
        enemyController = enemy.GetComponent<EnemyController>();
    }
    // Start is called before the first frame update
   public void activate_combo_3percent()//這個技能會讓combo變成每 1 combo +3%傷害，其實大概從函數名稱就可以看的出來
   {
    playerController.ComboScale=0.03f;
   }
   public void disable_combo_3percent()//關掉上面的技能
   {
    playerController.ComboScale=0.02f;
   }   

   public void activate_good_damage_up()//這個技能會讓打到good的傷害從30%變成45%，提升了50%，非常佛心
   {
    playerController.GoodDamage=0.45f;
   }
   public void disable_good_damage_up()//關掉上面的技能
   {
    playerController.GoodDamage=0.3f;
   }

   public void activate_perfectionist()//打到perfect的傷害提升了20%，但是great和good的傷害降低50%，給自詡為大佬的音遊玩家
   {
    playerController.PerfectDamage = 1.2f;
    playerController.GreatDamage = 0.3f;
    playerController.GoodDamage = 0.15f;
   }
   public void disable_perfectionist()//關掉上面的技能
   {
    playerController.PerfectDamage = 1f;
    playerController.GreatDamage = 0.6f;
    playerController.GoodDamage = 0.3f;
   }
}
