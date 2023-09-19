using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject objectToCollade;
    public float speed;
    public float distanceToCollade;

    private float sizeOfObjectToCollade;
    private float sizeOfCube;
    private InputListener inputListener = new();

    // Start is called before the first frame update
    void Start()
    {
        sizeOfObjectToCollade = objectToCollade.GetComponent<Renderer>().bounds.size.x/2;
        sizeOfCube = GetComponent<Renderer>().bounds.size.x/2;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Renderer>().enabled)
        {
            var toMove = inputListener.GetVector3ToMoveByKeyInput();
            transform.Translate(toMove * speed * Time.deltaTime);
        }

        if (CollideObject())
        {
            GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnDestroy()
    {
        Destroy(GetComponent<Renderer>());
    }


    private bool CollideObject()
    {
        if (objectToCollade.GetComponent<Renderer>().enabled)
        {
            sizeOfObjectToCollade = objectToCollade.GetComponent<Renderer>().bounds.size.x / 2;
            float distance = Vector3.Distance(transform.position,
            objectToCollade.transform.position) - (sizeOfObjectToCollade + sizeOfCube);
            Debug.Log(distance);
            if (distance <= distanceToCollade)
            {
                return true;
            }
        }
        
        return false;
    }
}

public class InputListener
{
    // GetVector3ToMoveByKeyInput gets vector3 by pressing wasd keys
    public Vector3 GetVector3ToMoveByKeyInput()
    {
        Vector3 toMove = new Vector3();
        Vector3 moveForward = new Vector3(0,0,1);
        Vector3 moveBack = new Vector3(0, 0, -1);
        Vector3 moveRight = new Vector3(1, 0, 0);
        Vector3 moveLeft = new Vector3(-1, 0, 0);


        // move forward
        if (Input.GetKey(KeyCode.W))
        {
            toMove += moveForward;
        }
        // move back
        if (Input.GetKey(KeyCode.S))
        {
            toMove += moveBack;
        }
        // move right
        if (Input.GetKey(KeyCode.D))
        {
            toMove += moveRight;
        }
        // move left
        if (Input.GetKey(KeyCode.A))
        {
            toMove += moveLeft;
        }
        return toMove;
    }
}
