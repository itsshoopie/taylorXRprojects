using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorFeatures : CoreFeatures //Inherit CoreFeatures
{
    [Header("Door Configuration")]

    [SerializeField]
    private Transform doorPivot; //controls pivot obv

    [SerializeField]
    private float maxAngle = 90.0f;

    [SerializeField]
    private bool reverseAngleDirection = false;

    [SerializeField]
    private float doorSpeed = 1.0f;

    [SerializeField]
    private bool open = false;

    [SerializeField]
    private bool MakeKinematicOnOpen = false;

    [Header("IneractionsConfiguration")]

    [SerializeField]
    private XRSocketInteractor socketInteractor;

    [SerializeField]
    private XRSimpleInteractable simpleInteractable;

    private void Start()
    {
        socketInteractor?.selectEntered.AddListener((s) =>
        {
            OpenDoor();
            PlayOnStart();

        });

        simpleInteractable?.selectEntered.AddListener((s) =>
        {
            OpenDoor();
        });
    }

    public void OpenDoor()
    {
        if (!open)
        {
            PlayOnStart();
            open = true;
            StartCoroutine(ProcessMotion());
        }
    }

    private IEnumerator ProcessMotion()
    {
        //Constantly look to confirm door is open.
        while (open)
        {
            var angle = doorPivot.localEulerAngles.y < 100 ? doorPivot.localEulerAngles.y  : doorPivot.localEulerAngles.y - 360;

            angle = reverseAngleDirection ? Mathf.Abs(angle) : angle;

            if(angle <= maxAngle)
            {
                doorPivot?.Rotate(Vector3.up, doorSpeed * Time.deltaTime * (reverseAngleDirection ? -1 : 1));
            }

            else
            {
                //When done interacting, turn off rigidbody
                open = false; 
                var featureRigidBody = GetComponent<Rigidbody>();
                if (featureRigidBody != null && MakeKinematicOnOpen) featureRigidBody.isKinematic = true; 
            }

            yield return null;

        } 


    }

}
