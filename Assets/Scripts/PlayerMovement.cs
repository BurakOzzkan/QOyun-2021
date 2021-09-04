using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    Rigidbody2D player1Rb;
    Rigidbody2D player2Rb;

    float movementSpeed = 64f;
    Vector2 movement;
    State state = State.Earth;
    bool moved = false;
    
    public enum State
    {
        Earth,
        Qars,
        Entangled,
        ReverseEntangled
    }


    // Start is called before the first frame update
    void Start()
    {
        player1Rb = player1.GetComponent<Rigidbody2D>();
        player2Rb = player2.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (state == State.Earth) state = State.Qars;
            else if (state == State.Qars) state = State.Earth;
        }

    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (!moved)
        {
            moved = true;
            switch (state)
            {
                case State.Earth:
                    player1Rb.MovePosition(player1Rb.position + movement * movementSpeed * Time.fixedDeltaTime);
                    break;
                case State.Qars:
                    player2Rb.MovePosition(player2Rb.position + movement * movementSpeed * Time.fixedDeltaTime);
                    break;
                case State.Entangled:
                    break;
                case State.ReverseEntangled:
                    break;
                default: break;
            }
        }

        if (movement == Vector2.zero && moved == true) moved = false;
    }

}
