using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.pitch = Random.Range(0.5f, 1.5f);
        _audioSource.Play();
        Invoke(nameof(Cleanup), 3);
    }

    private void Cleanup()
    {
        Destroy(gameObject);
    }
}
