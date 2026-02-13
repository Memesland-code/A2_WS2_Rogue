using UnityEngine;
using UnityEngine.InputSystem;

namespace Tech_Dev.Player
{
    public class InputManager : MonoBehaviour
    {
        public Vector2 Move;
        public bool Jump;
        public bool Attack;
        public bool Interact;
        public bool Dash;
        public bool MeleeAttack;
        public bool HeavyAttack;

        [SerializeField] private float _heavyAttackTime;
        private float _heavyAttackTimeDelta;
        private bool _attackPressed;

        private void LateUpdate()
        {
            if (_attackPressed)
            {
                _heavyAttackTimeDelta -= Time.deltaTime;
            }
            
            if (_heavyAttackTimeDelta <= 0 && (Mouse.current.leftButton.wasReleasedThisFrame || Gamepad.current.buttonWest.wasReleasedThisFrame))
            {
                HeavyAttack = true;
                _heavyAttackTimeDelta = _heavyAttackTime;
            }
            else if (_heavyAttackTimeDelta > 0 && (Mouse.current.leftButton.wasReleasedThisFrame || Gamepad.current.buttonWest.wasReleasedThisFrame))
            {
                MeleeAttack = true;
                _heavyAttackTimeDelta = _heavyAttackTime;
            }
        }

        private void Update()
        {
            Jump = false;
            MeleeAttack = false;
            HeavyAttack = false;
        }

        public void OnMove(InputValue value)
        {
            Move = value.Get<Vector2>();
        }

        public void OnJump(InputValue value)
        {
            Jump = Keyboard.current.spaceKey.wasPressedThisFrame || Gamepad.current.buttonSouth.wasPressedThisFrame;
        }

        public void OnAttack(InputValue value)
        {
            _attackPressed = value.isPressed;
        }

        public void OnInteract(InputValue value)
        {
            Interact = value.isPressed;
        }

        public void OnDash(InputValue value)
        {
            Dash = value.isPressed;
        }
    }
}
