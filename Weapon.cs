using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Photon.Pun;
using UnityEngine.UI;

public class Weapon : MonoBehaviourPun, IPunObservable
{
    // Start is called before the first frame update
    public GameObject BulletPrefab;
    public GameObject GunPlace;
    public Rigidbody2D rb;

    public PhotonView pv;

    private Vector3 smoothMove;

    //public Rigidbody2D rb;

    // Update is called once per frame

    void Update()
    {
        if (photonView.IsMine)
        {
            Shoot();
        }
        else
        {
            smoothMovement();
        }
    }

    private void smoothMovement()
    {
        //PhotonNetwork.Instantiate(BulletPrefab.transform.name, GunPlace.transform.position, BulletPrefab.transform.rotation);
        //transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10);
    }

    private void Shoot()
    {
        if (SummonAmmo.YesShoot == true)
        {
            Shooting();
        }    
    }

    void Shooting()
    {
        //Instantiate(GunPlace, GunPlace.transform);
        PhotonNetwork.Instantiate(BulletPrefab.transform.name, GunPlace.transform.position, BulletPrefab.transform.rotation);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
        {
            smoothMove = (Vector3)stream.ReceiveNext();
        }
    }
}
