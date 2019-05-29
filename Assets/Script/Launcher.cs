using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{

    // Use this for initialization

    public GameObject connectedScreen;
    public GameObject disconnectedScreen;
    public GameObject PleaseWait;
    //[SerializeField] Slider ss;

    public void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        disconnectedScreen.SetActive(true);
        PleaseWait.SetActive(false);
    }

    public override void OnJoinedLobby()
    {
        if (disconnectedScreen.activeSelf)
        {
            disconnectedScreen.SetActive(false);
            PleaseWait.SetActive(false);
        }
        PleaseWait.SetActive(false);
        connectedScreen.SetActive(true);
    }
}
