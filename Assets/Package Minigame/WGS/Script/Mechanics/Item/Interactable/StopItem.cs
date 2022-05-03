using System.Collections;
using UnityEngine;
using RunMinigames.Interface.Characters;

namespace RunMinigames.Mechanics.Interactable
{
    public class StopItem : InteractableItem
    {
        private new void Awake() => base.Awake();
        private void Update() => Destroy(gameObject, 60f);

        public override IEnumerator OnCollideBehaviour(ICharacterItem character)
        {
            mesh.enabled = false;
            sphereCollider.enabled = false;

            character.CanMove = false;
            character.IsItemSpeedActive = false;
            character.CharSpeed = 0;

            yield return new WaitForSeconds(LongTimeBehaviour);

            character.CanMove = true;
            Destroy(gameObject);
        }
    }
}
