using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DstToEntanglement : MonoBehaviour
{

    public GameObject entanglement;
    public CheckingCollisions checkingCollisions;
    public GameObject x;
    public GameObject cnot;
    GameObject created = null;
    float dstToEntanglement;
    public bool isX;
    bool flag = true;


    // Start is called before the first frame update
    void Start()
    {
        isX = getRandomNumber();
        Debug.Log(isX);
    }

    // Update is called once per frame
    void Update()
    {
        float xDiff = entanglement.transform.position.x - transform.position.x;
        float yDiff = entanglement.transform.position.y - transform.position.y;
        dstToEntanglement = Mathf.Sqrt(Mathf.Pow(xDiff, 2) + Mathf.Pow(yDiff, 2));
        if (dstToEntanglement < 1f && flag)
        {
            entanglement.SetActive(false);
            created = Instantiate((isX) ? x : cnot, entanglement.transform.position, Quaternion.identity);
            flag = false;
        }
        else if (dstToEntanglement > 1f && !checkingCollisions.inEntanglement)
        {
            flag = true;
            entanglement.SetActive(true);
            if (created != null) Destroy(created);

        }


    }


    bool getRandomNumber()
    {
        return new System.Random().NextDouble() < 0.5;
    }
}
