using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuUI;

    public GameObject objectMenuUI;

    //public GameObject dismissButton;

    [SerializeField]
    private Camera arCamera;

    private Vector2 touchPosition = default;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            touchPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;

                if (Physics.Raycast(ray, out hitObject))
                {
                    Button startButton = hitObject.transform.Find("MainMenu").GetComponentInChildren<Button>();

                    if (startButton != null)
                    {
                        BeginGame();
                    }

                    Button menuButton = hitObject.transform.Find("ObjectMenu").GetComponentInChildren<Button>();

                    if (menuButton != null && menuButton.name == "DismissButton")
                    {
                        DismissObjectMenu();
                    }

                    if (menuButton != null && menuButton.name == "QuitButton")
                    {
                        QuitGame();
                    }
                }
            }
        }
    }

    public void BeginGame()
    {
        mainMenuUI.SetActive(false);
    }

    public void DismissObjectMenu()
    {
        objectMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
