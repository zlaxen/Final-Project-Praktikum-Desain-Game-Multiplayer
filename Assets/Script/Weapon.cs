using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Photon.Pun;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    //public Transform Firepoint;
    public GameObject BulletPrefab;
    //public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButton("Shoot"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        PhotonNetwork.Instantiate(BulletPrefab.name, BulletPrefab.transform.position, BulletPrefab.transform.rotation);
        //rb.velocity = BulletPrefab.transform.right;
    }
}
