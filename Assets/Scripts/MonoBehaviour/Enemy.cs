using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PlayerController
{
    private Target target;

    private int jumpCount;

    protected override void Start()
    {
        base.Start();


    }

    protected override void CollisionEnter(Collision collision)
    {
        base.CollisionEnter(collision);


        if (collision.gameObject.tag == "target")
        {
            target = collision.transform.parent.GetComponent<Target>().Neighbor;
        }

    }

    protected override void JumpFinish()
    {
        base.JumpFinish();

        jumpCount++;

        if(jumpCount == 2)
        {
            StartCoroutine(MoveNextTarget(target.transform.position));
        }

    }

    private IEnumerator MoveNextTarget(Vector3 target)
    {
        float time = 0;

        Vector3 newTarget = new Vector3(target.x, target.y + 1f, target.z);

        rigidbody.useGravity = false;

        while(time < 1)
        {
            time += Time.deltaTime;

            time = time % 5f;

            transform.position = MathParabola.Parabola(transform.position, newTarget, 1f, time / 5f);

            

            yield return null;
        }

        jumpCount = 0;

        rigidbody.useGravity = true;
    }



}
