using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerEnemyScript : MonoBehaviour
{
    private bool moving;
    private bool inRange;

    private float cooldown;
    private int speed;
    private int range;
    private int health = 50;

    public PlayerStatusScriptable ps;

    // Start is called before the first frame update
    void Start()
    {
        moving = true;
        inRange = false;
        speed = 3;
        range = 20;
        cooldown = Random.Range(3.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        inRange = Vector3.Distance(ps.playerTransform.position, transform.position) < range;
        if (inRange)
        {
            transform.LookAt(ps.playerTransform);
            speed = 5;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (moving)
        {
            if (cooldown > 0)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                cooldown -= Time.deltaTime;
            }
            else
            {
                cooldown = Random.Range(3.0f, 5.0f);
                moving = false;
                StartCoroutine("wander");
            }
        }

        if (health <= 0)
        {
            Destroy(this.gameObject, 0f);
        }
    }

    private IEnumerator wander()
    {
        yield return new WaitForSeconds(1.0f);
        moving = true;
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
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
}
