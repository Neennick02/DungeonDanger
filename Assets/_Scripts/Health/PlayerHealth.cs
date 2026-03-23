using System;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    [SerializeField] private PlayerObject playerObject;

    public static event Action<int> OnHealthAmountChanged;
    public static event Action OnDeath;

    private PlayerMovement movement;
    private CharacterController controller;


    private bool isDefending;

    #region OnEnable
    private void OnEnable()
    {
        HeartPickup.OnPickup += AddHealth;
        SaveStatueInteractable.OnSavePlayerData += SaveData;
        GameManager.OnLoad += LoadData;
        PlayerInventory.OnDefendActive += Defend;
    }

    private void OnDisable()
    {
        HeartPickup.OnPickup -= AddHealth;
        SaveStatueInteractable.OnSavePlayerData -= SaveData;
        GameManager.OnLoad -= LoadData;
        PlayerInventory.OnDefendActive += Defend;
    }
    #endregion
    protected override void Start()
    {
        movement = GetComponent<PlayerMovement>();
        controller = GetComponent<CharacterController>();

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
        currentHealth += amount;
        OnHealthAmountChanged?.Invoke(currentHealth);
    }

    public override void DrainHealth(int amount)
    {
        if (isDefending) return;

        currentHealth -= amount;
        OnHealthAmountChanged?.Invoke(currentHealth);

        if (currentHealth > 0) FlashRed();

        if (currentHealth <= 0 && !isDead)
        {
            Die();
            isDead = true;
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

                    if (damage > 0)
                        DrainHealth((int)damage);
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
