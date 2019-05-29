using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Ammo : MonoBehaviourPun, IPunObservable
{
    // Start is called before the first frame update
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject Bullet;

    public PhotonView pv;

    private GameObject enemies;
    private GameObject players;

    private Vector3 smoothMove;

    public bool mine;
    public bool enemy;

    void Update()
    {
        enemies = GameObject.FindGameObjectWithTag("Enemy");
        players = GameObject.FindGameObjectWithTag("Char");
        if (photonView.IsMine)
        {
            Shoot();
            mine = true;
            gameObject.tag = "PBullet";
        }
        else
        {
            SmoothMovement();
            enemy = true;
            gameObject.tag = "EBullet";
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
        //transform.position = Vector3.Lerp(Bullet.transform.position, smoothMove, Time.deltaTime * 10);
    }

    private void Shoot()
    {
        if (SummonAmmo.YesShoot == true)
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
            SummonAmmo.YesShoot = false;
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
        else if (Myplayer.Left == true)
        {
            rb.velocity = transform.right * -speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Object.Destroy(Bullet);
        }

        if (this.gameObject.tag == "EBullet") {
            if (mine == true)
            {                
                if (collision.tag == "Char" || collision.tag == "char")
                {
                    Object.Destroy(Bullet);
                    Object.Destroy(players);
                    Destroy(this.gameObject);
                    //PlayerLife.Health -= 10f;
                }

                if (collision.tag == "Enemy")
                {
                    Object.Destroy(Bullet);
                    Object.Destroy(enemies);
                    //PlayerLife.Health -= 10f;
                }
            }
            else
            {
                if (collision.tag == "Enemy")
                {
                    Object.Destroy(Bullet);
                    Object.Destroy(enemies);
                    //PlayerLife.Health -= 10f;
                }

                if (collision.tag == "Char" || collision.tag == "char")
                {
                    Object.Destroy(Bullet);
                    Object.Destroy(players);
                    Destroy(this.gameObject);
                    //PlayerLife.Health -= 10f;
                }
            }
        }

        if (this.gameObject.tag == "PBullet")
        {
            if (mine == true)
            {
                if (collision.tag == "Enemy")
                {
                    Object.Destroy(Bullet);
                    Object.Destroy(enemies);
                    Destroy(this.gameObject);
                    //PlayerLife.Health -= 10f;
                }

                if (collision.tag == "Char" || collision.tag == "char")
                {
                    Object.Destroy(Bullet);
                    Object.Destroy(players);
                    //PlayerLife.Health -= 10f;
                }

            }
            else
            {
                if (collision.tag == "Char" || collision.tag == "char")
                {
                    Object.Destroy(Bullet);
                    Object.Destroy(players);
                    //PlayerLife.Health -= 10f;
                }

                if (collision.tag == "Enemy")
                {
                    Object.Destroy(Bullet);
                    Object.Destroy(enemies);
                    Destroy(this.gameObject);
                    //PlayerLife.Health -= 10f;
                }
            }
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
