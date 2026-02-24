using System.Collections;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Splines.Interpolators;
using UnityEngine.UI;

public class UpdateHealthBar : MonoBehaviour
{
    private Image healthBar;
    private float duration = 1f;
    private float time = 0f;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        PlayerHealth.OnHealthAmountChanged += TriggerUpdate;
    }

    public void TriggerUpdate(int amount)
    {
        StopAllCoroutines();
        StartCoroutine(UpdateHealth(amount));
    }
    public IEnumerator UpdateHealth(int currentHealth)
    {
        time = 0f;
        float targetAmount = (float)currentHealth / PlayerHealth.MaxHealth;

        while(healthBar.fillAmount != targetAmount)
        {
            time += Time.deltaTime;
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, targetAmount, time / duration);
            yield return null;
        }
    }

}
