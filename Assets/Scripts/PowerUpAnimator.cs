using UnityEngine;

public class PowerUpAnimator : MonoBehaviour
{
    [Header("Sine Wave")] 
    public float amplitude = 0.5f;
    public float frequency = 3f;

    private Vector3 _initPos;

    private void Start()
    {
        _initPos = transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(_initPos.x,
            Mathf.Sin(Time.time * frequency) * amplitude + _initPos.y, _initPos.z);
        transform.Rotate(new Vector3(0, 100f, 0) * Time.deltaTime);
    }
}
