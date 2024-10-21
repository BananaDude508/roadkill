using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    public AudioSource drivingSource;

    public AudioSource shootingSource;


    public void PlaySound(string id)
    {
        switch (id)
        {
            case "shoot":
                shootingSource.Play();
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
            case "drive":
                if (start) drivingSource.Play();
                else drivingSource.Stop();
                break;
            default:
                Debug.LogError($"{id} not a valid long sound ID");
                break;
        }
    }
}
