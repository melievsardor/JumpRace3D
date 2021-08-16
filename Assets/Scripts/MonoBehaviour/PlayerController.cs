using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour, IGameState
{
    [SerializeField]
    protected float jumpDelta = 2f;

    [SerializeField]
    protected float jumpForce = 10f;

    [SerializeField]
    protected float forwardSpeed = 2f;

    protected Rigidbody rigidbody;
    protected Animator animator;

    protected bool isPlay;
    protected bool isFinish;

    [SerializeField]
    private string playerName = "You";
    public string PlayerName { get { return playerName; } }

    protected float lookAtSpeed = 1f;

    private int index;
    public int Index { get { return index; } set { index = value; } }

    protected Target target;

    protected bool isPlayer;
    public bool IsPlayer { get { return isPlayer; } }

    protected bool isLookAt = true;

    protected virtual void Start()
    {
        GameManager.Instance.AddState(this);

        rigidbody = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollisionEnter(collision);
    }


    protected virtual void CollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "target")
        {
            target = collision.transform.parent.GetComponent<Target>();
        }
    }

    protected void JumpOnTarget()
    {
        index = target.Index;

        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        animator.SetTrigger("jump");

        isLookAt = true;
    }

    protected IEnumerator LookAt(Transform target)
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
        animator.SetTrigger("jumpingDown");

        GameManager.Instance.SetLeadrboard();
    }


    public void OnJumpEnd()
    {
        JumpFinish();
    }

    public void OnJumpingDownEnd()
    {

    }

    public void Play()
    {
        isPlay = true;
    }

    public void Failed()
    {
        isFinish = true;
    }

    public void Finish()
    {
        isFinish = true;
    }

    public void Leadrboard()
    {
       
    }

    
}
