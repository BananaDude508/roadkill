using Cinemachine.PostFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PostProcessing : MonoBehaviour
{
    public PostProcessVolume volume;
    private Vignette damageOverlay;
    private bool beingDamaged = false;
    public float damageOverlayIntensity = 0.3f;
    public float setToZero;
    public float damagerChecker;
    public float health;
   


    private void Awake()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out damageOverlay);
        health = PlayerStats.playerHealth;
        damagerChecker = health;
        
    }
    public void DamageVignette()
    {
        beingDamaged = true;
        damageOverlay.intensity.value = damageOverlayIntensity;
        StartCoroutine(DamageOverlayOff());
        damagerChecker = PlayerStats.playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = PlayerStats.playerHealth;
        if (damagerChecker != health && !beingDamaged)
        {
            DamageVignette();
        }

    }
    public IEnumerator DamageOverlayOff()
    {
        yield return new WaitForSeconds(1);
        damageOverlay.intensity.value = setToZero;
        yield return new WaitForSeconds(0.5f);
        beingDamaged = false;
    }
}
