using System;
using UnityEngine;

public class NpcBombController : MonoBehaviour
{
    public GameObject bombPrefab;
    private int _bombCount = 2;
    private float _bombRadius = 10;

    private bool _spew;
    private float _spewTimer = 15;

    private void Update()
    {
        if (_spew)
        {
            while (_bombCount > 0) DropBomb();
            _spewTimer -= Time.deltaTime;
            if (_spewTimer <= 0) _spew = false;
        }
    }

    public void DropBomb()
    {
        if (_bombCount <= 0) return;
        _bombCount--;
        var tf = transform;
        var bomb = Instantiate(bombPrefab, tf.position + tf.forward * 2, tf.rotation);
        var controller = bomb.GetComponent<BombController>();
        controller.SetRadius(_bombRadius);
        controller.Delegate = IncrementBombCount;
    }

    public void IncrementBombCount()
    {
        _bombCount++;
    }

    public float GetRadius()
    {
        return _bombRadius;
    }

    public void IncreaseRadius()
    {
        _bombRadius += 2;
    }

    public void ActivateSpew()
    {
        _spew = true;
    }
}
