using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FeatureUsage
{
    Once, //use once
    Toggle //use more than once
}

public class CoreFeatures : MonoBehaviour
{

    //Properties common way to access codeoutsife of this script.
    //Can create a public variable to access them in another script or you can create other Properties.
    //Properties are encapsulates and formatted as fields.
    //Properties have two ACCESSORS
    //Get Accessor (read) returns other encapsulated variable.
    //Set Accessor (write) allocates value to a property.
    //Property names are formatted as PascalCase only - PropertyName

    public bool AudioSFXSourceCreated
    { get; set; }

    //Audio plays when door opens
    [field: SerializeField]
    public AudioClip AudioClipOnStart { get; set; }

    //Audio plays on close
    [field: SerializeField]
    public AudioClip AudioClipOnEnd { get; set; }

    private AudioSource audioSource;

    public FeatureUsage featureUsage = FeatureUsage.Once;

    protected virtual void Awake()
    {
        MakeSFXAudioSource();
    }

    private void MakeSFXAudioSource()
    {
        //if this is equal to null, create it here
        audioSource = GetComponent<AudioSource>();

        //If component doesnt exist, make one
        if (audioSource == null)
        {

            audioSource = gameObject.AddComponent<AudioSource>();
        }

        AudioSFXSourceCreated = true;

    }

    protected void PlayOnStart()
    {
        if (AudioSFXSourceCreated && AudioClipOnStart != null)
        {
            audioSource.clip = AudioClipOnStart;
            audioSource.Play();
        }
    }

    protected void PlayOnEnd()
    {
        if (AudioSFXSourceCreated && AudioClipOnEnd != null)
        {
            audioSource.clip = AudioClipOnEnd;
            audioSource.Play();
        }

    }

}
