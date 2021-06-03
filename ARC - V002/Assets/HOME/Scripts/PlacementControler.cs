using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementControler : MonoBehaviour

{
    [SerializeField]
    private GameObject chairprefab, couchprefab, roundtableprefab, smallclosetprefab, roomprefab;

    [SerializeField]
    private Button chair, couch, table, smallcloset, room, clear, screenshot;

    private GameObject gameobjecttocreate, prefab;

    private ARRaycastManager aRRaycastManager;

    private void Awake()
    {
        aRRaycastManager = GetComponent<ARRaycastManager>();

        chair.onClick.AddListener(() => ChangePrefabTo(chairprefab));
        couch.onClick.AddListener(() => ChangePrefabTo(couchprefab));
        table.onClick.AddListener(() => ChangePrefabTo(roundtableprefab));
        smallcloset.onClick.AddListener(() => ChangePrefabTo(smallclosetprefab));
        room.onClick.AddListener(() => ChangePrefabTo(roomprefab));
        //clear.onClick.AddListener(() => DestroyObject());
        //screenshot.onClick.AddListener(() => ScreenShot());
    }

    private void ChangePrefabTo(GameObject prefab)
    {
        gameobjecttocreate = prefab;
    }

    private bool TrygetTouchPosition(out Vector2 touchposition)
    {
        if (Input.touchCount > 0)
        {
            touchposition = Input.GetTouch(0).position;
            return true;
        }
        touchposition = default;
        return false;
    }

    private void Update()
    {
        var hitpose = hits[0].pose;

        if (!TrygetTouchPosition(out Vector2 touchposition))

            return;

        if (aRRaycastManager.Raycast(touchposition, hits, TrackableType.PlaneWithinPolygon)) //gives us if we hit a collider or not
        {
            Instantiate(gameobjecttocreate, hitpose.position, hitpose.rotation);
        }
    }

    private void ScreenShot()
    {
        //var hitpose = hits[0].pose;
        //string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        //string fileName = "Screenshot" + timeStamp + ".png";
        //string pathToSave = fileName;
        //ScreenCapture.CaptureScreenshot("Screenshot");
        //Instantiate(gameobjecttocreate, hitpose.position, hitpose.rotation);

        Pose hitpose = hits[0].pose;
        {
            StartCoroutine("CaptureIt");
        }

        IEnumerator CaptureIt()
        {
            string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            string fileName = "Screenshot" + timeStamp + ".png";
            string pathToSave = fileName;
            ScreenCapture.CaptureScreenshot(pathToSave);
            yield return new WaitForEndOfFrame();
            Instantiate(gameobjecttocreate, hitpose.position, hitpose.rotation);
        }
    }

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
}