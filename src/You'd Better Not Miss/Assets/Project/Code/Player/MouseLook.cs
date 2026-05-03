using UnityEngine;

namespace Code
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private Transform _playerBody;
        [SerializeField] private float _sensivity = 100f;

        private PlayerInputHandler _input;

        private float _xRotation = 0f;

        private void Awake()
        {
            _input = GetComponentInParent<PlayerInputHandler>();
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            Vector2 look = _input.LookInput;

            float mouseX = look.x * _sensivity * Time.deltaTime;
            float mouseY = look.y * _sensivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

            _playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}