using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BombDeployer _deployer;
    private PlayerMovement _movement;
    private AnimStateController _animState;
    
    private void Start()
    {
        _deployer = GetComponent<BombDeployer>();
        _movement = GetComponent<PlayerMovement>();
        _animState = GetComponent<AnimStateController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("POW"))
        {
            switch (other.tag)
            {
                case "Bomb":
                    _deployer.IncrementBombCount();
                    break;
                case "Flame":
                    _deployer.IncreaseRadius();
                    break;
                case "Skate":
                    _movement.ChangeMoveSpeed(1);
                    break;
                case "Slow":
                    _movement.ChangeMoveSpeed(-3);
                    break;
                case "Spew":
                    _deployer.ActivateSpew();
                    break;
            }
            Destroy(other.gameObject);
        }
    }
}
