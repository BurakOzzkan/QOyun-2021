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
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Entanglement")
        {
            inEntanglement = true;
            playerMovement.state = (dstToEntanglement.isX) ? PlayerMovement.State.ReverseEntangled : PlayerMovement.State.Entangled;
            collision.gameObject.SetActive(false);
        }
    }
}
