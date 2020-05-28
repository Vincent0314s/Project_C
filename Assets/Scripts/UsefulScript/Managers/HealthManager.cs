using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthSystem {
    private Image HP_Ture;
    private Image HP_Effect;
    private float maxHP;
    private float currentHP;
    public Action OnDead;

    public HealthSystem(float hp,Image _trueHP,Image _effectHP) {
        HP_Ture = _trueHP;
        HP_Effect = _effectHP;
        maxHP = hp;
        ResetHealth();
    }
    public void ResetHealth() {
        currentHP = maxHP;
        UpdateHealthImage();
        UpdateHealthEffectImage();
    }

    public void TakeDamage(float damageAmount) {
        currentHP -= damageAmount;
        if (currentHP <= 0) {
            currentHP = 0;
            OnDead?.Invoke();
        }
        UpdateHealthImage();
    }

    private void UpdateHealthImage() {
        HP_Ture.fillAmount = GetHealthNormalized();
    }

    private void UpdateHealthEffectImage()
    {
        HP_Effect.fillAmount = GetHealthNormalized();
    }

    public void TakenDamageEffect() {
        if (HP_Ture.fillAmount < HP_Effect.fillAmount)
        {
            HP_Effect.fillAmount -= 0.25f * Time.deltaTime;
        }
        else {
            HP_Effect.fillAmount = HP_Ture.fillAmount;
        }
    }

    public float GetHealthNormalized() {
        return currentHP / maxHP;
    }
}

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private Image playerHP;
    [SerializeField]
    private Image playerHPEffect;
    [SerializeField]
    private Image enemyHP;
    [SerializeField]
    private Image enemyHPEffect;

    public static HealthSystem playerHealth;
    public static HealthSystem enemyHealth;


    private void LateUpdate()
    {
        playerHealth.TakenDamageEffect();
        enemyHealth.TakenDamageEffect();

    }
}
