using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{
    public float speed = 10f;
    public float destroyTime = 2f;
    public bool shootLeft = false;

    public GameObject CharacterPrefab;
    public GameObject BulletPrefab;

    public PhotonView pv;

    // Start is called before the first frame update
    IEnumerator DestroyBullets()
    {
        yield return new WaitForSeconds(destroyTime);
        this.GetComponent<PhotonView>().RPC("destroy", RpcTarget.AllBuffered);
    }

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        if(Myplayer.Mine == true)
        {
            if (BulletPrefab.tag == "PBullet")
            {
                if (collision.tag == "Enemy")
                {
                    Destroy(collision.gameObject);
                }
            }
            else if(BulletPrefab.tag == "EBullet")
            {
                if(collision.tag == "Player")
                {
                    Destroy(collision.gameObject);
                }
            }
        }
        else if(Myplayer.Yours == true)
        {
            if (BulletPrefab.tag == "EBullet")
            {
                if (collision.tag == "Player")
                {
                    Destroy(collision.gameObject);
                }
            }
            else if (BulletPrefab.tag == "PBullet")
            {
                if (collision.tag == "Enemy")
                {
                    Destroy(collision.gameObject);
                }
            }
        }

        /*if(collision.tag == "Enemy")
        {
            pv.RPC("GetDamage", RpcTarget.AllBuffered);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (!shootLeft)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    }

    [PunRPC]

    public void destroy()
    {
        Destroy(this.gameObject);
    }

    [PunRPC]
    public void changeDir()
    {
        shootLeft = true;
    }
}
