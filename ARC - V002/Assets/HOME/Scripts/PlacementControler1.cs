using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementControler1 : MonoBehaviour

{
    [SerializeField]
    private GameObject chairprefab, couchprefab, roundtableprefab, smallclosetprefab, roomprefab;

    [SerializeField]
    private Button chair, couch, table, smallcloset, room, clear, screenshot;

    private GameObject gameobjecttocreate, prefab;

    [SerializeField]
    private ARRaycastManager aRRaycastManager;

    [SerializeField]
    private Camera arcam;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Touch touch;

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

    private void Update()
    {
        touch = Input.GetTouch(0);

        if (Input.touchCount < 0 || touch.phase != TouchPhase.Began)
        {
            return;
        }

        if (Pointeroverui(touch)) return;
        {
            Ray ray = arcam.ScreenPointToRay(touch.position);
            if (aRRaycastManager.Raycast(ray, hits))
            {
                Pose pose = hits[0].pose;
                Instantiate(gameobjecttocreate, pose.position, pose.rotation);
            }
        }
    }

    private bool Pointeroverui(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}