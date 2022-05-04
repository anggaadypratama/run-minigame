using UnityEngine;
using System;
using Photon.Pun;
using RunMinigames.Interface.Characters;
using RunMinigames.Manager.Characters;
using TMPro;


namespace RunMinigames.Mechanics.Characters.Info
{
    public abstract class CharactersInfo : MonoBehaviour
    {
        [Header("Character Information")]
        public static CharactersInfo instance;
        public int CharaID;
        public string CharaName;
        // public TMP_Text CharaViewName;
        public int CharaScore;

        [Header("Check point system")]
        bool isRaceCompleted = false;
        int passedCheckPointNumber = 0;
        int numberOfPassedCheckpoints = 0;
        float timeAtLastPassCheckpoint = 0;
        int lapsCompleted = 0;
        public event Action<CharactersInfo> OnPassCheckpoint;

        protected float timer = 0f;
        protected PhotonView view;
        protected CheckGameType type;

        protected virtual void OnCollideCheckpoint(Collider coll, Action UpdateScore, Action<Checkpoint> UpdatePodium)
        {
            if (coll.CompareTag("Checkpoint"))
            {
                if (isRaceCompleted) return;

                var checkpoint = coll.GetComponent<Checkpoint>();

                TryGetComponent(out ICharacterItem myCharacter);


                if (passedCheckPointNumber + 1 == checkpoint.checkPointNumber)
                {
                    passedCheckPointNumber = checkpoint.checkPointNumber;
                    numberOfPassedCheckpoints++;
                    timeAtLastPassCheckpoint = Time.time;
                    CharaScore++;

                    UpdateScore();

                    if (checkpoint.isFinishLine)
                    {
                        if (myCharacter is Player)
                        {
                            passedCheckPointNumber = 0;
                            lapsCompleted++;
                        }

                        UpdatePodium(checkpoint);
                        myCharacter.MaxSpeed = 2;
                    }

                    OnPassCheckpoint?.Invoke(this);
                }

                if (checkpoint.stopAfterFinish) myCharacter.CanMove = false;
            }
        }


        protected abstract void CheckTypeUpdateScore();
        protected abstract void CheckTypeUpdatePodium(Checkpoint checkpoint);
    }
}
