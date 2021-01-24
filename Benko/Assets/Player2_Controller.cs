using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2_Controller : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float gravityValue = -9.81f;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }


        float jump = 0;

        if (Input.GetKey(KeyCode.Space))
        {
            jump = 10;
        }

        Vector3 move = new Vector3(Input.GetAxis("HorizontalPlayer2"), playerVelocity.y + jump, Input.GetAxis("VerticalPlayer2"));

        controller.Move(move * Time.deltaTime * playerSpeed);

        

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }



        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
