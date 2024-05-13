using UnityEngine;

public class InputMover : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Animator            _animator;

    private InputManager _input;
    private Vector3 _animVelocity;
    private Vector3 _moveDirection;

    public float speed;
    public float acceleration;
    public float deacceleration;
    public float cameraRadius;

    void Start() => _input = new InputManager();

    public void Move()
    {
        Vector2 hv = _input.KeyMovement;
        float mx = _input.MouseMotion;
        _moveDirection = (transform.right * hv.x + transform.forward * hv.y).normalized;
        _animVelocity.x = Mathf.Lerp(_animVelocity.x, -hv.x, _moveDirection.x > 0f ? acceleration : deacceleration);
        _animVelocity.z = Mathf.Lerp(_animVelocity.z, -hv.y, _moveDirection.z > 0f ? acceleration : deacceleration);
        
        transform.Rotate(Vector3.up * mx);

        if (!_controller.isGrounded)
            _moveDirection += Physics.gravity;

        if (_moveDirection.magnitude != 0.0f)
            _controller.Move(_moveDirection * speed * 0.005f);

        _animator.SetFloat("Horizontal Speed", -_animVelocity.x);
        _animator.SetFloat("Vertical Speed", -_animVelocity.z);
    }
}
