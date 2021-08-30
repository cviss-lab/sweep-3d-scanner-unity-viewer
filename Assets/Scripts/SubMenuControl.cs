using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject self;
    private bool isDisplay = false;
    void Start()
    {
        self.SetActive(isDisplay);
    }

    public void Display()
    {
        isDisplay = !isDisplay;
        self.SetActive(isDisplay);
    }

}
