using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy_Healthbar : MonoBehaviour
{
    Slider enemy_healthSlider;

    private void Start(){
        enemy_healthSlider = GetComponent<Slider>();
    }

    public void Enemy_setMaxHealth(int maxHealth){
        enemy_healthSlider.maxValue = maxHealth;
        enemy_healthSlider.value = maxHealth;
    }
    public void Enemy_setHealth(int health){
        enemy_healthSlider.value = health;
    }
}