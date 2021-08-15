using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : PlayerController
{
    private Quaternion lookRotation;

    private Vector3 targetRotate;

    private float posX;

    private float time = 0;

    private bool isForward;

    protected override void Start()
    {
        base.Start();

        GameManager.Instance.AddPlayer(this);

        isPlayer = true;
    }

    private void Update()
    {
        if (isLookAt)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            time += Time.deltaTime * lookAtSpeed;

            if (time >= 1)
            {
                isLookAt = false;
                time = 0;
            }
        }

        if (!isPlay || isFinish)
            return;

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
                isLookAt = false;

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

        if(collision.gameObject.tag == "target")
        {
            targetRotate = target.Neighbor.transform.position - transform.position;
            lookRotation = Quaternion.LookRotation(targetRotate);
        }
       

        if (collision.gameObject.tag == "water")
        {
            GameManager.Instance.SetFailed();
        }

        if(collision.gameObject.tag == "Finish")
        {
            GameManager.Instance.SetFinish();
        }

    }

    



}
