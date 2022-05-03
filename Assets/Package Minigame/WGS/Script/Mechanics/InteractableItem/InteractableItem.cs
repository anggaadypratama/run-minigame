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

        protected MeshRenderer mesh;
        protected SphereCollider sphereCollider;

        //! ganti pake interface
        public abstract IEnumerator OnCollideBehaviour(ICharacterItem character);
    }
}


