using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityStandardAssets.CrossPlatformInput;

public class Myplayer : MonoBehaviourPun, IPunObservable
{
    public PhotonView pv;

    public float moveSpeed = 10f;
    public float jumpforce = 20f;

    private Vector3 smoothMove;

    private GameObject sceneCamera;
    public GameObject playerCamera;

    float directionX;
    public Rigidbody2D rb;

    public SpriteRenderer sr;
    void start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (photonView.IsMine)
        {
            playerCamera = GameObject.Find("Main Camera");

            sceneCamera.SetActive(false);
            playerCamera.SetActive(true);
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
            sr.flipX = false;
            pv.RPC("OnDirectionChange_Right", RpcTarget.Others);
        }

        if (CrossPlatformInputManager.GetButtonDown("Left"))
        {
            sr.flipX = true;
            pv.RPC("OnDirectionChange_Left", RpcTarget.Others);
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            DoJump();
        }

        //var move = new Vector3(Input.GetAxis("Horizontal"), 0);
        //transform.position += move * moveSpeed * Time.deltaTime;
    }

    [PunRPC]
    void OnDirectionChange_Left()
    {
        sr.flipX = true;
    }

    [PunRPC]
    void OnDirectionChange_Right()
    {
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
