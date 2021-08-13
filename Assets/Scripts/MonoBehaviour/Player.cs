using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpDelta = 2f;

    [SerializeField]
    private float jumpSpeed = 10f;

    [SerializeField]
    private float forwardSpeed = 2f;

    private Animator animator;

    private Vector3 jumpEnd;

    private float posX;
    private bool isForward;

    private bool isJump;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //  transform.DOMoveY(transform.position.y + jumpDelta, jumpSpeed).SetEase(Ease.OutSine);

        var jump = new Vector3(transform.position.x, transform.position.y + jumpDelta, transform.position.z);

        transform.DOJump(jump, jumpDelta, 0, jumpSpeed).OnComplete(() =>
        {

            this.animator.SetTrigger("floating");

        });
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isForward = true;

            posX = Input.mousePosition.x;
            Debug.Log("first pos x = " + posX);
        }
        else if(Input.GetMouseButton(0))
        {
            float deltaX = posX - Input.mousePosition.x;

            Debug.Log("delta pos x = " + deltaX);
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "water")
        {
            RestartGame();
        }
        else
        {
            animator.SetTrigger("jump");
            jumpEnd = new Vector3(transform.position.x, transform.position.y + jumpDelta, transform.position.z);

            transform.DOJump(jumpEnd, jumpDelta, 0, jumpSpeed).SetEase(Ease.OutSine).OnComplete(() =>
            {

                this.animator.SetTrigger("floating");

            });

        }
        
    }


    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
    }

}
