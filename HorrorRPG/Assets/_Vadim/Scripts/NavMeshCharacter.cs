using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshCharacter : ICharacterMovement
{
    private NavMeshAgent agent;
    private BodyRotation _rotationLogic;
    private Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right, Vector3.forward + Vector3.right, Vector3.forward + Vector3.left, Vector3.back + Vector3.right, Vector3.back + Vector3.left };

    public override bool IsMoving => agent.velocity.magnitude > .5f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _rotationLogic = GetComponentInChildren<BodyRotation>();
        Deactivate();
    }
    public void MoveTowards(Vector3 targetPos)
    {

        if(Mathf.Abs(Vector3.Distance(transform.position, targetPos)) > .25f) agent.SetDestination(targetPos);

        if (IsMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            _rotationLogic.RotateTowards(targetRotation);
        }
        else
        {
            _rotationLogic.AutoRotate();
        }
    }

    public override void Activate()
    {
        agent.enabled = true;
    }

    public override void Deactivate()
    {
        agent.enabled = false;
    }
}
