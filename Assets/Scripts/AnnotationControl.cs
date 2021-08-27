using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnotationControl : MonoBehaviour
{
    public GameObject showText;
    public GameObject showPanel;
    public GameObject inputField;
    
    //
    public GameObject btnConfirm;
    public GameObject btnTilt;
    public GameObject btnCrack;
    public GameObject btnTunnel;
    public GameObject btn;

    // Start is called before the first frame update
    void Awake()
    {
        showPanel.SetActive(false);
        showText.SetActive(false);
    }

    public void Confirm()
    {
        showPanel.SetActive(true);
        showText.SetActive(true);
        showText.GetComponent<Text>().text = inputField.GetComponent<InputField>().text;

        inputField.SetActive(false);
        btnConfirm.SetActive(false);
        btnTilt.SetActive(false);
        btnCrack.SetActive(false);
        btnTunnel.SetActive(false);
        btn.SetActive(false);

}


    // Update is called once per frame
    void Update()
    {

    }
}
