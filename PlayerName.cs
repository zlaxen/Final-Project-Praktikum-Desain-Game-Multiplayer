using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerName : MonoBehaviour
{
    public InputField namePLayer;
    public Button setName;
    // Start is called before the first frame update
    public void OnTFChange()
    {
        if(namePLayer.text.Length > 2)
        {
            setName.interactable = true;
        }
        else
        {
            setName.interactable = false;
        }
    }

    public void OnClick_SetName()
    {
        PhotonNetwork.NickName = namePLayer.text;
    }
}
