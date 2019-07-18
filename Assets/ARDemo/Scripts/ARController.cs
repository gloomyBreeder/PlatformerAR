using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.SpatialTracking;
using Lean.Touch;

#if UNITY_EDITOR
using input = GoogleARCore.InstantPreviewInput;
#endif

public class ARController : MonoBehaviour
{
    public GameObject ParentAR;
    public GameObject GridPrefab;
    public GameObject FirstScene;
    public Transform MainCharacter;
    public Camera ARCamera;
    public GameObject JoystickPanel;
    public float Threshold;

    private List<DetectedPlane> _detectedPlanes = new List<DetectedPlane>();
    private List<GameObject> _planesGO = new List<GameObject>();
    private bool _hasTouched = false;

    void Update()
    {
        if (_hasTouched)
        {
            if (Session.Status == SessionStatus.LostTracking)
            {
                //Debug.LogError("LOST TRACKING");
                ARUIManager.instance.ShowMessage(ARUIManager.PanelConditions.Lost);
                return;
            }

            if (ARUIManager.instance.GetCurrentCondition() == ARUIManager.PanelConditions.Lost)
            {
                ARUIManager.instance.ShowMessage(ARUIManager.PanelConditions.HideAll);
                var mainController = MainCharacter.GetComponent<MainCharacterController>();
                if (mainController != null)
                    mainController.SetLastPosition();
            }
            TrackCameraModify();
            return;
        }

        if (Session.Status != SessionStatus.Tracking)
            return;

        Session.GetTrackables(_detectedPlanes, TrackableQueryFilter.New);

        // place planes
        for (int i = 0; i < _detectedPlanes.Count; i++)
        {
            GameObject grid = Instantiate(GridPrefab, Vector3.zero, Quaternion.identity, transform);
            grid.GetComponent<GridVisualiser>().Initialize(_detectedPlanes[i]);
            _planesGO.Add(grid);
            if (i == 0)
                ARUIManager.instance.ShowMessage(ARUIManager.PanelConditions.Found);
        }

        // get user's touch
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        // check if user touches plane
        TrackableHit hit;
        if (Frame.Raycast(touch.position.x, touch.position.y, TrackableHitFlags.PlaneWithinPolygon, out hit))
        {
            FirstScene.SetActive(true);
            JoystickPanel.SetActive(true);
            ARUIManager.instance.ShowMessage(ARUIManager.PanelConditions.Task);

            //create anchor where user tapped and place scene
            Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);
            //FirstScene.transform.position = new Vector3(hit.Pose.position.x, Threshold, hit.Pose.position.z);
            FirstScene.transform.position = new Vector3(hit.Pose.position.x, FirstScene.transform.position.y, hit.Pose.position.z);
            //change death distance in case of falling
            MainCharacterController character = MainCharacter.GetComponent<MainCharacterController>();
            character.DeathDistance += Threshold;

            //FirstScene.transform.position = hit.Pose.position - FirstScene.transform.up * Vector3.Distance(hit.Pose.position, FirstScene.transform.position);
            //FirstScene.transform.rotation = hit.Pose.rotation;
            //Vector3 cameraPosition = ARCamera.transform.position;
            //cameraPosition.y = hit.Pose.position.y;
            //FirstScene.transform.LookAt(cameraPosition, FirstScene.transform.up);

            // link object to anchor
            FirstScene.transform.parent = anchor.transform;
            _hasTouched = true;

            // hide planes
            for (int i = 0; i < _planesGO.Count; i++)
            {
                _planesGO[i].GetComponent<GridVisualiser>().HidePointCloud(false);
            }
        }
    }

    void TrackCameraModify()
    {
        // to fix bug with weird moving when scaling
        LeanScale scale = ParentAR.GetComponent<LeanScale>();
        if (scale != null)
        {
            MainCharacterController character = MainCharacter.GetComponent<MainCharacterController>();
            if (character == null)
                return;

            character.StopMoving = scale.IsCameraModify;
        }
    }
}
