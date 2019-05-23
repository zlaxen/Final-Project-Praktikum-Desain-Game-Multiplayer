using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class UIHandler : MonoBehaviourPunCallbacks
{
    public InputField createRoom;
    public InputField joinRoom;

    void Start()
    {

    }

    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoom.text, null);
    }

    public void OnClick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoom.text, new RoomOptions { MaxPlayers = 4 }, null);
    }

    public override void OnJoinedRoom()
    {
        print("Room Joined Success");
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("Failed To Join Room" + returnCode + "Message" + message);
    }

    //private void Update()
    //{
        //Debug.Log(PhotonNetwork.GetPing());
    //}
}
