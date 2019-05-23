using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.CrossPlatformInput;

public class Ammo : MonoBehaviourPun, IPunObservable
{
    // Start is called before the first frame update
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject Bullet;

    public PhotonView pv;

    private Vector3 smoothMove;

    void Update()
    {
        if (photonView.IsMine)
        {
            Shoot();
        }
        else
        {
            SmoothMovement();
        }
    }

    private void SmoothMovement()
    {
        if (Myplayer.Right == true)
        {
            rb.velocity = transform.right * speed;
        }
        else if(Myplayer.Left == true)
        {
            rb.velocity = transform.right * -speed;
        }
        //transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10);
    }

    private void Shoot()
    {
        if (CrossPlatformInputManager.GetButtonDown("Shoot"))
        {
            //PhotonNetwork.Instantiate(Bullet.name, Bullet.transform.position, Bullet.transform.rotation);
            if (Myplayer.Right == true)
            {
                rb.velocity = transform.right * speed;
            }
            else if (Myplayer.Left == true)
            {
                rb.velocity = transform.right * -speed;
            }
            pv.RPC("On_Shoot", RpcTarget.Others);
        }
    } 

    [PunRPC]
    void On_Shoot()
    {
        //PhotonNetwork.Instantiate(Bullet.name, Bullet.transform.position, Bullet.transform.rotation);
        if (Myplayer.Right == true)
        {
            rb.velocity = transform.right * speed;
        }
        else if(Myplayer.Left == true)
        {
            rb.velocity = transform.right * -speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Char" || collision.name == "Wall" || collision.tag == "char")
        {
            Object.Destroy(Bullet);
        }
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
