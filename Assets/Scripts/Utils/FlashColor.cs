using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderes;
    public Color color = Color.red;
    public Color colorBasic = Color.white;
    public float duration = .1f;

    private Tween _currentTween;

    private void OnValidate()
    {
        spriteRenderes = new List<SpriteRenderer>();
        foreach (var child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderes.Add(child);
        }
    }

    public void Flash()
    {
        if (_currentTween != null) 
        {
            _currentTween.Kill();
            spriteRenderes.ForEach(item => item.color = colorBasic);
        }

        foreach(var sprite in spriteRenderes)
        {
            _currentTween = sprite.DOColor(color,duration).SetLoops(2,LoopType.Yoyo);
        }
    }
}
