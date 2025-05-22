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
    public GameObject gameOver;

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;

    private Animator _currentPlayer;
    private float _currentSpeed;
    public int jumpLimit;

    [Header("Jump Collision Check")]
    public Collider2D colliderPlayer2D;
    public string tagToCompareFloor = "Floor";
    public string tagToCompareEnemy = "Enemy";
    public float distToGround;
    public float spaceToGround = .1f;
    public ParticleSystem jumpVFX;
    private int _playerDirection = 1;

    [Header("Audio Setup")]

    public AudioRandomPlayAudioClips audioJump;

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

    private void Start()
    {
        jumpLimit = 0;
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
            _playerDirection = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
            if (myRigidBody.transform.localScale.x != 1)
            {
                myRigidBody.transform.DOScaleX(1, soPlayerSetup.durationToTurn);
            }
            _playerDirection = 1;
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

    //Tweener tween;

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && jumpLimit<2)
        {
            myRigidBody.velocity = Vector2.up * soPlayerSetup.forceJump;
            //myRigidBody.transform.localScale = Vector2.one;

            //Animação de Pulo ainda não funciona (A Corrigir) - Nessa posição o JumpAnimation não funciona
            

            DOTween.Kill(myRigidBody.transform);
            //if (tween != null) tween.Kill();


            
            //Debug.Log("Pulei");
            if(audioJump!=null) audioJump.PlayRandom();
            JumpAnimation();
            PlayerJumpVFX();
            jumpLimit++;
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
        gameOver.SetActive(true);
        
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector2.up, Color.yellow, distToGround + spaceToGround);

        //if (colliderPlayer2D.transform.CompareTag(tagToCompare)) 
        return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + spaceToGround);
        
        //else return false;
    }


    
    private void JumpAnimation()
    {
        myRigidBody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2,LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRigidBody.transform.DOScaleX(soPlayerSetup.jumpScaleX * _playerDirection, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        //tween = DOTween.To(ScaleXGetter, ScaleXSetter, soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }

    //Alternativa para  DOScaleX (Não estava funcionando apropriadamente)

    /*private float ScaleXGetter()
    {
        return myRigidBody.transform.localPosition.x;
    }

    private void ScaleXSetter(float value)
    {
        var s = myRigidBody.transform.localScale;
        s.x = value * _playerDirection;
        myRigidBody.transform.localScale = s;
    }*/

    #endregion

    #region ONTRIGGER

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.transform.CompareTag(tagToCompareEnemy))
        {
            if (jumpLimit < 2) jumpLimit++;
        }
    }

    #endregion

    #region ONCOLLIDER

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(tagToCompareFloor))
        {
            jumpLimit = 0;
        }
    }

    #endregion
}
