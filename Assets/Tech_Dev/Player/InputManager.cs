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
        public bool Skill;

        [SerializeField] private float _heavyAttackTime;
        private float _heavyAttackTimeDelta;
        private bool _attackPressed;


        private void Update()
        {
            Jump = false;
            //MeleeAttack = false;
            //HeavyAttack = false;
        }

        public void OnMove(InputValue value)
        {
            Move = value.Get<Vector2>();
        }

        public void OnJump(InputValue value)
        {
            Jump = Keyboard.current.spaceKey.wasPressedThisFrame || (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame);
        }

        public void OnAttack(InputValue value)
        {
            MeleeAttack = value.isPressed;
        }

        public void OnSpecialAttack(InputValue value)
        {
            HeavyAttack = value.isPressed;
        }

        public void OnInteract(InputValue value)
        {
            Interact = value.isPressed;
        }

        public void OnDash(InputValue value)
        {
            Dash = value.isPressed;
        }

        public void OnSkill(InputValue value)
        {
            Skill = value.isPressed;
        }

        public void OnCloseUI(InputValue value)
        {
	        print("ee");
            GameManager.CloseAllUIs();
        }
    }
}
