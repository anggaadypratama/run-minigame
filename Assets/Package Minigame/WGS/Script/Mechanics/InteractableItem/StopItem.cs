using System.Collections;
using UnityEngine;
using RunMinigames.Interface.Characters;

namespace RunMinigames.Mechanics.Interactable
{
    public class StopItem : InteractableItem
    {
        private void Update() => Destroy(gameObject, 60f);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICharacterItem character))
                StartCoroutine(OnCollideBehaviour(character));
        }

        public override IEnumerator OnCollideBehaviour(ICharacterItem character)
        {
            character.CanMove = false;
            character.IsItemSpeedActive = false;
            character.CharSpeed = 0;

            Destroy(gameObject);

            yield return new WaitForSeconds(LongTimeBehaviour);

            character.CanMove = true;
        }
    }
}
