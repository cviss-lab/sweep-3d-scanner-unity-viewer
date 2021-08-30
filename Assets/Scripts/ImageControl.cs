using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageControl : MonoBehaviour, IPointerClickHandler
{
    private Camera cam;
    public GameObject OVRPlayer;

    private Vector3 originPosition;
    private bool panoramaMode = false;
    

    void Start()
    {
        cam = Camera.main;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        panoramaMode = true;
        originPosition = OVRPlayer.transform.position;
        OVRPlayer.transform.position = this.transform.position;
        OVRPlayer.GetComponent<OVRPlayerController>().EnableLinearMovement = false;
        OVRPlayer.GetComponent<OVRPlayerController>().EnableRotation = false;
    }

    void Update()
    {
        if(panoramaMode)
        {
            if(OVRInput.GetDown(OVRInput.Button.Three)|| OVRInput.GetDown(OVRInput.Button.Four))
            {
                panoramaMode = false;
                OVRPlayer.transform.position = originPosition;
                OVRPlayer.GetComponent<OVRPlayerController>().EnableLinearMovement = true;
                OVRPlayer.GetComponent<OVRPlayerController>().EnableRotation = true;
            }
            if(OVRInput.GetDown(OVRInput.Button.One))
            {
                
            }
        }
    }
}
