using UnityEngine;

public class PowerDropper : MonoBehaviour
{
    [Header("PowerUps")] 
    public GameObject bomb;
    public GameObject flame;
    public GameObject skate;
    public GameObject slow;

    private bool _hasDropped;
    
    private GameObject GetPowerUp()
    {
        var number = Random.Range(0, 10);
        return number switch
        {
            0 or 1 => bomb,
            2 or 3 => flame,
            4 => skate,
            5 => slow,
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
