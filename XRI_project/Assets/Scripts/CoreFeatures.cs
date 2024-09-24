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
    [field:SerializeField]
    public AudioClip AudioClipOnStart { get; set; }

    //Audio plays on close
    [field: SerializeField]
    public AudioClip AudioClipOnEnd { get; set; }

    private AudioSource audioSource;

    public FeatureUsage featureUsage = FeatureUsage.Once;

    protected virtual void Awake()
    {
        //MakeSFXAudioSource();
    }

    private void MakeSFXAudioSource()
    {
        //if this is equal to null, create it here

        if(audioSource == null) {

            audioSource = gameObject.AddComponent<AudioSource>();
    }

}
