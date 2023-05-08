using UnityEngine;

public class BombDeployer : MonoBehaviour
{
    public int bombCount;
    public float radius = 10f;
    public GameObject bombPrefab;
    public Transform orientation;

    public delegate void UpdateUI(int bombCount, float bombRadius);
    public UpdateUI UIDelegate;

    private bool _spew;
    private float _spewTimer;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        UpdateBombUi();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && bombCount > 0)
        {
            DeployBomb();
        }

        if (_spew)
        {
            while (bombCount > 0)
                DeployBomb();
            _spewTimer -= Time.deltaTime;
            if (_spewTimer <= 0)
                _spew = false;
        }
    }

    public void ActivateSpew()
    {
        _spewTimer = 15;
        _spew = true;
    }

    private void DeployBomb()
    {
        _audioSource.pitch = Random.Range(0.5f, 1.5f);
        _audioSource.Play();
        bombCount--;
        UpdateBombUi();
        var tf = transform;
        var bomb = Instantiate(bombPrefab, tf.position + orientation.forward * 2, tf.rotation);
        var controller = bomb.GetComponent<BombController>();
        controller.SetRadius(radius);
        controller.Delegate = IncrementBombCount;
    }

    public void IncrementBombCount()
    {
        bombCount++;
        UpdateBombUi();
    }

    private void UpdateBombUi()
    {
        UIDelegate(bombCount, radius);
    }

    public void IncreaseRadius()
    {
        radius += 2f;
        UpdateBombUi();
    }
}
