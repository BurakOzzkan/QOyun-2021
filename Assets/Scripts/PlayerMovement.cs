using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    Rigidbody2D player1Rb;
    Rigidbody2D player2Rb;

    int numberOfMoves = 0;
    float movementSpeed = 32f;
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

        Debug.Log(numberOfMoves);

    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (!moved)
        {
            moved = true;
            if (Mathf.Abs(movement.x) == 1 && Mathf.Abs(movement.y) == 1) numberOfMoves += 2;
            else if (movement != Vector2.zero) numberOfMoves++;
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


        GameObject currentGameObject = player1;
        if (state == State.Earth) currentGameObject = player1;
        else if (state == State.Qars) currentGameObject = player2;
        RaycastHit2D hit = Physics2D.Raycast(currentGameObject.transform.position, movement);
        if (hit.collider != null)
        {
            Debug.Log("aaaa");
        }
    }

}
