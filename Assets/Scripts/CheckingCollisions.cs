using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingCollisions : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Entanglement")
        {
            playerMovement.state = PlayerMovement.State.Entangled;
            collision.gameObject.SetActive(false);
        }
    }
}
