using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingCollisions : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement playerMovement;
    public DstToEntanglement dstToEntanglement;
    public bool inEntanglement = false;

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
            if (gameObject.name == "Player1")
            {
                playerMovement.isPlayer1Finished = true;
                playerMovement.state = PlayerMovement.State.Qars;
                playerMovement.player1.SetActive(false);
            }
            else if (gameObject.name == "Player2")
            {
                playerMovement.isPlayer2Finished = true;
                playerMovement.state = PlayerMovement.State.Earth;
                playerMovement.player2.SetActive(false);
            }
        }
        if (collision.gameObject.tag == "Entanglement")
        {
            inEntanglement = true;
            playerMovement.state = (dstToEntanglement.isX) ? PlayerMovement.State.ReverseEntangled : PlayerMovement.State.Entangled;
            collision.gameObject.SetActive(false);
        }
    }
}
