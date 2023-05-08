using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BombDeployer _deployer;
    private PlayerMovement _movement;
    private AnimStateController _animState;

    private bool _canKick;

    public LayerMask bombMask;
    
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
                    _movement.ChangeMoveSpeed(-1);
                    break;
                case "Spew":
                    _deployer.ActivateSpew();
                    break;
                case "Kick":
                    _canKick = true;
                    break;
            }
            Destroy(other.gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bomb") && _canKick)
        {
            var bomb = other.gameObject.GetComponent<BombController>();
            if (!bomb.HasGrounded()) return;
            Vector3 dir = (other.transform.position - transform.position).normalized;
            dir.y = 0;
            other.rigidbody.AddForce(dir * 700, ForceMode.Impulse);
        }
    }
}
