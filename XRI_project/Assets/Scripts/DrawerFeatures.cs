using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;
using UnityEngine.XR.OpenXR.Input;

public class DrawerFeatures : CoreFeatures
{
    [Header("Drawer Configuration")]

    [SerializeField]
    private Transform drawerSlide;

    [SerializeField]
    private float maxDistance = 1.0f;

    [SerializeField]
    private FeatureDirection featureDirection = FeatureDirection.Forward;

    [SerializeField]
    private bool open = false;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private XRSimpleInteractable simpleInteractable;

    private Vector3 initialPosition; //stores initial pos of drawer

    //Restrict drawer position
    private float drawerMinLimit;
    private float drawerMaxLimit;

    void Start()
    {
        //Save initial position of drawer on start
        initialPosition = drawerSlide.localPosition;

        //find the drawer min and max limits based on initialPosition and max distance
        if (featureDirection == FeatureDirection.Forward)
        {
            drawerMinLimit = initialPosition.z;
            drawerMaxLimit = initialPosition.z + maxDistance;
        }

        else
        {
            drawerMinLimit = initialPosition.z - maxDistance;
            drawerMaxLimit = initialPosition.z;
        }

        //Drawer with simple interactable
        simpleInteractable?.selectEntered.AddListener((s) =>
        {
            //if drawer is not open, open it.
            if (!open)
            {
                OpenDrawer();
            }

            else
            {
                CloseDrawer();
            }
        });
    }

    private void OpenDrawer()
    {
        open = true;
        PlayOnStart();
        StopAllCoroutines();
        StartCoroutine(ProcessMotion());
    }

    private void CloseDrawer()
    {
        open = false;
        PlayOnEnd();   
        StopAllCoroutines();
        StartCoroutine(ProcessMotion());
    }

    private IEnumerator ProcessMotion()
    {
        //open drawer to max distance or close initial position based on "open" bool status
        Vector3 targetPosition = open ? new Vector3(drawerSlide.localPosition.x, drawerSlide.localPosition.y, drawerMaxLimit) : initialPosition;

        while (drawerSlide.localPosition != targetPosition)
        {
            drawerSlide.localPosition = Vector3.MoveTowards(drawerSlide.localPosition, targetPosition, Time.deltaTime * speed);

            //ensure drawer stays within our defined limits
            float clampedZ = Mathf.Clamp(drawerSlide.localPosition.z, drawerMinLimit, drawerMaxLimit);

            drawerSlide.localPosition = new Vector3(drawerSlide.localPosition.x, drawerSlide.localPosition.y, clampedZ);
            yield return null;
        }
    }
}
