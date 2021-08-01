using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustAnnotation : MonoBehaviour
{

    public Transform lookat;
    public Transform target;
    public float rotateSpeed = 2.0f;
    private bool init = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(init)
        {
            this.transform.position = target.position;
            Quaternion angle = Quaternion.LookRotation(-(lookat.position - this.transform.position));
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, angle, rotateSpeed * Time.deltaTime);
        }

        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            init = false;
        }
    }
}
