using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerDropper : MonoBehaviour
{
    [Header("PowerUps")] 
    public GameObject bomb;
    public GameObject flame;
    public GameObject skate;
    public GameObject slow;
    public GameObject spew;
    public GameObject kick;

    private bool _hasDropped;
    
    private GameObject GetPowerUp()
    {
        var number = Random.Range(0, 15);
        return number switch
        {
            0 or 1 or 2 => bomb,
            3 or 4 or 5 => flame,
            6 => skate,
            7 => slow,
            8 => spew,
            9 or 10 => kick,
            _ => null
        };
    }

    public void DropPowerUp()
    {
        if (_hasDropped) return;
        _hasDropped = true;
        var power = GetPowerUp();
        if (power is null) return;
        var tf = transform;
        Instantiate(power, tf.position, tf.rotation);
    }
}
