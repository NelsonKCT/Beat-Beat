using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float hp;
    float maxHp;
    public float attack;
    // Start is called before the first frame update
    SpriteRenderer spriteRenderer;
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
        if (hp < 0) {
            spriteRenderer.enabled = false;
        }
    }
    public void modifyHP(float num) {
        hp = ((hp + num) > maxHp) ? maxHp : hp + num;
    }
}
