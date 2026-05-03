using UnityEngine;
using UnityEngine.InputSystem;

namespace Code
{
    public class PlayerInputHandler : MonoBehaviour // ввод с мыши и клавиатуры
    {
        private PlayerInputActions _input;

        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }

        public bool SprintHeld { get; private set; }
        public bool FirePressed { get; private set; }
        public bool ReloadPressed { get; private set; }
        public bool MeleePressed { get; private set; }
        public bool PausePressed { get; private set; }
        public bool QuickSavePressed { get; private set; }
        public bool QuickLoadPressed { get; private set; }

        private void Awake()
        {
            _input = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _input.Enable();

            _input.Player.Move.performed += OnMove;
            _input.Player.Move.canceled += OnMove;

            _input.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
            _input.Player.Look.canceled += _ => LookInput = Vector2.zero;

            _input.Player.Sprint.performed += _ => SprintHeld = true;
            _input.Player.Sprint.canceled += _ => SprintHeld = false;

            _input.Player.Fire.performed += _ => FirePressed = true;
            _input.Player.Fire.canceled += _ => FirePressed = false;

            _input.Player.Reload.performed += _ => ReloadPressed = true;
            _input.Player.Reload.canceled += _ => ReloadPressed = false;

            _input.Player.Melee.performed += _ => MeleePressed = true;
            _input.Player.Melee.canceled += _ => MeleePressed = false;

            _input.Player.Pause.performed += _ => PausePressed = true;

            _input.Player.QuickSave.performed += _ => QuickSavePressed = true;

            _input.Player.QuickLoad.performed += _ => QuickLoadPressed = true;
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            MoveInput = ctx.ReadValue<Vector2>();
        }

        public bool ConsumePause()
        {
            if(!PausePressed)
            {
                return false;
            }

            PausePressed = false;
            return true;
        }

        public bool ConsumeQuickSave()
        {
            if (!QuickSavePressed)
            {
                return false;
            }

            QuickSavePressed = false;
            return true;
        }

        public bool ConsumeQuickLoad()
        {
            if (!QuickLoadPressed)
            {
                return false;
            }

            QuickLoadPressed = false;
            return true;
        }
    }
}