using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SoundManagerSO : ScriptableObject
{
    public List<AudioClip> Chop;
    public List<AudioClip> DeliveryFail;
    public List<AudioClip> DeliverySuccess;
    public List<AudioClip> footstep;
    public List<AudioClip> dropObject;
    public List<AudioClip> pickUpObject;
    public AudioClip pansizzle;
    public List<AudioClip> trash;
    public List<AudioClip> warning;
}
