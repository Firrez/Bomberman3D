using System;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [Header("Bomb settings")]
    public float delay;
    
    
    [Header("Bomb Visuals")]
    public GameObject effect;

    private float _countdown;
    private float _radius;
    private bool _hasExploded;

    public LayerMask bomb;
    public LayerMask targets;
    
    public delegate void CountDelegate();
    public CountDelegate Delegate;

    private UIManager _uiManager;

    private void Awake()
    {
        _uiManager = GameObject.Find("Player").GetComponent<UIManager>();
    }

    void Start()
    {
        _countdown = delay;
    }
    
    void Update()
    {
        _countdown -= Time.deltaTime;

        if (_countdown <= 0 && !_hasExploded)
        {
            Explode();
        }
    }

    public void SetRadius(float r)
    {
        _radius = r;
    }

    public float GetRadius()
    {
        return _radius;
    }
    
    private void Explode()
    {
        _hasExploded = true;
        var pos = transform;
        var position = pos.position;
        Instantiate(effect, position, pos.rotation);

        Collider[] results = Physics.OverlapSphere(position, _radius, targets);
        var hits = new List<Destructible>();

        foreach (var col in results)
        {
            var direction = col.ClosestPoint(position) - position;
            var ray = new Ray(position, direction.normalized);
            if (Physics.Raycast(ray, out var hit, maxDistance: _radius, bomb))
            {
                if (hit.collider.CompareTag("Destructible"))
                {
                    var dest = hit.collider.GetComponent<Destructible>();
                    hits.Add(dest);
                }

                if (hit.collider.CompareTag("Character"))
                {
                    hit.collider.GetComponent<NpcController>().Die();
                    _uiManager.ReportDead(hit.collider.name);
                }

                if (hit.collider.CompareTag("Player"))
                {
                    hit.collider.GetComponentInChildren<AnimStateController>().Die();
                    hit.collider.GetComponent<PlayerMovement>().SetDead();
                }
            }
        }
        
        foreach (var hit in hits) hit.DestroyObject();

        Delegate();
        Destroy(gameObject);
    }
}
