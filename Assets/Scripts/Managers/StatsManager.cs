using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class StatsManager : MonoBehaviour
{
    [Header("State Bools")]
    public bool isTakingDamage = false;
    public bool isInvincible = false;
    public bool isAttacking = false;

    [Header("Health")]
    public int maxHealth;
    public int currentHealth;

    [Header("Stamina")]
    public float maxStamina;
    public float currentStamina;
    public float staminaRegenAmount = 30f;
    public float rollCost = 15f;
    public float actionCost = 7f;
    float staminaRegenTimer = 0f;

    [Header("Stats")]

    [Header("Attack Bonuses")] 
    public int heavyModifier = 1;

    [Header("Components")]
    public HealthBar healthBar;
    public StaminaBar staminaBar;
    AnimatorManager animatorManager;
    InputManager inputManager;
    EquipmentManager equipmentManager;

    private void Awake() 
    {
        animatorManager = GetComponentInChildren<AnimatorManager>();
        healthBar = GetComponent<HealthBar>();
        staminaBar = GetComponent<StaminaBar>();
        inputManager = GetComponent<InputManager>();
        equipmentManager = GetComponent<EquipmentManager>();
    }

    private void Update() 
    { 
        RegenerateStamina();
        UpdateStats();
    }

    public void TakeDamage(int damage)
    {
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

    public void UseLightStamina()
    {
        currentStamina = currentStamina - equipmentManager.rightWeapon.lightCost;
        staminaBar.SetCurrentStamina(currentStamina);
    }
    public void UseHeavyStamina()
    {
        currentStamina = currentStamina - equipmentManager.rightWeapon.heavyCost;
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
            if(currentStamina < maxStamina && staminaRegenTimer > .25f)
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
        isAttacking = animatorManager.animator.GetBool("isAttacking");

        if(isTakingDamage)
            GetComponent<DamageCollider>().DisableDamageCollider();

        if(currentHealth <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
