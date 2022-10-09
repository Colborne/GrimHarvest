using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class StatsManager : MonoBehaviour
{
    public bool isTakingDamage = false;
    public bool isInvincible = false;

    [Header("Health")]
    public int maxHealth;
    public int currentHealth;

    [Header("Stamina")]
    public float maxStamina;
    public float currentStamina;
    public float staminaRegenAmount = 30f;
    private float staminaRegenTimer = 0f;

    [Header("Stats")]

    [Header("Attack Bonuses")]

    [Header("Components")]
    public HealthBar healthBar;
    public StaminaBar staminaBar;
    AnimatorManager animatorManager;
    InputManager inputManager;

    private void Awake() 
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
        healthBar = FindObjectOfType<HealthBar>();
        staminaBar = FindObjectOfType<StaminaBar>();
        inputManager = GetComponent<InputManager>();
    }

    private void Update() 
    { 
        UpdateStats();
    }

    public void TakeDamage(int damage)
    {
        GetComponent<DamageCollider>().DisableDamageCollider();
        currentHealth = currentHealth - damage;
        healthBar.SetCurrentHealth(currentHealth);

        if(currentHealth > 0)
        {
            animatorManager.PlayTargetAnimation("Damage", true);
            animatorManager.animator.SetBool("isTakingDamage", true);
        }

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            animatorManager.PlayTargetAnimation("Dying", true);
        } 
    }

    public void Heal(int amount)
    {
        currentHealth = currentHealth + amount;
        if (currentHealth > 100)
            currentHealth = 100;
        healthBar.SetCurrentHealth(currentHealth);
    }

    public void UseStamina(float cost)
    {
        currentStamina = currentStamina - cost;
        staminaBar.SetCurrentStamina(currentStamina);
    }

    public void RegenerateStamina()
    {
        if(inputManager.isInteracting || inputManager.isSprinting)
        {
            staminaRegenTimer = 0;
        }
        else
        {
            staminaRegenTimer += Time.deltaTime;
            if(currentStamina < maxStamina && staminaRegenTimer > 1f)
            {
                currentStamina += staminaRegenAmount * Time.deltaTime;
                staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
            }
        }
    }

    void UpdateStats()
    {
        if(currentStamina < 0f)
            currentStamina = 0f;

        isTakingDamage = animatorManager.animator.GetBool("isTakingDamage");
        isInvincible = animatorManager.animator.GetBool("isInvincible");
    }
}
