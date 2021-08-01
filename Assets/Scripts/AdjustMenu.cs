using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustMenu : MonoBehaviour
{
    public Transform target;
    public Transform lookat;
    public float rotateSpeed = 2.0f;
    public float menuHeight = 1.6f;
    void Awake()
    {
        GetComponent<Canvas>().enabled = false;
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            GetComponent<Canvas>().enabled = true;

        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
           GetComponent<Canvas>().enabled = false;
        }

        if(OVRInput.Get(OVRInput.Button.One))
        {
            Vector3 position = new Vector3(target.position.x, menuHeight, target.position.z);
            this.transform.position = position;
            Quaternion angle = Quaternion.LookRotation(-(lookat.position - this.transform.position));
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, angle, rotateSpeed * Time.deltaTime);
        }

    }
}
