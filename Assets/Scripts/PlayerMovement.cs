using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    public float moveSpeed = 6.0f;
    public float rotateSpeed = 2.0f;
    public PlayerStatusScriptable playerStatus;

    bool jumping = false;
    Rigidbody rb;
    float jumpForce = 8.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerStatus.playerTransform = this.gameObject.transform;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0,1,0), -rotateSpeed, Space.Self);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 1, 0), rotateSpeed, Space.Self);
        }

        if (Input.GetButtonDown("Jump") && !jumping)
        {
            jumping = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumping = false;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Time.timeScale = 0;
        }
    }
}
