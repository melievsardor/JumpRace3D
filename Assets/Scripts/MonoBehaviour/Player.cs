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

            posX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        }
        else if(Input.GetMouseButton(0))
        {
            if (isForward)
            {
                isLookAt = false;
                float posDelta = posX - Camera.main.ScreenToViewportPoint(Input.mousePosition).x;

                transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);
                Vector3 direction = new Vector3(0f, -posDelta, 0f);
                transform.Rotate(direction, 1f);
                //Vector3 direction = new Vector3(0f, -posDelta * 20 + transform.eulerAngles.y, 0f);
                //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, direction, 0.1f);

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
            if(target.Neighbor != null)
            {
                targetRotate = target.Neighbor.transform.position - transform.position;
                lookRotation = Quaternion.LookRotation(targetRotate);
            }
            
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
