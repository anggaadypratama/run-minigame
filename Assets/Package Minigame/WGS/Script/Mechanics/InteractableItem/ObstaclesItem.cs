using System.Collections;
using UnityEngine;
using RunMinigames.Interface.Characters;
using Photon.Pun;
using RunMinigames.Manager.Characters;

namespace RunMinigames.Mechanics.Interactable
{
    public class ObstaclesItem : InteractableItem
    {

        CheckGameType type;
        PhotonView view;

        private void Awake()
        {
            GameObject gameManager = GameObject.Find("GameManager");
            type = gameManager.GetComponent<CheckGameType>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICharacterItem character))
            {
                StartCoroutine(OnCollideBehaviour(character));

                // if (character is Player player)
                // {
                //     view = player?.GetComponent<PhotonView>();
                //     Zetcode_CameraFollowPlayerFixed.cameraFollow.isShake = (type.IsMultiplayer && view.IsMine) || type.IsSingleplayer;
                // }
            }
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
