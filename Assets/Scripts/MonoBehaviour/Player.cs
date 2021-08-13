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

    private Rigidbody rigidbody;




    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        transform.DOMoveY(transform.position.y + jumpDelta, jumpSpeed).SetEase(Ease.OutSine);
    }

}
