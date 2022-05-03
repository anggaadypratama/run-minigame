using System.Collections;
using UnityEngine;
using RunMinigames.Interface.Characters;

namespace RunMinigames.Mechanics.Interactable
{
    public class SpeedItem : InteractableItem
    {

        float PrevPlayerSpeed;
        float PrevNPCSpeed;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICharacterItem character))
                StartCoroutine(OnCollideBehaviour(character));
        }

        private void Update() => Destroy(gameObject, 60f);

        public override IEnumerator OnCollideBehaviour(ICharacterItem character)
        {
            if (character.CanMove)
            {
                PrevPlayerSpeed = character.CharSpeed;
                character.CharSpeed += SpeedCharacter;
                character.IsItemSpeedActive = true;

                Destroy(gameObject);

                yield return new WaitForSeconds(LongTimeBehaviour);

                character.CharSpeed = PrevPlayerSpeed;
                character.IsItemSpeedActive = false;
            }

            character.IsItemSpeedActive = false;
            yield return new WaitForSeconds(0);
        }
    }
}
