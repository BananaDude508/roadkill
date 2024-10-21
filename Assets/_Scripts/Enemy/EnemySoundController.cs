using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    public AudioSource groanSource;
    public AudioClip[] groanSounds;
    public AudioClip[] deathSounds;

    public AudioSource walkingSource;


    public void PlaySound(string id)
    {
        switch (id)
        {
            case "groan":
                groanSource.clip = groanSounds[Random.Range(0, groanSounds.Length)];
                groanSource.Play();
                break;
            case "die":
                groanSource.clip = deathSounds[Random.Range(0, deathSounds.Length)];
                groanSource.Play();
                break;
            default:
                Debug.LogError($"{id} not a valid temp sound ID (did you forget the 2nd parameter?)");
                break;
        }
    }

    public void PlaySound(string id, bool start)
    {
        switch (id) 
        {
            // case "walk":
            //     if (start) walkingSource.Play();
            //     else walkingSource.Stop();
            //     break;
            default:
                Debug.LogError($"{id} not a valid long sound ID");
                break;
        }
    }
}
