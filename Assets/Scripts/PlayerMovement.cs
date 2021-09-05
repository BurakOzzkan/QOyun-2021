using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;
    public Text moveText;
    public Text maximumText;
    public Text stateText;
    Rigidbody2D player1Rb;
    Rigidbody2D player2Rb;

    public int numberOfMoves = 0;
    public bool isPlayer1Finished = false;
    public bool isPlayer2Finished = false;
    float dstRaycast = 0.65f;
    float movementSpeed = 32f;
    Vector2 movement;
    public State state = State.Earth;
    bool moved = false;
    bool hittingWall = false;
    bool hittingWall2 = false;
    GameObject controllingInReverse;
    int maximumMoves;

    public string nextSceneName;
    AudioSource audioData;

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
        controllingInReverse = player1;
        maximumMoves = int.Parse(maximumText.text.Substring(15));
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (state == State.Earth) state = State.Qars;
            else if (state == State.Qars) state = State.Earth;
            else if (state == State.ReverseEntangled)
            {
                if (controllingInReverse == player1) controllingInReverse = player2;
                else controllingInReverse = player1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (state == State.Entangled || state == State.ReverseEntangled) state = State.Earth;
        }

        if (numberOfMoves <= maximumMoves && isPlayer1Finished && isPlayer2Finished)
        {
            SceneManager.LoadScene(nextSceneName);
        }

        if ((numberOfMoves > maximumMoves && isPlayer1Finished && isPlayer2Finished) || Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.B))
        {
            SceneManager.LoadScene(nextSceneName);
        }


    }

    void FixedUpdate()
    {

        GameObject currentGameObject = player1;
        GameObject currentGameObject2 = player2;
        if (state == State.Earth) currentGameObject = player1;
        else if (state == State.Qars) currentGameObject = player2;
        else if (state == State.Entangled || state == State.ReverseEntangled)
        {
            currentGameObject = player1;
            currentGameObject2 = player2;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        RaycastHit2D hit = Physics2D.Raycast(currentGameObject.transform.position, new Vector3(movement.x, movement.y, 0), dstRaycast);
        RaycastHit2D hit2 = Physics2D.Raycast(currentGameObject2.transform.position, (state == State.Entangled) ? new Vector3(movement.x, movement.y, 0) : new Vector3(-movement.x, -movement.y, 0), dstRaycast);

        if (state == State.ReverseEntangled && controllingInReverse == player2)
        {
            hit = Physics2D.Raycast(currentGameObject.transform.position, new Vector3(-movement.x, -movement.y, 0), dstRaycast);
            hit2 = Physics2D.Raycast(currentGameObject2.transform.position, new Vector3(movement.x, movement.y, 0), dstRaycast);
        }

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name == "Collidable")
            {
                hittingWall = true;
            }
        }
        else
        {
            hittingWall = false;
            hittingWall2 = false;
        }

        if (state == State.Entangled || state == State.ReverseEntangled)
        {
            if(hit2.collider != null)
            {
                if (hit2.collider.gameObject.name == "Collidable")
                {
                    hittingWall2 = true;
                }
            }
        }


        if (!moved)
        {
            moved = true;
            if (Mathf.Abs(movement.x) == 1 && Mathf.Abs(movement.y) == 1) goto OutOfIf;
            else if (movement != Vector2.zero && ((state == State.Entangled || state == State.ReverseEntangled) ? !hittingWall || !hittingWall2 : !hittingWall) && (!isPlayer1Finished || !isPlayer2Finished)) numberOfMoves++;
            moveText.text = (numberOfMoves >= 2) ? "Moves: " + numberOfMoves.ToString() : "Move: " + numberOfMoves.ToString();
            switch (state)
            {
                case State.Earth:
                    stateText.text = "State: Earth";
                    if (!hittingWall)
                    {
                        player1Rb.MovePosition(player1Rb.position + movement * movementSpeed * Time.fixedDeltaTime);
                        if (movement != Vector2.zero) audioData.Play(0);
                    }
                    break;
                case State.Qars:
                    stateText.text = "State: Qars";

                    if (!hittingWall)
                    {
                        player2Rb.MovePosition(player2Rb.position + movement * movementSpeed * Time.fixedDeltaTime);
                        if (movement != Vector2.zero) audioData.Play(0);
                    }
                    break;
                case State.Entangled:
                    stateText.text = "State: Entangled";

                    if (!hittingWall || !hittingWall2)
                    {
                        player1Rb.MovePosition(player1Rb.position + movement * movementSpeed * Time.fixedDeltaTime);
                        player2Rb.MovePosition(player2Rb.position + movement * movementSpeed * Time.fixedDeltaTime);
                        if (movement != Vector2.zero) audioData.Play(0);
                    }
                    break;
                case State.ReverseEntangled:
                    if (controllingInReverse == player1) stateText.text = "State: Reverse E. (Controlling Earth)";
                    else if (controllingInReverse == player2) stateText.text = "State: Reverse E. (Controlling Qars)";

                    if (!hittingWall || !hittingWall2)
                    {
                        if (controllingInReverse == player1)
                        {
                            player1Rb.MovePosition(player1Rb.position + movement * movementSpeed * Time.fixedDeltaTime);
                            player2Rb.MovePosition(player2Rb.position - movement * movementSpeed * Time.fixedDeltaTime);
                            if (movement != Vector2.zero) audioData.Play(0);
                        }

                        if (controllingInReverse == player2)
                        {
                            player1Rb.MovePosition(player1Rb.position - movement * movementSpeed * Time.fixedDeltaTime);
                            player2Rb.MovePosition(player2Rb.position + movement * movementSpeed * Time.fixedDeltaTime);
                            if (movement != Vector2.zero) audioData.Play(0);
                        }

                    }
                    break;
                default: break;
            }
        } OutOfIf:;

        if (movement == Vector2.zero && moved == true) moved = false;


    }

}
