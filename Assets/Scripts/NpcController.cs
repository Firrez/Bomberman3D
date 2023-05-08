using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public LayerMask ground, enemyMask, destructible, powerUp, bombMask;

    private bool _dead;
    
    private NavMeshAgent _navMeshAgent;
    private NpcBombController _npcBombController;
    private NpcPowerUpController _npcPowerUpController;
    private NpcAnimationController _npcAnimationController;
    
    //Roaming
    private Vector3 _destination;
    private bool _destSet;
    public float destRange = 20f;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _npcBombController = GetComponent<NpcBombController>();
        _npcPowerUpController = GetComponent<NpcPowerUpController>();
        _npcAnimationController = GetComponent<NpcAnimationController>();
    }

    private void Update()
    {
        if (_dead)
        {
            _navMeshAgent.isStopped = true;
            return;
        }

        var position = transform.position;
        var bombInRange = Physics.CheckSphere(position, 20, bombMask);
        var enemyInRange = Physics.CheckSphere(position, 20, enemyMask);
        var powerUpInRange = Physics.CheckSphere(position, 20, powerUp);
        var wallInRange = Physics.CheckSphere(position, 20, destructible);

        
        if (bombInRange) RunForCover();
        else if (enemyInRange) HuntEnemy();
        else if (powerUpInRange) CollectPowerUp();
        else if (wallInRange) BlowWall();
        else Roam();
    }

    private void RunForCover()
    {
        var position = transform.position;
        var bombs = Physics.OverlapSphere(position, 20, bombMask);
        foreach (var bomb in bombs)
        {
            var dist = Vector3.Distance(position, bomb.transform.position);
            var radius = bomb.GetComponent<BombController>().GetRadius();
            if (dist < radius + 3f)
            {
                var dir = position - bomb.transform.position;
                var newPos = position + dir;
                _navMeshAgent.SetDestination(newPos);
            }
        }
    }

    private void HuntEnemy()
    {
        var position = transform.position;
        var enemy = Physics.OverlapSphere(position, 20, enemyMask).First();
        var dist = Vector3.Distance(position, enemy.transform.position);
        
        if (dist > _npcBombController.GetRadius() / 2)
        {
            _navMeshAgent.SetDestination(enemy.transform.position);
        }
        else _npcBombController.DropBomb();
    }

    private void CollectPowerUp()
    {
        var position = transform.position;
        var power = Physics.OverlapSphere(position, 20, powerUp).First();
        _navMeshAgent.SetDestination(power.transform.position);
    }

    private void BlowWall()
    {
        var position = transform.position;
        var walls = Physics.OverlapSphere(position, 20, destructible);
        var wall = ClosestWall(walls);
        var dist = Vector3.Distance(position, wall.ClosestPoint(position));

        if (dist > 3f)
        {
            _navMeshAgent.SetDestination(wall.ClosestPoint(position));
        }
        else _npcBombController.DropBomb();
    }

    private Collider ClosestWall(Collider[] walls)
    {
        var position = transform.position;
        Collider closestWall = null;

        foreach (var wall in walls)
        {
            if (closestWall is null)
            {
                closestWall = wall;
                continue;
            }

            var newDist = Vector3.Distance(position, wall.ClosestPoint(position));
            var currentDist = Vector3.Distance(position, closestWall.ClosestPoint(position));

            if (newDist < currentDist) closestWall = wall;
        }

        return closestWall;
    }

    private void Roam()
    {
        if (!_destSet)
        {
            GetDestination();
        }

        if (_destSet)
        {
            _navMeshAgent.SetDestination(_destination);
        }

        var distance = transform.position - _destination;
        if (distance.magnitude < 3f) _destSet = false;
    }
    
    private void GetDestination()
    {
        var randomZ = Random.Range(-destRange, destRange);
        var randomX = Random.Range(-destRange, destRange);

        var pos = transform.position;
        _destination = new Vector3(pos.x + randomX, pos.y, pos.z + randomZ);
        
        if (Physics.Raycast(_destination, -transform.up, 2f, ground))
        {
            // Checks if position is inside wall.
            var impassable = Physics.CheckSphere(_destination, 0);
            if (impassable) return;
            _destSet = true;
        }
    }

    public void Die()
    {
        _dead = true;
        _npcAnimationController.Die();
        Invoke(nameof(Cleanup), 5);
    }

    private void Cleanup()
    {
        Destroy(gameObject);
    }
}
