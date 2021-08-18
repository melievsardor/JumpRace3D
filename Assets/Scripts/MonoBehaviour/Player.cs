using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : PlayerController
{
    [SerializeField]
    private Material material;

    [SerializeField]
    private Color targetColor;

    [SerializeField]
    private Color notargetColor;

    [SerializeField]
    private Transform startTransform;

    [SerializeField]
    private Transform dotTransform;

    [SerializeField]
    private LineRenderer lineRenderer;

    private Quaternion lookRotation;

    private Vector3 targetRotate;

    private Vector3 currentVelocity;
    private Vector3 heightVelocity;
    private Vector3 currPosition;
        
    private Vector3 heightTargetPosition;
    private const float jumpForceHeight = 50;

    private float posX;

    private float time = 0;
    private float heigtTime = 0;

    private bool isForward;
    private bool isJumpHeight;
    private bool isRayBlock;

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
            isLookAt = false;

            posX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        }
        else if(Input.GetMouseButton(0))
        {
            if (isForward)
            {
                isLookAt = false;
                float posDelta = posX - Camera.main.ScreenToViewportPoint(Input.mousePosition).x;

                transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed);

                Vector3 direction = new Vector3(0f, -(posDelta * 10) + transform.eulerAngles.y, 0f);

                transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles,
                    direction, ref currentVelocity, 0.01f);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isForward = false;
        }


        if(isJumpHeight)
        {
            transform.position = Vector3.MoveTowards(transform.position, heightTargetPosition, Time.deltaTime * 15f);
            heigtTime += Time.deltaTime;
            
            if(Vector3.Distance(transform.position, heightTargetPosition) < 0.01f)
            {
                heigtTime = 0;
                isJumpHeight = false;
                rigidbody.useGravity = true;
            }

        }

        DotHelper();

        lineRenderer.SetPosition(0, startTransform.position);
        lineRenderer.SetPosition(1, dotTransform.localPosition);
    }


    private void DotHelper()
    {
        var ray = new Ray(this.transform.position, new Vector3(0f, -1f, 0f));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "target")
            {
                material.color = targetColor;
            }
            else
            {
                material.color = notargetColor;
            }
           
            dotTransform.position = new Vector3(startTransform.position.x, hit.point.y,
           startTransform.position.z);

        }

    }

    protected override void CollisionEnter(Collision collision)
    {
        base.CollisionEnter(collision);

        if(collision.gameObject.tag == "target")
        {
            if (target == null)
                return;

            if(target.targetType == Target.TargetType.OneTime)
            {
                rigidbody.useGravity = false;

                currPosition = transform.position;

                heightTargetPosition = new Vector3(transform.position.x, jumpForceHeight, transform.position.z);
                isJumpHeight = true;

                animator.SetTrigger("jump");
            }
            else
            {
                JumpOnTarget();
            }

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
