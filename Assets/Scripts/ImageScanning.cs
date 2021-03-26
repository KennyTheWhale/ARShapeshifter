using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using TMPro;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageScanning : MonoBehaviour
{
    ObjectController objectController;
    MenuController menuController;

    [SerializeField]
    GameObject objectName;
    TextMeshProUGUI objectNameText;
    [SerializeField]
    GameObject objectDescription;
    TextMeshProUGUI objectDescriptionText;
    private string objectDisplayNameUpper;
    private string objectDisplayNameLower;
    private string displayJoke;

    [SerializeField]
    private GameObject[] objectPrefabs;
    /*[SerializeField]
    private GameObject redObject;
    [SerializeField]
    private GameObject blueObject;
    [SerializeField]
    private GameObject greenObject;
    private GameObject nextObjectRed;
    private GameObject nextObjectGreen;
    private GameObject nextObjectBlue;
    public GameObject nextObject;*/
    //public bool removeObjectRequested;

    //[SerializeField]
    //private Vector3 scaleFactor = new Vector3(0.1f, 0.1f, 0.1f);

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager arTrackedImageManager;

    public void Awake()
    {
        arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        foreach(GameObject prefab in objectPrefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }

        /*nextObjectRed = Instantiate(redObject, Vector3.zero, Quaternion.identity);
        nextObjectBlue = Instantiate(blueObject, Vector3.zero, Quaternion.identity);
        nextObjectGreen = Instantiate(greenObject, Vector3.zero, Quaternion.identity);*/

        objectController = GetComponent<ObjectController>();
        menuController = GameObject.Find("Canvas").GetComponent<MenuController>();
        objectNameText = objectName.GetComponent<TextMeshProUGUI>();
        objectDescriptionText = objectDescription.GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach(ARTrackedImage trackedImage in args.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in args.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
            objectController.objectTouched = false;
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;

        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = position;

        prefab.SetActive(true);

        if (name == "RedSphere")
        {
            objectDisplayNameUpper = "Red Apple";
            objectDisplayNameLower = "red apple";
            displayJoke = "(Make sure you don't drop it)";
            //nextObject = nextObjectBlue;
        }

        if (name == "BlueCube")
        {
            objectDisplayNameUpper = "Blue Ice Cube";
            objectDisplayNameLower = "blue ice cube";
            displayJoke = "(Don't give yourself frostbite)";
            //nextObject = nextObjectGreen;
        }

        if (name == "GreenCapsule")
        {
            objectDisplayNameUpper = "Green Cactus";
            objectDisplayNameLower = "green cactus";
            displayJoke = "(Be careful of the spikes)";
            //nextObject = nextObjectRed;
        }

        /*if (removeObjectRequested)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
            objectController.objectTouched = false;
            yield return null;
            removeObjectRequested = false;
        }*/

        foreach (GameObject go in spawnedPrefabs.Values)
        {
            if (go.name != name)
            {
                go.SetActive(false);
            }

            if (menuController.objectMenuUI != null)
            {
                objectNameText.text = objectDisplayNameUpper;
                objectDescriptionText.text = "This is a " + objectDisplayNameLower + "!\nPinch the screen to change its size, or rotate it with both fingers.\n" + displayJoke;
            }

            /*if (nextObjectRequested)
            {
                go.SetActive(false);
                nextObject.SetActive(true);
                nextObjectRequested = false;
            }*/
        }

        
    }
}
