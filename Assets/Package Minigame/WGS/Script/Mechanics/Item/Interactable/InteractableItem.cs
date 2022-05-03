using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using RunMinigames.Manager.Characters;
using RunMinigames.Interface.Characters;


//TODO: CREATE CHARACTERS CONTROLLER
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
            moveItem = gameObject.AddComponent<MoveItem>();
            moveItem.isObstacles = isObstacles;
            moveItem.speed = speedItemMove;
        }

        //! ganti pake interface
        public abstract IEnumerator OnCollideBehaviour(ICharacterItem character);
    }
}


