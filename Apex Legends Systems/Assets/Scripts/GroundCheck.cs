using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public static bool isGrounded;

    public float gravity = -9.8f;
    public Transform groundCheckObject;
    public float groundTrueDist;
    public float maxJumpHeight;
    public Vector3 downVelo;

    private CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        downVelo = new Vector3(0, 0, 0);

        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit ray;
        Physics.Raycast(groundCheckObject.position, groundCheckObject.forward, out ray);
        if(Mathf.Abs(ray.point.y - transform.position.y) > groundTrueDist)
        {
            if (isGrounded)
            {
                isGrounded = false;
               
            }
        }
        else if (!isGrounded)
        {
            isGrounded = true;
            downVelo = Vector3.zero;
        }


        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck.isGrounded)
        {
            downVelo.y = Mathf.Sqrt(-2 * gravity * maxJumpHeight);
        }

        downVelo.y += gravity * Time.deltaTime;
        character.Move(downVelo * Time.deltaTime);
    }
}
