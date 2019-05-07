using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRubyShared
{
    /// <summary>
    /// Useful script for using a pan gesture to move an object forward or back along z axis using pan up and down
    /// and left or right using pan left or right
    /// </summary>
    [AddComponentMenu("Fingers Gestures/Component/Pan AR", 5)]
    public class FingersPanARComponentScript : MonoBehaviour
    {
        [Tooltip("The camera to use to convert screen coordinates to world coordinates. Defaults to Camera.main.")]
        public Camera Camera;

        [Tooltip("Target game object. If null, gets set to the transform of this script.")]
        public Transform Target;

        [Range(-3.0f, 3.0f)]
        [Tooltip("The speed at which to move the object forward and backwards.")]
        public float SpeedForwardBack = 1.0f;

        [Range(-3.0f, 3.0f)]
        [Tooltip("The speed at which to move the object left and right.")]
        public float SpeedLeftRight = 1.0f;

        [Range(-3.0f, 3.0f)]
        [Tooltip("Rotation speed, set to 0 to disable rotation.")]
        public float RotateSpeed = 3.0f;

        [Range(0.0f, 10.0f)]
        [Tooltip("Scale speed. Set to 0 to disable scaling.")]
        public float ScaleSpeed = 1.0f;

        [Tooltip("Whether a double tap will reset rotation.")]
        public bool DoubleTapToResetRotation = true;

        [Tooltip("Allow triple tap gesture to destroy the object.")]
        public bool TripleTapToDestroy;

        [Range(-3.0f, 3.0f)]
        [Tooltip("Orbit speed (desktop only, right mouse button and drag).")]
        public float OrbitSpeed = 0.25f;

        /// <summary>
        /// Pan gesture
        /// </summary>
        public PanGestureRecognizer PanGesture { get; private set; }

        /// <summary>
        /// Rotate gesture
        /// </summary>
        public RotateGestureRecognizer RotateGesture { get; private set; }

        /// <summary>
        /// Scale gesture
        /// </summary>
        public ScaleGestureRecognizer ScaleGesture { get; private set; }

        /// <summary>
        /// Double tap gesture to reset rotation
        /// </summary>
        public TapGestureRecognizer TapGestureReset { get; private set; }

        /// <summary>
        /// Triple tap gesture to destroy
        /// </summary>
        public TapGestureRecognizer TapGestureDestroy { get; private set; }

        /// <summary>
        /// Long press gesture to destroy
        /// </summary>
        public LongPressGestureRecognizer LongPressGesture { get; private set; }

        /// <summary>
        /// Long press callback to do things like show a popup menu
        /// </summary>
        public System.Action<FingersPanARComponentScript> LongPressGestureBegan { get; set; }

        private Vector3? orbitTarget;
        private float prevMouseX;
        private Quaternion origRotation = Quaternion.identity;
        private Vector3 origScale = Vector3.one;

        private void PanGestureStateUpdated(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Executing && orbitTarget == null)
            {
                Vector3 right = Camera.transform.right;
                right.y = 0.0f;
                right = right.normalized;
                Vector3 forward = Camera.transform.forward;
                forward.y = 0.0f;
                forward = forward.normalized;
                Target.Translate(right * gesture.DeltaX * Time.deltaTime * SpeedLeftRight, Space.World);
                Target.Translate(forward * gesture.DeltaY * Time.deltaTime * SpeedForwardBack, Space.World);
            }
        }

        private void RotateGestureStateUpdated(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Executing && orbitTarget == null)
            {
               Target.Rotate(Target.up, RotateGesture.RotationDegreesDelta * RotateSpeed);
            }
        }

        private void ScaleGestureStateUpdated(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Executing && orbitTarget == null && ScaleSpeed > Mathf.Epsilon)
            {
                Target.localScale *= ScaleGesture.ScaleMultiplier;
            }
        }

        private void TapGestureResetStateUpdated(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Ended && DoubleTapToResetRotation)
            {
                Debug.Log("Double tap on fingers pan ar component script ended");

                Target.rotation = origRotation;
                Target.localScale = origScale;
            }
        }

        private void TapGestureDestroyStateUpdated(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Ended && TripleTapToDestroy)
            {
                Debug.Log("Tripe tap on fingers pan ar component script ended");

                Destroy(gameObject);
            }
        }

        private void LongPressGestureStateUpdated(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Began)
            {
                Debug.Log("Long tap on fingers pan ar component script began");

                if (LongPressGestureBegan != null)
                {
                    LongPressGestureBegan.Invoke(this);
                }
                gesture.Reset();
            }
        }

        private void OnEnable()
        {
            if (Camera == null)
            {
                Camera = Camera.main;
            }

            Target = (Target == null ? transform : Target);
            origRotation = Target.rotation;
            origScale = Target.localScale;

            PanGesture = new PanGestureRecognizer();
            PanGesture.StateUpdated += PanGestureStateUpdated;
            PanGesture.PlatformSpecificView = gameObject;
            FingersScript.Instance.AddGesture(PanGesture);

            RotateGesture = new RotateGestureRecognizer();
            RotateGesture.StateUpdated += RotateGestureStateUpdated;
            RotateGesture.PlatformSpecificView = gameObject;
            RotateGesture.AllowSimultaneousExecution(PanGesture);
            FingersScript.Instance.AddGesture(RotateGesture);

            ScaleGesture = new ScaleGestureRecognizer();
            ScaleGesture.StateUpdated += ScaleGestureStateUpdated;
            ScaleGesture.PlatformSpecificView = gameObject;
            ScaleGesture.ZoomSpeed *= ScaleSpeed;
            ScaleGesture.AllowSimultaneousExecution(RotateGesture);
            ScaleGesture.AllowSimultaneousExecution(PanGesture);
            FingersScript.Instance.AddGesture(ScaleGesture);

            TapGestureReset = new TapGestureRecognizer();
            TapGestureReset.NumberOfTapsRequired = 2;
            TapGestureReset.PlatformSpecificView = gameObject;
            TapGestureReset.StateUpdated += TapGestureResetStateUpdated;
            FingersScript.Instance.AddGesture(TapGestureReset);

            TapGestureDestroy = new TapGestureRecognizer();
            TapGestureDestroy.NumberOfTapsRequired = 3;
            TapGestureDestroy.PlatformSpecificView = gameObject;
            TapGestureDestroy.StateUpdated += TapGestureDestroyStateUpdated;
            FingersScript.Instance.AddGesture(TapGestureDestroy);

            TapGestureReset.RequireGestureRecognizerToFail = TapGestureDestroy;

            LongPressGesture = new LongPressGestureRecognizer();
            LongPressGesture.PlatformSpecificView = gameObject;
            LongPressGesture.StateUpdated += LongPressGestureStateUpdated;
            FingersScript.Instance.AddGesture(LongPressGesture);
        }

        private void OnDisable()
        {
            if (FingersScript.HasInstance)
            {
                FingersScript.Instance.RemoveGesture(PanGesture);
                FingersScript.Instance.RemoveGesture(RotateGesture);
                FingersScript.Instance.RemoveGesture(ScaleGesture);
                FingersScript.Instance.RemoveGesture(TapGestureReset);
                FingersScript.Instance.RemoveGesture(TapGestureDestroy);
            }
        }

        private void Update()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.LinuxPlayer:
                case RuntimePlatform.LinuxEditor:
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.OSXEditor:
                    if (UnityEngine.Input.GetMouseButtonDown(1))
                    {
                        orbitTarget = Target.position;
                        prevMouseX = UnityEngine.Input.mousePosition.x;
                    }
                    else if (UnityEngine.Input.GetMouseButtonUp(1))
                    {
                        orbitTarget = null;
                    }
                    if (orbitTarget != null)
                    {
                        Camera.transform.RotateAround(orbitTarget.Value, Vector3.up, (UnityEngine.Input.mousePosition.x - prevMouseX) * Time.deltaTime * OrbitSpeed);
                    }
                    break;
            }
        }
    }
}
