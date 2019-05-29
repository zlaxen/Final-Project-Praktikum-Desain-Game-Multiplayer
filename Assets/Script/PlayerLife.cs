using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerLife : MonoBehaviourPun
{
    Image Life;
    public float maxHealth;
    public float Health;

    public bool damaged = false;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 100f;
        Life = GetComponent<Image>();
        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Life.fillAmount = Health / maxHealth;

        if(damaged == true)
        {
            Health -= 10f;
            damaged = false;
        }
    }

    [PunRPC]
    public void GetDamage(){
        damaged = true;
    }


}
