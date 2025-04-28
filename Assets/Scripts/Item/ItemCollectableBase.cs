using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public string compareTag = "Player";
    public GameObject graphicItem;
    public float timeToHide = 1;

    [Header("Sounds")]
    public AudioSource audioSource;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag)) 
        {
            Collect();
        }

    }

    protected virtual void Collect()
    {
        if(graphicItem != null) graphicItem.SetActive(false);
        Invoke(nameof(HideItem), timeToHide);
        OnCollect();
    }

    private void HideItem()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        if(particleSystem != null) particleSystem.Play();
        if(audioSource != null) audioSource.Play();
    }
}
