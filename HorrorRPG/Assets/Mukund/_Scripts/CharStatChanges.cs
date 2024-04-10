using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStatChanges : MonoBehaviour
{
    //public CharacterScriptableObjectCreator characterStats;
    //health
    [SerializeField]private float maxHealth;
    private float currentHealth;
    //sanity
    [SerializeField] float maxSanity;
    private float currentSanity;
    //hunger
    [SerializeField] float maxHunger;
    private float currentHunger;

    [Header("Events")]
    public GameEvent onHealthUIChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
        currentSanity = maxSanity;
        currentHunger = maxHunger;

    }

    public void HealthChanger(Component sender, object data)
    {
        if (sender == gameObject.GetComponent<Collider>())
        {
            currentHealth += (float)data;
            print("health changed:" + currentHealth);
            onHealthUIChanged.TriggerEvent(this, currentHealth);
        }
    }


}
