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
    public Text Name;
    public GameObject connectedScreen;
    public GameObject disconnectedScreen;
    //[SerializeField] Slider ss;

    void Start()
    {
        Name.text = "Please Connect To Master";    
    }
    public void OnClick_ConnectBtn()
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
        Name.gameObject.SetActive(false);
        disconnectedScreen.SetActive(true);
    }

    public override void OnJoinedLobby()
    {
        if (disconnectedScreen.activeSelf)
        {
            disconnectedScreen.SetActive(false);
        }
        Name.gameObject.SetActive(false);
        connectedScreen.SetActive(true);
    }
}
