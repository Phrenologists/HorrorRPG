using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CharacterState
{
    Idle,
    Walk
}
public class CharacterController : MonoBehaviour
{
    private CharacterState state = CharacterState.Idle;
    private BodyRotation rotationLogic;
    private SpriteManager spriteManager;
    private StrictMovement characterMovement;

    public Transform cameraFollowObject;
    public BodyRotation RotationLogic => rotationLogic;
    private void Awake()
    {
        rotationLogic = GetComponentInChildren<BodyRotation>();
        spriteManager = GetComponentInChildren<SpriteManager>();
        characterMovement = GetComponent<StrictMovement>();
        spriteManager.BodyLogic = rotationLogic.transform;
    }

    private void Update()
    {
        state = characterMovement.IsMoving ? CharacterState.Walk : CharacterState.Idle;
        spriteManager.CharacterState = state.ToString();
    }
    private void OnEnable()
    {
        characterMovement.enabled = true;
    }
    private void OnDisable()
    {
        characterMovement.enabled = false;

    }
}
