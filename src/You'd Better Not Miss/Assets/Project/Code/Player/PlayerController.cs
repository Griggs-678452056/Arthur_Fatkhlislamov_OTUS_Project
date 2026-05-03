using UnityEngine;

namespace Code
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _walkSpeed = 3f;
        [SerializeField] private float _runSpeed = 6f;
        [SerializeField] private float _gravity = -9.81f;

        private CharacterController _controller;
        private PlayerInputHandler _input;

        private Vector3 _velocity;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<PlayerInputHandler>();
        }

        private void Update()
        {
            Move();
            ApplyGravity();
        }

        private void Move()
        {
            Vector2 input = _input.MoveInput;

            Vector3 move = transform.right * input.x + transform.forward * input.y;

            float speed = _input.SprintHeld ? _runSpeed : _walkSpeed;

            _controller.Move(move * speed * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            if (_controller.isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            _velocity.y += _gravity * Time.deltaTime;
            _controller.Move(_velocity * Time.deltaTime);
        }
    }
}