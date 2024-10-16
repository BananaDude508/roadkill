using Cinemachine.PostFX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PostProcessing : MonoBehaviour
{
    public PostProcessVolume volume;
    private Vignette damageOverlay;
    public float damageOverlayIntensity = 0.3f;
    public float setToZero;
    public float damagerChecker;
    public float health;
    public float startLerp;
    

    private void Awake()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out damageOverlay);
        health = PlayerStats.playerHealth;
        damagerChecker = health;
    }
    public void DamageVignette()
    {
        
        damageOverlay.intensity.value = damageOverlayIntensity;
        StartCoroutine(DamageOverlayOff());
        damagerChecker = PlayerStats.playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = PlayerStats.playerHealth;
        if (damagerChecker != health)
        {
            DamageVignette();
        }
    }
    public IEnumerator DamageOverlayOff()
    {
        yield return new WaitForSeconds(2);
        damageOverlay.intensity.value = setToZero;
        
    }
}
