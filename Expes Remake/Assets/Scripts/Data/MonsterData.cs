using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class MonsterAI
{
    [SerializeField]
    public float sightDistance = 20;

    [SerializeField]
    public float fov = 90;

    [SerializeField]
    public float closeCombatRange = 1.2f;

    [SerializeField]
    public float leapRange;

    [SerializeField]
    public int visionRate = 2;
}

[Serializable]
public class MonsterAnimations
{
    [SerializeField]
    public bool[] stand;

    [SerializeField]
    public bool[] patrol;

    [SerializeField]
    public bool[] chase;

    [SerializeField]
    public bool[] leap;

    [SerializeField]
    public bool[] attackLongRange;

    [SerializeField]
    public bool[] attackShortRangeLight;

    [SerializeField]
    public bool[] attackShortRangeHard;

    [SerializeField]
    public bool[] pain;

    [SerializeField]
    public bool[] death;
}

[Serializable]
public class MonsterAudio
{
    [SerializeField]
    public AudioClip[] idle;

    [SerializeField]
    public AudioClip[] sight;

    [SerializeField]
    public AudioClip[] attack;

    [SerializeField]
    public AudioClip[] smash;

    [SerializeField]
    public AudioClip[] swing;

    [SerializeField]
    public AudioClip[] pain;

    [SerializeField]
    public AudioClip[] death;
}

[Serializable]
[CreateAssetMenu]
public class MonsterData : ScriptableObject
{
    [SerializeField]
    public MonsterAI ai;

    [SerializeField]
    public MonsterAnimations animations;

    [SerializeField]
    public MonsterAudio audio;

    public bool canAttackLongRange
    {
        get { return HasAnimations(animations.attackLongRange); }
    }

    public bool canAttackCloseRange
    {
        get { return HasAnimations(animations.attackShortRangeLight) || HasAnimations(animations.attackShortRangeHard); }
    }

    public float sightDistanceSqr
    {
        get { return ai.sightDistance * ai.sightDistance; }
    }

    public float closeCombatRangeSqr
    {
        get { return ai.closeCombatRange * ai.closeCombatRange; }
    }

    bool HasAnimations(bool[] animations)
    {
        return animations != null && animations.Length > 0;
    }
}
