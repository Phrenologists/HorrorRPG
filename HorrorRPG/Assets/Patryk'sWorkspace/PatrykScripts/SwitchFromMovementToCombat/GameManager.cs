using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public MovementScript movementScript;
    public PlayerMove playerMove;
    public EnemyMove enemyMove;
    public EnemyCollisions enemyCollisions;
    public GridInitialiser grid;

    public bool turnBasedCombatInitiated = false;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        SetCombatState(false);
    }
    private void Update()
    {


            //movementScript.enabled = !turnBasedCombatInitiated;
            //enemyMove.enabled = turnBasedCombatInitiated;
            //playerMove.enabled = turnBasedCombatInitiated;
            //grid.gameObject.SetActive(turnBasedCombatInitiated); 
    }
    public void SetCombatState(bool isActive)
    {
        turnBasedCombatInitiated = isActive;

        movementScript.enabled = !isActive;
        enemyMove.enabled = isActive;
        playerMove.enabled = isActive;
        grid.gameObject.SetActive(isActive);
    }

    public void SetGridPosition(Vector3 newPos)
    {
        grid.CurrentPosition = newPos;
    }
    
    public void React()
    {
        print("REACT");
    }
}
