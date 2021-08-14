using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpDelta = 2f;

    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private float forwardSpeed = 2f;

    [SerializeField]
    private float lookAtSpeed = 1f;

    private Rigidbody rigidbody;

    private Animator animator;

    private float posX;

    private bool isForward;
    private bool isJump;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();
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

        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "water")
        {
            RestartGame();
        }
        else if(collision.gameObject.tag == "target")
        {

            var neighbor = collision.transform.parent.GetComponent<Target>().Neighbor;

            if(neighbor != null)
            {
                StartCoroutine(LookAt(neighbor.transform));
            }
            

            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("jump");
        }
        
    }

    private IEnumerator LookAt(Transform target)
    {

        var temp = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(temp);

        float time = 0;

        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            time += Time.deltaTime * lookAtSpeed;

            yield return null;
        }
    }

    private void OnJumpEnd()
    {
        animator.SetTrigger("jumpingDown");
    }

    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
    }

}
