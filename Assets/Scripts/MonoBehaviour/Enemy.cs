using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayerController
{
    private int jumpCount;

    protected override void Start()
    {
        base.Start();

        GameManager.Instance.AddPlayer(this);
    }

    protected override void CollisionEnter(Collision collision)
    {
        base.CollisionEnter(collision);

        if (collision.gameObject.tag == "target")
        {
            if (target == null)
                return;

            JumpOnTarget();

            if (target.Neighbor != null)
            {
                StartCoroutine(LookAt(target.transform));
            }
        }

    }

    protected override void JumpFinish()
    {
        base.JumpFinish();

        if (!isPlay || isFinish)
            return;

        jumpCount++;

        if(jumpCount >= Random.Range(2, 5))
        {
            StartCoroutine(MoveNextTarget(target.transform.position));
        }

    }

    private IEnumerator MoveNextTarget(Vector3 target)
    {
        float time = 0;

        Vector3 newTarget = new Vector3(target.x, target.y + 1f, target.z);

        rigidbody.useGravity = false;

        jumpCount = 0;

        while (time < 1)
        {
            time += Time.deltaTime;

            time = time % 5f;

            transform.position = MathParabola.Parabola(transform.position, newTarget, 1f, time / 5f);

            

            yield return null;
        }

        rigidbody.useGravity = true;
    }



}
