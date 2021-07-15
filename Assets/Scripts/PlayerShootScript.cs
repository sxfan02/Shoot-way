using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour
{
    public GameObject bullet;
    public GunTypeScriptable gun;

    private float timeToNext = 0;
    private bool readyToFire = true;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        bool inRange = transform.eulerAngles.y < 45 ||
            transform.eulerAngles.y > 315;

        if (Input.GetMouseButton(0) && readyToFire && inRange)
        {
            readyToFire = false;
            timeToNext = gun.cooldown + Time.time;

            GameObject myBullet;
            BulletScript bs;
            myBullet = Instantiate(bullet, this.transform.position, this.transform.rotation);
            bs = myBullet.GetComponent<BulletScript>();
            bs.parent = this.gameObject;
            bs.damage = gun.damage;
        }

        if (Time.time > timeToNext)
        {
            readyToFire = true;
        }
    }
}
