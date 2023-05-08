using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcPowerUpController : MonoBehaviour
{
    private NpcBombController _bombController;
    private NavMeshAgent _navMeshAgent;
    
    private void Awake()
    {
        _bombController = GetComponent<NpcBombController>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("POW"))
        {
            switch (other.tag)
            {
                case "Bomb":
                    _bombController.IncrementBombCount();
                    break;
                case "Flame":
                    _bombController.IncreaseRadius();
                    break;
                case "Skate":
                    _navMeshAgent.speed++;
                    break;
                case "Slow":
                    _navMeshAgent.speed--;
                    break;
                case "Spew":
                    _bombController.ActivateSpew();
                    break;
            }
            Destroy(other.gameObject);
        }
    }
}
