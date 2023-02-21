using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /*
    hp : 目前血量
    maxHp : 最大血量
    attack : 攻擊力
    spriteRenderer : 控制物件的顯示
    */
    float hp;
    public float maxHp;
    public float attack;
    public float ComboScale = 0.02f;
    public float PerfectDamage = 1f;
    public float PerfectRegen = 0.2f;
    public float GreatDamage = 0.6f;
    public float GreatEnemyDamage = 0f;
    public float GreatRegen = 0.1f;
    public float GoodDamage = 0.3f;
    public float GoodEnemyDamage = 0.5f;
    public float MissDamage = 0f;
    public float MissEnemyDamage = 1f;
    SpriteRenderer spriteRenderer;
    [SerializeField] Healthbar _healthbar;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHp = 500;
        hp = maxHp;
        attack = 30;
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
        _healthbar.setHealth((int)hp);
    }
}
