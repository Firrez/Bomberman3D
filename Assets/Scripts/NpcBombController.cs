using UnityEngine;

public class NpcBombController : MonoBehaviour
{
    public GameObject bombPrefab;
    public int bombCount = 2;
    public float bombRadius = 10;
    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }

    public void DropBomb()
    {
        if (bombCount <= 0) return;
        bombCount--;
        var tf = transform;
        var bomb = Instantiate(bombPrefab, tf.position + tf.forward * 2, tf.rotation);
        var controller = bomb.GetComponent<BombController>();
        controller.SetRadius(bombRadius);
        controller.Delegate = IncrementBombCount;
    }

    public void IncrementBombCount()
    {
        bombCount++;
    }
}
