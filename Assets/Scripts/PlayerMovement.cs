using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

<<<<<<< HEAD
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private State state;
    [SerializeField] private Place place;
    

    public enum State
    {
        Active,
        Passive,
        Entangled,
        ReverseEntangled
    }

    public enum Place
    {
        Earth,
        Qars
    }
=======
    public Rigidbody2D rb;
    public float movementSpeed = 5f;

    public enum State {Active, Passive, Entangled, ReverseEntangled}
>>>>>>> daf7bcb49cd7fd269d4027551b8175c67093cb77

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        switch (state) {
            case State.Active:
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                movement = movement.normalized;
                break;
            case State.Passive:
                break;
            case State.Entangled:
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                movement = movement.normalized;
                break;
            case State.ReverseEntangled:
                if (place == Place.Earth) {
                    movement.x = Input.GetAxisRaw("Horizontal");
                    movement.y = Input.GetAxisRaw("Vertical");
                    movement = movement.normalized;
                } else {
                    movement.x = -Input.GetAxisRaw("Horizontal");
                    movement.y = -Input.GetAxisRaw("Vertical");
                    movement = movement.normalized;
                }
                break;
            default: break;
        }
        /*
        */
=======
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
>>>>>>> daf7bcb49cd7fd269d4027551b8175c67093cb77
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
