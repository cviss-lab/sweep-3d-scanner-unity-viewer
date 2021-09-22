using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Reflection;
using System.Linq;
using UnityEditor;
using System;

public class ImageControl : MonoBehaviour, IPointerClickHandler
{
    private Camera cam;
    public GameObject OVRPlayer;

    private Vector3 originPosition;
    private bool panoramaMode = false;

    // Mapping function
    public GameObject rightHandController;
    private LineRenderer lineRenderer;
    private GameObject mapPoint;
    private bool confirmMapping = false;
    private Vector3 confirmPosition;

    RaycastHit hit;

    void Start()
    {
        cam = Camera.main;
        
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = 0.005f;
        lineRenderer.endWidth = 0.005f;
        Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
        lineRenderer.material = whiteDiffuseMat;
        lineRenderer.useWorldSpace = true;

        hit = new RaycastHit();
        var editorTypes = typeof(Editor).Assembly.GetTypes();
        type_HandleUtility = editorTypes.FirstOrDefault(t => t.Name == "HandleUtility");
        meth_IntersectRayMesh = type_HandleUtility.GetMethod("IntersectRayMesh", (BindingFlags.Static | BindingFlags.NonPublic));


        meshObj = GameObject.Find("meshObj");
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        panoramaMode = true;
        originPosition = OVRPlayer.transform.position;
        OVRPlayer.transform.position = this.transform.position;
        OVRPlayer.GetComponent<OVRPlayerController>().EnableLinearMovement = false;
        OVRPlayer.GetComponent<OVRPlayerController>().EnableRotation = false;

        mapPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        mapPoint.transform.localScale -= new Vector3(0.97f, 0.97f, 0.97f);
        mapPoint.GetComponent<MeshRenderer>().material.color = Color.white;
        mapPoint.transform.SetParent(gameObject.transform);
    }

    protected static MethodInfo meth_IntersectRayMesh;
    public static Type type_HandleUtility;
    private GameObject meshObj;
    private MeshFilter meshFilter;

    public static bool IntersectRayMesh(Ray ray, MeshFilter meshFilter, out RaycastHit hit)
    {
        return IntersectRayMesh(ray, meshFilter.mesh, meshFilter.transform.localToWorldMatrix, out hit);
    }

    public static bool IntersectRayMesh(Ray ray, Mesh mesh, Matrix4x4 matrix, out RaycastHit hit)
    {
        var parameters = new object[] { ray, mesh, matrix, null };
        bool result = (bool)meth_IntersectRayMesh.Invoke(null, parameters);
        hit = (RaycastHit)parameters[3];
        return result;
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

                //Destroy(mapPoint);
                //confirmMapping = false;
            }

            if(!confirmMapping)
            {
                mapPoint.transform.position = rightHandController.transform.position;
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {
                    confirmMapping = true;
                    confirmPosition = rightHandController.transform.position;
                }
            }

            if(confirmMapping)
            {
                // cast ray
                Vector3 direction = confirmPosition - gameObject.transform.position;
                Ray ray = new Ray(gameObject.transform.position, direction);
                foreach(Transform child in meshObj.transform)
                {
                    meshFilter = child.GetComponent<MeshFilter>();
                    if (IntersectRayMesh(ray, meshFilter, out hit))
                    {
                        // draw line
                        lineRenderer.positionCount = 0;
                        lineRenderer.positionCount = 2;
                        lineRenderer.SetPosition(0, gameObject.transform.position);
                        lineRenderer.SetPosition(1, hit.point);
                    }
                }




            }


        }
    }
}
