using UnityEngine;

public class Destructible : MonoBehaviour
{
    private PowerDropper _dropper;

    private void Start()
    {
        _dropper = GetComponent<PowerDropper>();
    }

    public void DestroyObject()
    {
        _dropper.DropPowerUp();
        Destroy(gameObject);
    }
}
