using System.Collections;
using System.Collections.Generic;
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
            //OpenDoor();

        });
    }

}
