using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// Used to control Ruler object.
// Measure distance between two points.
public class RulerControl : MonoBehaviour
{
    // Input control
    public GameObject leftController;
    private bool firstPress = false;
    private bool secondPress = false;
    private bool confirm = false;


    // Create a new mark object
    private GameObject markObj;
    private GameObject canvasObj;
    private GameObject textObj;
    private int markCount = 0;

    // Draw Line
    LineRenderer lineRenderer;
    private Vector3 firstAncherPos;
    private Vector3 secondAncherPos;

    // Text
    private float length;
    private Vector3 textPos;
    private Text text;
    private Canvas textCanvas;
    private float rotateSpeed = 2.0f;
    public Transform lookat;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    void Start()
    {

    }

    void Update()
    {
        // Measure length
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (!firstPress)
            {
                // Create a new mark object
                markCount++;
                markObj = new GameObject("markObj" + markCount.ToString()); ;
                canvasObj = new GameObject("canvasObj" + markCount.ToString());
                textObj = new GameObject("textObj" + markCount.ToString());

                // Canvas object initial
                textCanvas = canvasObj.AddComponent<Canvas>();
                canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();
                textCanvas.renderMode = RenderMode.WorldSpace;

                // Text object initial
                text = textObj.AddComponent<Text>();
                textObj.transform.SetParent(canvasObj.transform);
                textCanvas.transform.localScale -= new Vector3(0.995f, 0.995f, 0.995f);
                textCanvas.transform.position -= new Vector3(0f, 0.01f, 0f);
                text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;


                canvasObj.transform.SetParent(markObj.transform);


                // Initial line renderer
                lineRenderer = markObj.AddComponent<LineRenderer>();
                lineRenderer.positionCount = 0;
                lineRenderer.startColor = Color.white;
                lineRenderer.endColor = Color.white;
                lineRenderer.startWidth = 0.005f;
                lineRenderer.endWidth = 0.005f;
                lineRenderer.useWorldSpace = true;
                Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
                lineRenderer.material = whiteDiffuseMat;


                firstPress = true;
                firstAncherPos = transform.position;
                //GameObject firstAncher = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //firstAncher.transform.position = firstAncherPos;
                //(firstAncher.GetComponent(typeof(Collider)) as Collider).isTrigger = true;
                //firstAncher.transform.localScale -= new Vector3(0.95f, 0.95f, 0.95f);
                
                //firstAncher.transform.SetParent(markObj.transform);

            }
            else
            {
                secondPress = true;
                secondAncherPos = transform.position;
                //GameObject secondAncher = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //secondAncher.transform.position = secondAncherPos;
                //(secondAncher.GetComponent(typeof(Collider)) as Collider).isTrigger = true;
                //secondAncher.transform.localScale -= new Vector3(0.95f, 0.95f, 0.95f);

                //secondAncher.transform.SetParent(markObj.transform);
            }
        }

        // Cancel 
        if ((OVRInput.GetDown(OVRInput.Button.Four)|| OVRInput.GetDown(OVRInput.Button.Three)) && secondPress)
        {
            Destroy(markObj);
            firstPress = secondPress = confirm = false;
            this.gameObject.SetActive(false);
        }

        //confirm
        if ((OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick)|| OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)) && secondPress)
        {
            confirm = true;
        }


        if (!firstPress&&!secondPress&&!confirm)
        {
            transform.position = leftController.transform.position;
        }
        else if(firstPress&&!secondPress&&!confirm)
        {
            transform.position = leftController.transform.position;

            // Draw line
            lineRenderer.positionCount = 0;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, firstAncherPos); 
            lineRenderer.SetPosition(1, transform.position);

            // Draw length mark
            length = Vector3.Distance(firstAncherPos, transform.position);
            textPos = (transform.position + firstAncherPos) / 2;
            text.text = Math.Round(length, 3).ToString() + "m";
            canvasObj.transform.position = textPos;
            Quaternion angle = Quaternion.LookRotation(-(lookat.position - canvasObj.transform.position));
            canvasObj.transform.rotation = Quaternion.Slerp(canvasObj.transform.rotation, angle, rotateSpeed * Time.deltaTime);
        }
        else if(firstPress&&secondPress&&!confirm)
        {
            Quaternion angle = Quaternion.LookRotation(-(lookat.position - canvasObj.transform.position));
            canvasObj.transform.rotation = Quaternion.Slerp(canvasObj.transform.rotation, angle, rotateSpeed * Time.deltaTime);
        }
        else if(confirm)
        {
            firstPress = secondPress = confirm = false;
            this.gameObject.SetActive(false);
        }
    }
}

