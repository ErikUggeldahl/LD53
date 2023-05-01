using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    enum AnimState
    {
        Idle = 0,
        Walking = 1,
    }

    private const float MOVEMENT_FORCE = 6f;
    private const float MOVEMENT_MAX_SPEED = 12f;
    private const float JUMP_FORCE = 10f;
    private const float ROTATION_SPEED = 45f;
    private readonly Vector3 _xzMask = new Vector3(1f, 0f, 1f);

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] private JumpTrigger jumpTrigger;

    public float horizontalSpeedPub;
    
    private int _stateParam;
    private int _jumpedParam;

    private void Start()
    {
        _stateParam = Animator.StringToHash("State");
        _jumpedParam = Animator.StringToHash("Jumped");
    }

    private void FixedUpdate()
    {
        var direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        
        var horizontalSpeed = Vector3.Scale(rb.velocity, _xzMask).magnitude;
        horizontalSpeedPub = horizontalSpeed;
        if (horizontalSpeed < MOVEMENT_MAX_SPEED)
        {
            var force = Mathf.Clamp(MOVEMENT_FORCE * Time.fixedTime, 0f, MOVEMENT_MAX_SPEED - horizontalSpeed);
            rb.AddRelativeForce(force * direction, ForceMode.VelocityChange);
        }
        
        SetAnim(horizontalSpeed > 0f ? AnimState.Walking : AnimState.Idle);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpTrigger.Grounded)
        {
            rb.AddForce(Vector3.up * JUMP_FORCE, ForceMode.VelocityChange);
            anim.SetTrigger(_jumpedParam);
        }
        
        var rotation = Input.GetAxis("Rotation");
        transform.Rotate(transform.up * (rotation * ROTATION_SPEED * Time.deltaTime));
    }
    
    private void SetAnim(AnimState state) => anim.SetInteger(_stateParam, (int)state);
}
