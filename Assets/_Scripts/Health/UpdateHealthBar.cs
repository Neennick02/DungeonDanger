using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthBar : MonoBehaviour
{
    private Image healthBar;
    private float duration = 1f;
    private float time = 0f;
    [SerializeField] private PlayerObject playerObject;
    private int startHealth;
    private void OnEnable()
    {
        PlayerHealth.OnHealthAmountChanged += TriggerUpdate;
    }

    private void OnDisable()
    {
        PlayerHealth.OnHealthAmountChanged -= TriggerUpdate;
    }
    private void Start()
    {
        startHealth = playerObject.StartHealth;
        healthBar = GetComponent<Image>();
    }

    public void TriggerUpdate(int amount)
    {
        StopAllCoroutines();
        StartCoroutine(UpdateHealth(amount));
    }
    public IEnumerator UpdateHealth(int currentHealth)
    {
        time = 0f;
        float targetAmount = (float)currentHealth / startHealth;

        while(healthBar.fillAmount != targetAmount)
        {
            time += Time.deltaTime;
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, targetAmount, time / duration);
            yield return null;
        }
    }

}
