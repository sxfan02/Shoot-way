using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int speed = 10;
    public GameObject parent;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 2.0f);
        Debug.Log(damage);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != parent)
        {
            Destroy(this.gameObject, 0);
        }
    }
}
