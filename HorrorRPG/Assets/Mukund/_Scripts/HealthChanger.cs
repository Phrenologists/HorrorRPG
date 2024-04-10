using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChanger : MonoBehaviour
{
    public GameEvent onCharHealthChanged;
    public float healthChange;

    private void OnTriggerEnter(Collider other)
    {
        
        onCharHealthChanged.TriggerEvent(other, healthChange);
    }

}
