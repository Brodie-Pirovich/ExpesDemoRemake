using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Header("Projectile Stats")]
    public float ExplosionRadius = 4.5f;
    public float ExplosionUpwardsForce = 1f;
    public int ExplosionDamage = 100;
    public float ExplosionForce = 70f;
    public float ExplosionDamageFalloff = 1f;
    public float velocity;

    private AudioSource audioSource;
    public AudioClip ExplosionSound;

    private Vector3 shootDirection;

    private IEnumerator Start()
    {
        // wait one frame before starting, to ensure all objects are affected
        yield return null;

        //var trailParticles = Instantiate(trailParticles, trailParticlesOrigin.position, Quaternion.identity) as ParticleSystem;
        //trailParticles.transform.parent = transform;*/
        audioSource = GetComponent<AudioSource>();   
    }

    public void Setup(Vector3 shootDirection)
    {
        this.shootDirection = shootDirection;
    }

    private void Update()
    {
        transform.position += shootDirection * velocity * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        Entity Enemy = collision.gameObject.GetComponent<Entity>();
        if (Enemy != null)
        {
            Enemy.TakeDamage(ExplosionDamage);
            Destroy(gameObject);
        }

        // for every object within the explosion radius with a rigidbody component, add them to the rigidbody list
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (Collider hit in objectsInRange)
        {
            QuakeMovement player = hit.GetComponent<QuakeMovement>();
            if (player != null)
            {
                // Apply knockback force to player if they are in explosion radius
                player.Knockback(player.transform.position - transform.position, ExplosionForce);
                Destroy(gameObject);
            }
            else
            {
                if (hit.GetComponent<Rigidbody>() != null)
                {
                    RaycastHit raycast;
                    if (Physics.Raycast(transform.position, hit.transform.position - transform.position, out raycast, Mathf.Infinity))
                    {
                        if (raycast.collider == hit)
                        {
                            Entity TargetHit = hit.GetComponent<Entity>();
                            if (TargetHit != null)
                            {
                                float proximity = (transform.position - TargetHit.transform.position).magnitude;
                                Debug.Log(proximity);
                                float effect = ExplosionDamageFalloff - (proximity / ExplosionRadius);

                                // if the target is very close to the explosion origin, take full damage
                                if (proximity <= 0.7f)
                                {
                                    TargetHit.TakeDamage(ExplosionDamage);
                                }
                                // else, take splash damage
                                else
                                {
                                    int actualDamage =  Mathf.RoundToInt(ExplosionDamage * effect);
                                    TargetHit.TakeDamage(actualDamage);
                                }

                                StartCoroutine(playAudio(ExplosionSound));
                                Destroy(gameObject);
                            }
                            else
                            {
                                StartCoroutine(playAudio(ExplosionSound));
                                Destroy(gameObject);
                            }
                        }
                    }
                }
                else
                {
                    StartCoroutine(playAudio(ExplosionSound));
                    Destroy(gameObject);
                }
            }
        }
    }

    public IEnumerator playAudio(AudioClip soundEffect)
    {
        if(soundEffect)
        {
            AudioSource.PlayClipAtPoint(soundEffect, transform.position);
        }
        yield break;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
