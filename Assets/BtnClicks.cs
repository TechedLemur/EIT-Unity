using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnClicks : MonoBehaviour
{
    public GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ToggleActive()
    {
        gameObject.SetActive(!(gameObject.activeSelf));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
