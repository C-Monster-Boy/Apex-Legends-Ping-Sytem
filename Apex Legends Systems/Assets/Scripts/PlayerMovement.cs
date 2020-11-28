using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;

    private float xMove;
    private float zMove;

    private CharacterController character;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxis("Horizontal");
        zMove = Input.GetAxis("Vertical");

        Vector3 moveVector = transform.right * xMove + transform.forward * zMove;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveVector = (moveVector * runSpeed * Time.deltaTime);
        }
        else
        {
            moveVector = (moveVector * walkSpeed * Time.deltaTime);
        }

        character.Move(moveVector);


    }
}
