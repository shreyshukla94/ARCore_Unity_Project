using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Listens for touch events and performs an AR raycast from the screen touch point.
    /// AR raycasts will only hit detected trackables like feature points and planes.
    ///
    /// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
    /// and moved to the hit position.
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class PlaceOnPlane : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        private GameObject m_PlacedPrefab;

        private List<GameObject> m_PlacedObjects = new List<GameObject>();

        /// <summary>
        /// The prefab to instantiate on touch.
        /// </summary>
        //  public GameObject placedPrefab
        //  {
        //      get { return m_PlacedPrefab; }
        //      set { m_PlacedPrefab = value; }
        //  }
        /// <summary>
        /// The object instantiated as a result of a successful raycast intersection with a plane.
        /// </summary>
        private GameObject spawnedObject { get; set; }

        private void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

        private bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }
            touchPosition = default;
            return false;
        }

        //void Update()
        //{
        //    if (!TryGetTouchPosition(out Vector2 touchPosition))
        //        return;
        //
        //    RaycastSpawnObject(touchPosition);
        //}
        public void RaycastSpawnObject()
        {
            RaycastSpawnObject(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
        }

        public void RaycastSpawnObject(Vector2 position)
        {
            if (m_RaycastManager.Raycast(position, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;
                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnedObject.transform.position = hitPose.position;
                }
            }
        }

        public void DestroyPlacedObject()
        {
            Destroy(spawnedObject);
            spawnedObject = null;
        }

        private static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
        private ARRaycastManager m_RaycastManager;
    }
}