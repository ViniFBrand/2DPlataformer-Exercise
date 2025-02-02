using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    public Animator player;
    public SOString soStringName;

    [Header("Speed Settings")]
    public Vector2 friction = new Vector2(-1f, 0);
    public float speed = 5;
    public float speedRun = 10;
    public float forceJump = 20;

    [Header("Animation Settings")]
    public float jumpScaleY = 1.1f;
    public float jumpScaleX = .7f;
    public float animationDuration = .3f;
    public Ease ease = Ease.OutBack;

    [Header("Animation Player")]
    public string boolRun = "Run";
    public string triggerDeath = "Death";
    public float durationToTurn = .1f;
}
