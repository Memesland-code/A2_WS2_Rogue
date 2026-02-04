using UnityEngine;
using UnityEngine.InputSystem;

namespace TECH.Player
{
    public class InputManager : MonoBehaviour
    {
        public Vector2 Move;
        public bool Jump;
        public bool Attacking;

        public void OnMove(InputValue value)
        {
            Move = value.Get<Vector2>();
        }

        public void OnJump(InputValue value)
        {
            Jump = value.isPressed;
        }

        public void OnAttack(InputValue value)
        {
            Attacking = value.isPressed;
        }
    }
}
