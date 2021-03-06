using UnityEngine;
using System.Collections;
using RunMinigames.Interface.Characters;

namespace RunMinigames.Mechanics.Interactable
{
    public abstract class InteractableItem : MonoBehaviour
    {
        [SerializeField] protected float SpeedCharacter;
        [SerializeField] protected float LongTimeBehaviour;
        [SerializeField] float speedItemMove;

        protected MeshRenderer mesh;
        protected SphereCollider sphereCollider;
        protected bool isObstacles;

        MoveItem moveItem;

        protected void Awake()
        {
            if (!isObstacles)
            {
                moveItem = gameObject.AddComponent<MoveItem>();
                moveItem.speed = speedItemMove;
            }

            mesh = GetComponent<MeshRenderer>();
            sphereCollider = GetComponent<SphereCollider>();
        }

        protected void Update()
        {
            if (!isObstacles) Destroy(gameObject, 60f);
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICharacterItem character))
                StartCoroutine(OnCollideBehaviour(character));
        }

        public abstract IEnumerator OnCollideBehaviour(ICharacterItem character);
    }
}


