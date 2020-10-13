﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public abstract class Entity : ExpesBehaviour
{
    protected const int SPAWNFLAG_NOT_EASY = 256;
    protected const int SPAWNFLAG_NOT_MEDIUM = 512;
    protected const int SPAWNFLAG_NOT_HARD = 1024;

    [HideInInspector]
    public string data;

    public int spawnflags;

    [SerializeField]
    public float health;

    AudioSource m_audioSource;

    #region Life cycle

    protected void Awake()
    {
        if (CheckSpawnFlags(this.spawnflags))
        {
            OnAwake();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected void Start()
    {
        OnStart();
    }

    protected void Update()
    {
        OnUpdate(Time.deltaTime);
    }

    protected virtual void OnDestroy()
    {
    }

    protected virtual void OnAwake()
    {
    }

    protected virtual void OnStart()
    {
    }

    protected virtual void OnUpdate(float deltaTime)
    {
    }

    #endregion

    #region Spawnflags

    protected virtual bool CheckSpawnFlags(int spawnflags)
    {
        /*if ((spawnflags & SPAWNFLAG_NOT_EASY) != 0 && GameMode.skill == GameSkill.Easy)
        {
            return false;
        }

        if ((spawnflags & SPAWNFLAG_NOT_MEDIUM) != 0 && GameMode.skill == GameSkill.Normal)
        {
            return false;
        }*/

        return true;
    }

    #endregion

    #region Collision

    void OnTriggerEnter(Collider other)
    {
        CharacterController character = other.GetComponent<CharacterController>();
        if (character != null)
        {
            OnCharacterEnter(character);
        }
    }

    void OnTriggerExit(Collider other)
    {
        CharacterController character = other.GetComponent<CharacterController>();
        if (character != null)
        {
            OnCharacterExit(character);
        }
    }

    protected virtual void OnCharacterEnter(CharacterController character)
    {
    }

    protected virtual void OnCharacterExit(CharacterController character)
    {
    }

    #endregion

    #region Damage

    public override void TakeDamage(int damage)
    {
        if (this.health > 0)
        {
            this.health -= damage;
            if (health > 0)
            {
                OnHurt(damage);
            }
            else
            {
                health = 0.0f;
                OnKill(damage);
            }
        }
    }

    protected virtual void OnHurt(int damage)
    {
    }

    protected virtual void OnKill(int damage)
    {
    }

    #endregion

    #region Target

    protected virtual void OnSignal()
    {
    }

    #endregion

    #region Sounds

    protected void PlayRandomAudioClip(AudioClip[] clips)
    {
        if (clips == null)
        {
            Debug.LogWarning("Can't play random sound: audio clips are null");
            return;
        }

        if (clips.Length == 0)
        {
            Debug.LogWarning("Can't play random sound: clips are empty");
            return;
        }

        if (m_audioSource == null)
        {
            m_audioSource = GetComponent<AudioSource>();
            if (m_audioSource == null)
            {
                Debug.LogError("Can't play random sound: AudioSource component is missing");
                return;
            }
        }

        m_audioSource.clip = clips[Random.Range(0, clips.Length)];
        m_audioSource.Play();
    }

    #endregion
}
