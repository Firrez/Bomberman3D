using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcAnimationController : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    
    private static readonly int Dying = Animator.StringToHash("isDying");
    private static readonly int ForwardSpeed = Animator.StringToHash("Speed");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _animator.SetFloat(ForwardSpeed, _navMeshAgent.velocity.magnitude);
    }

    public void Die()
    {
        _animator.SetBool(Dying, true);
    }
}
