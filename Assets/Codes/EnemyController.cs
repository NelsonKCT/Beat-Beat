using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /*
    hp : 目前血量
    maxHp : 最大血量
    attack : 攻擊力
    spriteRenderer : 控制物件的顯示
    */
    float hp;
    float maxHp;
    public float attack;
    // Start is called before the first frame update
    SpriteRenderer spriteRenderer;
    [SerializeField] Enemy_Healthbar enemy_healthbar;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHp = 2000;
        hp = maxHp;
        attack = 50;
    }

    // Update is called once per frame
    void Update()
    {
        //如果血量歸0，便使人物消失
        if (hp < 0) {
            spriteRenderer.enabled = false;
        }
    }
    //修改血量
    public void modifyHP(float num) {
        hp = ((hp + num) > maxHp) ? maxHp : hp + num;
        enemy_healthbar.Enemy_setHealth((int)hp);
    }
}
