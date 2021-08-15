using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : PlayerController
{
    private float posX;

    private bool isForward;

    protected override void Start()
    {
        base.Start();


    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isForward = true;

            posX = Input.mousePosition.x;
        }
        else if(Input.GetMouseButton(0))
        {
            float deltaX = posX - Input.mousePosition.x;

            if (isForward)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);

                transform.Rotate(new Vector3(0f, -deltaX / 10 * Time.deltaTime, 0f));
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isForward = false;
        }
    }


    protected override void CollisionEnter(Collision collision)
    {
        base.CollisionEnter(collision);


        if (collision.gameObject.tag == "water")
        {
            RestartGame();
        }
    }

    



}
