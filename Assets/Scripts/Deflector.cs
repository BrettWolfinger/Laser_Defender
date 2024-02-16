using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflector : MonoBehaviour
{
    [SerializeField] GameObject deflectorEffectPrefab;
    public bool isDeflecting;

    Coroutine deflectorCoroutine;

    // Update is called once per frame
    void Update()
    {
        Deflect();
    }

    void Deflect()
    {
        if(isDeflecting && deflectorCoroutine == null)
        {
            deflectorCoroutine = StartCoroutine(DeflectorCoroutine());
        }
        else if(!isDeflecting && deflectorCoroutine != null)
        {
            StopCoroutine(deflectorCoroutine);
            deflectorCoroutine = null;
        }
    }

    IEnumerator DeflectorCoroutine()
    {
        GameObject instance = Instantiate(deflectorEffectPrefab, 
                                this.transform, worldPositionStays:false);
        yield return new WaitForSeconds(5);
        Destroy(instance);
        isDeflecting = false;
    }
}
