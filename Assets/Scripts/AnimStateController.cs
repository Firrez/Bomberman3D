using System;
using UnityEngine;

public class AnimStateController : MonoBehaviour
{
    public Transform orientation;

    private Transform _model;
    
    private Animator _animator;
    
    private static readonly int InputW = Animator.StringToHash("w");
    private static readonly int InputA = Animator.StringToHash("a");
    private static readonly int InputS = Animator.StringToHash("s");
    private static readonly int InputD = Animator.StringToHash("d");
    private static readonly int Dying = Animator.StringToHash("isDying");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _model = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool(InputW, Input.GetKey("w"));
        _animator.SetBool(InputA, Input.GetKey("a"));
        _animator.SetBool(InputS, Input.GetKey("s"));
        _animator.SetBool(InputD, Input.GetKey("d"));
    }

    private void FixedUpdate()
    {
        _model.rotation = orientation.rotation;
    }

    public void Die()
    {
        _animator.SetBool(Dying, true);
    }
}
