using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    TextMeshProUGUI label;
    [SerializeField] EnemyHealth health;
    private void OnEnable()
    {
        label = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (label == null)
        {
            label = GetComponent<TextMeshProUGUI>();
            return;
        }
        label.text = health.currentHealth + "/" + health.maxHealth;
        if (health.currentHealth < 2)
        {
            label.color = Color.red;
        }
        else label.color = Color.white;
    }
}
