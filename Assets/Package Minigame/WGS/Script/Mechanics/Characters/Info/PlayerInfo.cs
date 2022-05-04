using UnityEngine;
using Photon.Pun;

namespace RunMinigames.Mechanics.Characters.Info
{
    public class PlayerInfo : CharactersInfo
    {
        private void Awake()
        {
            instance = this;
            view = GetComponent<PhotonView>();
            type = GameObject.Find("GameManager").GetComponent<CheckGameType>();

            CharaName = type.IsMultiplayer && !type.IsSingleplayer ? view.Owner.NickName : gameObject.name;
            CharaID = type.IsMultiplayer && !type.IsSingleplayer ? view.Owner.ActorNumber - 1 : 0;

            if (type.IsMultiplayer)
            {
                view.RPC("SetAvatarIndex", RpcTarget.AllBuffered, PlayerPrefs.GetInt("playerAvatar"));
            }

            // CharaViewName.text = CharaName;
        }

        private void Start()
        {
            if (type.IsMultiplayer && !type.IsSingleplayer)
            {
                view.RPC("UpdatePlayerName", RpcTarget.AllBuffered, CharaID, CharaName);
                view.RPC("SetPlayerName", RpcTarget.AllBuffered, CharaName);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            OnCollideCheckpoint(
                other,
                UpdateScore: () => CheckTypeUpdateScore(),
                UpdatePodium: (checkpoint) => CheckTypeUpdatePodium(checkpoint)
            );
        }

        protected override void CheckTypeUpdatePodium(Checkpoint checkpoint)
        {
            if (type.IsMultiplayer && !type.IsSingleplayer)
            {
                view.RPC(
                    "UpdatePodiumList", RpcTarget.AllBuffered, //RPC Arguments
                    checkpoint.isFinishLine, CharaID, timer, CharaName //Method Arguments
                    );
            }
            else
            {
                GameObject finishUI = GameObject.FindGameObjectWithTag("Finish UI");
                finishUI.GetComponent<MultiplayerFinishManager>()
                    .Finish(checkpoint.isFinishLine, CharaID, timer, CharaName);
            }
        }

        protected override void CheckTypeUpdateScore()
        {
            if (type.IsMultiplayer && !type.IsSingleplayer)
            {
                view.RPC(
                    "UpdatePlayerScore", RpcTarget.AllBuffered, //RPC Arguments
                    CharaName, CharaScore //Method Arguments
                    );
            }
            else
            {
                // LeaderboardManager.instance.UpdatePlayerScore(CharaName, CharaScore);
            }
        }

        private void Update()
        {
            timer += Time.deltaTime;
        }

        [PunRPC]
        void UpdatePodiumList(bool isFinish, int id, float timer, string playerName)
        {
            GameObject finishUI = GameObject.FindGameObjectWithTag("Finish UI");
            finishUI.GetComponent<MultiplayerFinishManager>().Finish(isFinish, id, timer, playerName);
        }

        [PunRPC]
        void UpdatePlayerScore(string name, int score)
        {
            LeaderboardManager.instance.UpdatePlayerScore(name, score); //disini rpc nya
        }

        [PunRPC]
        void UpdatePlayerName(int id, string name) => LeaderboardManager.instance.UpdatePlayerName(id, name);


        [PunRPC]
        void SetPlayerName(string name) => LeaderboardManager.instance.SetPlayerName(name);


        [PunRPC]
        void SetAvatarIndex(int index)
        {
            GameObject manager = GameObject.Find("NPCSpawner");
            NPCSpawner run = manager.GetComponent<NPCSpawner>();
            run.SetPlayerIndex(index);
        }
    }
}