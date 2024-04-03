using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.SetCombatState(true);
            GameManager.Instance.SetGridPosition(other.transform.position);
        }
    }
    
}
