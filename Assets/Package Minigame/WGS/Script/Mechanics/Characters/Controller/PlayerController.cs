using Photon.Pun;
using UnityEngine;

namespace RunMinigames.Mechanics.Characters.Controller
{
    public class PlayerController : CharactersController
    {

        protected PhotonView view;

        private void Start()
        {
            view = GetComponent<PhotonView>();
            Rb = GetComponent<Rigidbody>();
        }

        public override void Jump()
        {
            if (IsGrounded && CanMove)
            {
                Debug.Log(Vector3.up * JumpForce);
                Rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                IsGrounded = false;
                TargetAnimator.SetTrigger("Jump");
            }
        }

        private void OnCollisionEnter(Collision collider)
        {

            IsGrounded = collider.gameObject.tag == "Ground";
            TargetAnimator.SetBool("isGrounded", IsGrounded);
        }
    }
}

