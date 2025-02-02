using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region VARIABLES
    public Rigidbody2D myRigidBody;
    public HealthBase healthBase;

    [Header("Speed Settings")]
    public Vector2 friction = new Vector2(-1f, 0);
    public float speed = 5;
    public float speedRun = 10;
    public float forceJump = 20;

    [Header("Animation Settings")]
   /*public float jumpScaleY = 1.1f;
    public float jumpScaleX = .7f;
    public float animationDuration = .3f;*/
    public SOFloat soJumpScaleY;
    public SOFloat soJumpScaleX;
    public SOFloat soAnimationDuration;

    public Ease ease = Ease.OutBack;

    [Header("Animation Player")]
    public string boolRun = "Run";
    public string triggerDeath = "Death";
    public Animator animator;
    public float durationToTurn = .1f;

    private float _currentSpeed;



    #endregion

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }
    }

    private void Update()
    {
        Jump();
        Movement();
    }
    #region FUNCTIONS

    private void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = speedRun;
            animator.speed = 2;
        }
            
        else
        {
            _currentSpeed = speed;
            animator.speed = 1;
        }
            

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
            animator.SetBool(boolRun, true);
            if(myRigidBody.transform.localScale.x != -1)
            {
                myRigidBody.transform.DOScaleX(-1, durationToTurn);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            animator.SetBool(boolRun, true);
            if (myRigidBody.transform.localScale.x != 1)
            {
                myRigidBody.transform.DOScaleX(1, durationToTurn);
            }
        }
        else
        {
            animator.SetBool(boolRun, false);
        }

        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity += friction;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity -= friction;
        }

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = Vector2.up * forceJump;
            //myRigidBody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidBody.transform);

            //JumpAnimation();
        }
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        animator.SetTrigger(triggerDeath);
        
    }


    /*private void JumpAnimation()
    {
        myRigidBody.transform.DOScaleY(soJumpScaleY.value, soAnimationDuration.value).SetLoops(2,LoopType.Yoyo).SetEase(ease);
        myRigidBody.transform.DOScaleX(soJumpScaleX.value, soAnimationDuration.value).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }*/

    #endregion
}
