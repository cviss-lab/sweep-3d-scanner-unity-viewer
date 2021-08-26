using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanoramaControl : MonoBehaviour
{
    public bool isDisplay = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayControl()
    {
        if (!isDisplay)
        {
            this.gameObject.SetActive(true);
            isDisplay = true;
        }
        else
        {
            this.gameObject.SetActive(false);
            isDisplay = false;
        }
    }

}
