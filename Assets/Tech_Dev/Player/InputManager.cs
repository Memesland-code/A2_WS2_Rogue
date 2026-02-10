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

        public void OnMove(InputValue value)
        {
            Move = value.Get<Vector2>();
        }

        public void OnJump(InputValue value)
        {
            Jump = Keyboard.current.spaceKey.wasPressedThisFrame;
        }

        public void OnAttack(InputValue value)
        {
            Attack = value.isPressed;
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
