using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_HealthChanger : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public void UpdateHealthUI(Component sender, object currentHealth)
    {
        string containerName = gameObject.transform.parent.parent.name;
        if (containerName.Contains(sender.name))
        {
            healthText.text = currentHealth.ToString();
        }
    }
}

