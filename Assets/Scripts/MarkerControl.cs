using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerControl : MonoBehaviour
{

    private float rotateSpeed = 2.0f;
    public Transform lookat;
    // Input control
    public GameObject rightController;
    public GameObject Annotation;
    private GameObject annotation;

    private bool firstPress = false;
    private bool secondPress = false;
    private bool confirm = false;
    private int annotationCount = 0;

    private GameObject annotationObj;
    private GameObject ancherObj;

    LineRenderer lineRenderer;
    private Vector3 firstAncherPos;
    private Vector3 secondAncherPos;

    private Color markColor = new Color(255, 255, 255);

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (!firstPress)
            {

                
                annotationCount++;
                firstPress = true;
                firstAncherPos = transform.position;

                // Initial Objects
                annotationObj = new GameObject("annotationObj" + annotationCount.ToString());

                annotation = Instantiate(Annotation, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
                annotation.transform.SetParent(annotationObj.transform);

                ancherObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                ancherObj.transform.position = firstAncherPos;
                (ancherObj.GetComponent(typeof(Collider)) as Collider).isTrigger = true;
                ancherObj.transform.localScale -= new Vector3(0.97f, 0.97f, 0.97f);
                ancherObj.transform.SetParent(annotationObj.transform);
                ancherObj.GetComponent<MeshRenderer>().material.color = markColor;



                // Initial line renderer
                lineRenderer = annotationObj.AddComponent<LineRenderer>();
                lineRenderer.positionCount = 0;
                lineRenderer.startColor = markColor;
                lineRenderer.endColor = markColor;
                lineRenderer.startWidth = 0.005f;
                lineRenderer.endWidth = 0.005f;
                Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
                lineRenderer.material = whiteDiffuseMat;
                lineRenderer.useWorldSpace = true;



            }   
            else
            {
                secondPress = true;
                secondAncherPos = transform.position;

                ancherObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                ancherObj.transform.position = secondAncherPos;
                (ancherObj.GetComponent(typeof(Collider)) as Collider).isTrigger = true;
                ancherObj.transform.localScale -= new Vector3(0.97f, 0.97f, 0.97f);
                ancherObj.transform.SetParent(annotationObj.transform);
                ancherObj.GetComponent<MeshRenderer>().material.color = markColor;
            }

        }


        // Cancel 
        if ((OVRInput.GetDown(OVRInput.Button.Four) || OVRInput.GetDown(OVRInput.Button.Three)) && secondPress)
        {
            Destroy(annotationObj);
            firstPress = secondPress = confirm = false;
            this.gameObject.SetActive(false);
        }

        //confirm
        if ((OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick) || OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)) && secondPress)
        {
            confirm = true;
        }


        if (!firstPress && !secondPress)
        {
            transform.position = rightController.transform.position;
        }
        else if (firstPress && !secondPress)
        {
            transform.position = rightController.transform.position;

            // Draw line
            lineRenderer.positionCount = 0;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, firstAncherPos);
            lineRenderer.SetPosition(1, transform.position);


            annotation.SetActive(true);
            annotation.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        }
        else if (firstPress && secondPress &&!confirm)
        {
            Quaternion angle = Quaternion.LookRotation(-(lookat.position - annotation.transform.position));
            annotation.transform.rotation = Quaternion.Slerp(annotation.transform.rotation, angle, rotateSpeed * Time.deltaTime);
        }
        else if  (confirm)
        {
            firstPress = secondPress = confirm = false;
            this.gameObject.SetActive(false);
        }
    }



}
