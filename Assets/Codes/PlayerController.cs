using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float hp;
    float maxHp;
    public float attack;
    SpriteRenderer spriteRenderer;
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
        if (hp < 0) {
            spriteRenderer.enabled = false;
        }
        
    }

    public void modifyHP(float num) {
        hp = ((hp + num) > maxHp) ? maxHp : hp + num;
    }
}
