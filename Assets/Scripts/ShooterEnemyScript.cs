using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyScript : MonoBehaviour
{
    public float cooldown = 1.25f;
    public GameObject bullet;
    public PlayerStatusScriptable playerStatus;

    private float timeToNext = 0f;
    public bool readyToFire = true;
    private float distToPlayer;
    private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerStatus.playerTransform);
        distToPlayer = Vector3.Distance(transform.position,
            playerStatus.playerTransform.position);

        if (readyToFire && distToPlayer <= 10.0f)
        {
            readyToFire = false;
            timeToNext = Time.time + cooldown;
            StartCoroutine(miniShoot());
        }

        if (Time.time > timeToNext)
        {
            readyToFire = true;
        }

        if (health <= 0)
        {
            Destroy(this.gameObject, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletScript bs = collision.gameObject.GetComponent<BulletScript>();
            health -= bs.damage;
            Debug.Log(health);
        }
    }

    IEnumerator miniShoot()
    {
        readyToFire = false;
        GameObject myBullet;
        BulletScript bs;

        //rapid fire 3 bullets
        for (int i = 0; i < 3; i++)
        {
            myBullet = Instantiate(bullet, this.transform.position, this.transform.rotation);
            bs = myBullet.GetComponent<BulletScript>();
            bs.parent = this.gameObject;
            bs.damage = 99999;
            yield return new WaitForSeconds(0.2f);
        }

        //shuffle back forwards after recoil
        for (int i = 0; i < 5; i++)
        {
            transform.Translate(Vector3.forward * 0.1f);
            yield return new WaitForSeconds(0.04f);
        }   
    }
}
