using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;
    
    public Animator animator;
    public string triggerAttack = "Attack";
    public string triggerIdle = "Idle";
    public string triggerDeath = "Death";

    public HealthBase healthBase;
    public AudioSource audioSourceKill;

    private void Awake()
    {
        if(healthBase != null)
        {
            healthBase.OnKill += OnEnemyKill;
        }
    }

    private void OnEnemyKill()
    {
        healthBase.OnKill -= OnEnemyKill;
        if(audioSourceKill != null) audioSourceKill.Play();
        PlayDeathAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var health = collision.gameObject.GetComponent<HealthBase>();

        if (health != null)
        {
            health.Damage(damage);
        }
    }

    /* private void OnCollisionExit2D(Collision2D collision)
     {
         PlayIdleAnimation();
     }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayAttackAnimation();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayIdleAnimation();
    }

    private void PlayAttackAnimation()
    {
        animator.SetTrigger(triggerAttack);
    }

    private void PlayIdleAnimation()
    {
        animator.SetTrigger(triggerIdle);
    }

    private void PlayDeathAnimation()
    {
        animator.SetTrigger(triggerDeath);
    }

    public void Damage(int amount)
    {
        healthBase.Damage(amount);
    }

}
