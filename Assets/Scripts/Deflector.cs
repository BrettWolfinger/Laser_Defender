using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deflector : MonoBehaviour
{
    [SerializeField] GameObject deflectorEffectPrefab;
    [SerializeField] Slider deflectorSlider;

    float deflectorDuration = 0.5f;
    float deflectorCooldownTime = 5f;
    bool isDeflecting;
    bool isOnCooldown = false;

    Coroutine deflectorCoroutine;

    void Update()
    {
        Deflect();
    }

    public void UseDeflector()
    {
        if(isOnCooldown || isDeflecting)
        {
            Debug.Log("deflector on cooldown!");
        }
        else
        {
            isDeflecting = true;
        }
    }

    void Deflect()
    {
        if(isDeflecting && deflectorCoroutine == null)
        {
            deflectorCoroutine = StartCoroutine(DeflectorCoroutine());
        }
        //Deflector is ready to be used again, stop coroutine so it can be called again
        else if(!isOnCooldown && deflectorCoroutine != null)
        {
            StopCoroutine(deflectorCoroutine);
            deflectorCoroutine = null;
        }
    }

    IEnumerator DeflectorCoroutine()
    {
        isOnCooldown = true;
        GameObject instance = Instantiate(deflectorEffectPrefab, 
                                this.transform, worldPositionStays:false);
        
        //Length of deflector activation
        float time = deflectorDuration;
        while(time > 0)
        {
            deflectorSlider.value = time / Mathf.Max(deflectorDuration, Mathf.Epsilon);
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(instance);
        isDeflecting = false;

        //Keep deflector on cooldown
        time = 0.0f;
        while(time < deflectorCooldownTime)
        {
            deflectorSlider.value = time / Mathf.Max(deflectorCooldownTime, Mathf.Epsilon);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        isOnCooldown = false;
    }
}
