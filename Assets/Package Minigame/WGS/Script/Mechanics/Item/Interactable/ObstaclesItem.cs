using System.Collections;
using UnityEngine;
using RunMinigames.Interface.Characters;
// using Photon.Pun;

namespace RunMinigames.Mechanics.Interactable
{
    public class ObstaclesItem : InteractableItem
    {
        CheckGameType type;
        // PhotonView view;

        private new void Awake()
        {
            isObstacles = true;
            GameObject gameManager = GameObject.Find("GameManager");
            type = gameManager.GetComponent<CheckGameType>();

            base.Awake();
        }

        public override IEnumerator OnCollideBehaviour(ICharacterItem character)
        {
            character.IsItemSpeedActive = false;
            character.MaxSpeed = SpeedCharacter;

            yield return new WaitForSeconds(LongTimeBehaviour);

            character.MaxSpeed = 10;
        }
    }
}
