using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool canJump;
    private bool jumping;
    private int jumpBuffer;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float gravityScale;
    private Vector3 movementAxis;
    private Vector3 jumpAxis;
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 0.12f;
        gravityScale = 5;
        jumpPower = 18f;
        jumpAxis = new Vector3(0, jumpPower, 0);
        rigidbody = GetComponent<Rigidbody>();
        canJump = false;
        jumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBuffer = 8;
            Debug.Log("Jump Pressed");
        }
        
        movementAxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * movementSpeed;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {

            if (rigidbody.velocity.y <= 0 && (((transform.position.y - 0.45) >= collision.gameObject.transform.parent.transform.position.y)))
            {
                Debug.Log(collision.gameObject.transform.position.y);
                canJump = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            canJump = false;
        }
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(Physics.gravity * (gravityScale - 1) * rigidbody.mass);
        jumping = (jumpBuffer > 0) && canJump;

        if (jumping)
        {
            jumpBuffer = 0;
            Debug.Log("JUMPING");
            rigidbody.AddForce(jumpAxis, ForceMode.Impulse);
            canJump = false;
        }
        jumpBuffer = Mathf.Max(jumpBuffer - 1, 0);

        rigidbody.MovePosition(rigidbody.position + movementAxis);
    }
}
