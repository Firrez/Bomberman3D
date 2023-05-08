using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    public GameObject grass;
    
    private const float StartZ = -69.5f;
    private const float StartX = -49.5f;

    private void Start()
    {
        for (var x = StartX; x < 60.5f; x++)
        {
            for (var z = StartZ; z < 80.5f; z++)
            {
                var xOffset = Random.Range(-0.3f, 0.3f);
                var zOffset = Random.Range(-0.3f, 0.3f);
                var pos = new Vector3(x + xOffset, 0, z + zOffset);
                Instantiate(grass, pos, grass.transform.rotation);
            }
        }
    }
}
