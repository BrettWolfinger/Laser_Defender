using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 2f;
    
    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = .1f;
    
    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    public bool isFiring;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, 
                                transform.position, Quaternion.identity);
            
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }
            Destroy(instance,projectileLifetime);
            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance,
                        baseFiringRate + firingRateVariance);
            Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
    
            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }

}
