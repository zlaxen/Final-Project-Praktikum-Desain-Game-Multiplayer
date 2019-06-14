using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityStandardAssets.CrossPlatformInput;

public class Myplayer : MonoBehaviourPun, IPunObservable
{
    public PhotonView pv;

    public float moveSpeed = 10f;
    public float jumpforce = 20f;

    private Vector3 smoothMove;

    public GameObject sceneCamera;
    public GameObject playerCamera;

    float directionX;
    public Rigidbody2D rb;

    public SpriteRenderer sr;

    public Text nameText;

    public GameObject BulletPrefab;
    public Transform BulletPoint;
    public Transform AnotherBulletPoint;

    public static bool Mine;
    public static bool Yours;
    //private GameObject Enemies;
    //private GameObject Players;

    public static bool Left = false;
    public static bool Right = true;
    void Start()
    {
        if (photonView.IsMine)
        {
            nameText.text = PhotonNetwork.NickName;

            rb = GetComponent<Rigidbody2D>();
            playerCamera = GameObject.Find("Main Camera");

            sceneCamera.SetActive(false);
            playerCamera.SetActive(true);
            gameObject.tag = "Player";
            BulletPrefab.tag = "PBullet";

            Mine = true;
            Yours = false;
            //gameObject.tag = "Char";
            //BulletPrefab.gameObject.tag = "PBullet";
        }
        else
        {
            nameText.text = pv.Owner.NickName;
            gameObject.tag = "Enemy";
            BulletPrefab.tag = "EBullet";

            Mine = false;
            Yours = true;
            //gameObject.tag = "Enemy";
            //BulletPrefab.gameObject.tag = "EBullet";
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            ProcessInputs();
        }
        else
        {
            smoothMovement();
        }
    }

    private void smoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, smoothMove, Time.deltaTime * 10);
    }
    private void ProcessInputs()
    {
        directionX = CrossPlatformInputManager.GetAxis("Horizontal");

        if (CrossPlatformInputManager.GetButtonDown("Right"))
        {
            /*if (Right == false)
            {
                transform.Rotate(0f, 180f, 0f);
                Right = true;
                Left = false;
            }
            if(Right == true)
            {
                transform.Rotate(0f, 0f, 0f);
                Right = true;
                Left = false;
            }*/
            sr.flipX = false;
            pv.RPC("OnDirectionChange_Right", RpcTarget.Others);
        }

        if (CrossPlatformInputManager.GetButtonDown("Left"))
        {
            /*if (Left == false)
            {
                transform.Rotate(0f, 180f, 0f);
                Left = true;
                Right = false;
            }
            if(Left == true)
            {
                transform.Rotate(0f, 0f, 0f);
                Left = true;
                Right = false;
            }*/
            sr.flipX = true;
            pv.RPC("OnDirectionChange_Left", RpcTarget.Others);
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            DoJump();
        }

        if (CrossPlatformInputManager.GetButtonDown("Shoot"))
        {
            Shoot();
        }

        //var move = new Vector3(Input.GetAxis("Horizontal"), 0);
        //transform.position += move * moveSpeed * Time.deltaTime;
    }

    public void Shoot()
    {
        GameObject Bullet;

        if (sr.flipX == true)
        {
            Bullet = PhotonNetwork.Instantiate(BulletPrefab.name, AnotherBulletPoint.position, Quaternion.identity);
        }

        else
        {
            Bullet = PhotonNetwork.Instantiate(BulletPrefab.name, BulletPoint.position, Quaternion.identity);
        }

        if (sr.flipX == true)
        {
            Bullet.GetComponent<PhotonView>().RPC("changeDir", RpcTarget.AllBuffered); 
        }
    }

    [PunRPC]
    void OnDirectionChange_Left()
    {
        /*if (Left == false)
        {
            transform.Rotate(0f, 180f, 0f);
            Left = true;
            Right = false;
        }
        if (Left == true)
        {
            transform.Rotate(0f, 0f, 0f);
            Left = true;
            Right = false;
        }*/
        sr.flipX = true;
    }

    [PunRPC]
    void OnDirectionChange_Right()
    {
        /*if (Right == false)
        {
            transform.Rotate(0f, 180f, 0f);
            Right = true;
            Left = false;
        }
        if (Right == true)
        {
            transform.Rotate(0f, 0f, 0f);
            Right = true;
            Left = false;
        }*/
        sr.flipX = false;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);
    }

    public void DoJump()
    {
        if(rb.velocity.y == 0)
        {
            rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Force);
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
