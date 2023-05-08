using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float drag;

    public Transform orientation;

    private float _hInput;
    private float _vInput;

    private Vector3 _direction;

    private Rigidbody _rb;
    
    public delegate void UpdateUI(float speed);
    public UpdateUI UIDelegate;

    private UIManager _uiManager;

    private bool _dead;

    private void Awake()
    {
        _uiManager = GetComponent<UIManager>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rb.freezeRotation = true;
        _rb.drag = drag;
        UpdateUi();
    }

    private void Update()
    {
        if (!_dead)
        {
            UserInput();
            SpeedControl();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void ChangeMoveSpeed(int change)
    {
        moveSpeed += change;
        UpdateUi();
    }

    private void UserInput()
    {
        _hInput = Input.GetAxisRaw("Horizontal");
        _vInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        _direction = orientation.forward * _vInput + orientation.right * _hInput;
        _rb.AddForce(_direction.normalized * (moveSpeed * 10f), ForceMode.Force);
    }

    private void SpeedControl()
    {
        var velocity = _rb.velocity;
        var flatVel = new Vector3(velocity.x, 0f, velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            var limitVel = flatVel.normalized * moveSpeed;
            _rb.velocity = new Vector3(limitVel.x, 0f, limitVel.z);
        }
    }

    public void SetDead()
    {
        _uiManager.ReportDead("Player");
        _dead = true;
        Invoke(nameof(CleanUp), 5);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }

    private void UpdateUi()
    {
        UIDelegate(moveSpeed);
    }
}
