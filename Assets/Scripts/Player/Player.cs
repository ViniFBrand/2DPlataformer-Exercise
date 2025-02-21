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

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;

    private Animator _currentPlayer;
    private float _currentSpeed;

    [Header("Jump Collision Check")]
    public Collider2D colliderPlayer2D;
    public float distToGround;
    public float spaceToGround = .1f;
    public ParticleSystem jumpVFX;

    #endregion

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);

        var gun = _currentPlayer.transform.GetComponentInChildren<GunBase>();
        gun.playerSideReference = transform;

        healthBase.flashColor = _currentPlayer.transform.GetComponentInChildren<FlashColor>();

        if(colliderPlayer2D != null)
        {
            distToGround = colliderPlayer2D.bounds.extents.y;
        }
    }

    private void Update()
    {
        IsGrounded();
        Jump();
        Movement();
    }
    #region FUNCTIONS

    private void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = soPlayerSetup.speedRun;
            _currentPlayer.speed = 2;
        }
            
        else
        {
            _currentSpeed = soPlayerSetup.speed;
            _currentPlayer.speed = 1;
        }
            

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            if(myRigidBody.transform.localScale.x != -1)
            {
                myRigidBody.transform.DOScaleX(-1, soPlayerSetup.durationToTurn);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            if (myRigidBody.transform.localScale.x != 1)
            {
                myRigidBody.transform.DOScaleX(1, soPlayerSetup.durationToTurn);
            }
        }
        else
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
        }

        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity += soPlayerSetup.friction;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity -= soPlayerSetup.friction;
        }

    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            myRigidBody.velocity = Vector2.up * soPlayerSetup.forceJump;
            //myRigidBody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidBody.transform);

            //JumpAnimation();
            PlayerJumpVFX();
        }
    }

    private void PlayerJumpVFX()
    {
        if (jumpVFX != null) jumpVFX.Play();
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
        
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector2.up, Color.yellow, distToGround + spaceToGround);
        return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + spaceToGround);
    }


    
    /*private void JumpAnimation()
    {
        myRigidBody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2,LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRigidBody.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }*/

    #endregion
}
