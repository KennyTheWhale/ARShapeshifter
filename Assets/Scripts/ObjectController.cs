using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ObjectController : MonoBehaviour
{
    ImageScanning imageScanning;
    MenuController menuController;

    [SerializeField]
    private Camera arCamera;

    private Vector2 touchPosition = default;

    public bool objectTouched = false;

    void Awake()
    {
        imageScanning = GetComponent<ImageScanning>();
        menuController = GameObject.Find("Canvas").GetComponent<MenuController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (!objectTouched && (touch.phase == TouchPhase.Began))
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;

                if (Physics.Raycast(ray, out hitObject))
                {
                    PlacementObject placementObject = hitObject.transform.GetComponent<PlacementObject>();

                    if (placementObject != null)
                    {
                        menuController.objectMenuUI.SetActive(true);
                    }
                }
            }
        }
    }
}
