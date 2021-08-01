using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnotationControl : MonoBehaviour
{
    public GameObject Annotation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void instantiateAnnotation()
    {
        GameObject annotation = Instantiate(Annotation,transform);
        annotation.GetComponent<Canvas>().enabled = true;
    }
}
