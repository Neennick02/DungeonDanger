using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    [SerializeField] private PlayerObject playerObject;
    [SerializeField] private MusicManager musicManager;
    private PlayerInventory inventory;

    public static event Action<int> OnHealthAmountChanged;
    public static event Action OnDeath;

    private PlayerMovement movement;
    private CharacterController controller;

    [Header("Sound Effects")]
    [SerializeField] private List<AudioClip> healSounds;
    [SerializeField] private List<AudioClip> potionSounds;

    [SerializeField] private List<AudioClip> hurtSounds;
    [SerializeField] private List<AudioClip> deathSounds;

    [SerializeField] private List<AudioClip> shieldSounds;
    [SerializeField] private List<AudioClip> fallDamageSounds;

    private bool isDefending;

    #region OnEnable
    private void OnEnable()
    {
        HeartPickup.OnPickup += AddHealth;
        SaveStatueInteractable.OnSavePlayerData += SaveData;
        GameManager.OnLoad += LoadData;
        PlayerInventory.OnDefendActive += Defend;
        PlayerInputHandler.OnDrinkPotion += UsePotion;

    }

    private void OnDisable()
    {
        HeartPickup.OnPickup -= AddHealth;
        SaveStatueInteractable.OnSavePlayerData -= SaveData;
        GameManager.OnLoad -= LoadData;
        PlayerInventory.OnDefendActive -= Defend;
        PlayerInputHandler.OnDrinkPotion -= UsePotion;

    }
    #endregion
    protected override void Start()
    {
        movement = GetComponent<PlayerMovement>();
        controller = GetComponent<CharacterController>();
        inventory = GetComponent<PlayerInventory>();

        //set health
        maxHealth = playerObject.StartHealth;
        currentHealth = maxHealth;

        //update health bar
        OnHealthAmountChanged?.Invoke(currentHealth);

        //choose first material
        if (meshes.Count > 0)
            standardMat = meshes[0].material;
    }
    public override void AddHealth(int amount)
    {
        AudioManager.Instance.PlayClip(healSounds);
        currentHealth += amount;
        OnHealthAmountChanged?.Invoke(currentHealth);

        //stop heart sound effect
        if (currentHealth >= maxHealth / 3)
        {
            musicManager.ToggleHeartBeat(false);
        }
    }

    public override void DrainHealth(int amount)
    {
        if (isDefending)
        {
            AudioManager.Instance.PlayClip(shieldSounds);
            return;
        }

        AudioManager.Instance.PlayClip(hurtSounds);

        currentHealth -= amount;
        OnHealthAmountChanged?.Invoke(currentHealth);

        if (currentHealth > 0) FlashRed();

        if (currentHealth <= 0 && !isDead)
        {
            Die();
            isDead = true;
        }

        //play heart sound effect
        if(currentHealth <= maxHealth / 3)
        {
            musicManager.ToggleHeartBeat(true);
        }
    }
    private void UsePotion()
    {
        if (inventory.potionAmount >= 1)
        {
            AddHealth(inventory.potionHealAmount);
            AudioManager.Instance.PlayClip(potionSounds);
        }
    }
    private void Defend(bool active)
    {
        isDefending = active;
    }
    protected override void Update()
    {
        if (movement == null || controller == null) return;

        if (movement._verticalVelocity < -2)
        {
            if (!isFalling)
            {
                isFalling = true;
                highestPoint = transform.position.y;
            }
        }


        //hadle landing
        if (isFalling)
        {
            if (controller.isGrounded)
            {
                //calculate distance
                float fallDist = highestPoint - transform.position.y;

                if (fallDist > minFallHeight)
                {
                    //calculate damage
                    float damage = (fallDist * minFallHeight) * FallDamageMultiplier;

                    if ((int)damage > 0)
                    { 
                        DrainHealth((int)damage);
                        AudioManager.Instance.PlayClip(fallDamageSounds);
                    }
                }
                isFalling = false;
            }
        }
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Void")) return;

        if (isFalling)
        {
            float fallDist = highestPoint - transform.position.y;

            if (fallDist > minFallHeight)
            {
                float damage = (fallDist * minFallHeight) * FallDamageMultiplier;
                OnHealthAmountChanged?.Invoke((int)damage);

            }
            isFalling = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Void")) return;
    }
    protected override void Die()
    {
        OnDeath?.Invoke();
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("Health", currentHealth);
    }
    private void LoadData()
    {
        currentHealth = PlayerPrefs.GetInt("Health", 25);
    }
}
