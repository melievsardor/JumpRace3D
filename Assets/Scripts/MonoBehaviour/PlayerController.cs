using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected float jumpDelta = 2f;

    [SerializeField]
    protected float jumpForce = 10f;

    [SerializeField]
    protected float forwardSpeed = 2f;


    protected Rigidbody rigidbody;
    protected Animator animator;

    protected float jumpForceHeight;


    private float lookAtSpeed = 1f;





    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

        jumpForceHeight = transform.position.y;
    }



    private void OnCollisionEnter(Collision collision)
    {
        CollisionEnter(collision);
    }


    protected virtual void CollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "target")
        {
            JumpOnTarget(collision);
        }
    }

    private void JumpOnTarget(Collision collision)
    {
        Target target = collision.transform.parent.GetComponent<Target>();

        if (target != null)
        {
            var neighbor = target.Neighbor;

            if (target.targetType == Target.TargetType.OneTime)
            {
                rigidbody.AddForce(Vector3.up * jumpForceHeight, ForceMode.Impulse);
                target.transform.DOScale(Vector3.zero, 2f);
            }
            else
            {
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            animator.SetTrigger("jump");

            if (neighbor != null)
            {
                StartCoroutine(LookAt(neighbor.transform));
            }
        }
    }

    private IEnumerator LookAt(Transform target)
    {

        var temp = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(temp);

        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            time += Time.deltaTime * lookAtSpeed;

            yield return null;
        }
    }


    protected virtual void JumpFinish()
    {

    }


    public void OnJumpEnd()
    {
        animator.SetTrigger("jumpingDown");
        JumpFinish();
    }

    public void OnJumpingDownEnd()
    {

    }

   

    protected void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
    }

}
