using UnityEngine;

namespace RunMinigames.Mechanics.Characters.Controller
{
    public abstract class CharactersController : MonoBehaviour
    {
        [Header("Base Character")]
        public GameObject Character;
        public Animator TargetAnimator;
        protected Rigidbody Rb;


        [Header("Character Speed")]
        public float MaxSpeed = 10f;

        [Range(0.0F, 10f)] public float CharSpeed = 0f;
        public bool CanMove = true;
        public bool IsItemSpeedActive = false;


        [Header("Character Jump")]
        [Space(10f)]
        public float JumpForce = 6f;
        protected bool IsGrounded;

        public virtual void Movement()
        {
            if (CharSpeed >= 0 && !IsItemSpeedActive)
            {

                CharSpeed -= 0.01f;
            }
            else if (CharSpeed >= 0 && IsItemSpeedActive)
            {
                TargetAnimator.SetBool("isRunning", true);
            }
            else
            {
                TargetAnimator.SetBool("isRunning", false);
            }

            if (CharSpeed >= MaxSpeed)
            {
                CharSpeed = MaxSpeed;
            }

            Character.transform.position += new Vector3(0, 0, CharSpeed * Time.deltaTime);
        }


        public virtual void Running(float runSpeed = 1f)
        {
            if (CanMove)
            {
                TargetAnimator.SetBool("isRunning", true);

                if (!IsItemSpeedActive)
                {
                    CharSpeed += runSpeed;
                }
            }
            else
            {
                CharSpeed = 0;
                TargetAnimator.SetBool("isRunning", false);
            }
        }

        public abstract void Jump();
    }
}
