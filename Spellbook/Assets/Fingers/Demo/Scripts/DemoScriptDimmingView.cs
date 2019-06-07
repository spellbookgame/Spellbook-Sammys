using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigitalRubyShared
{
    public class DemoScriptDimmingView : MonoBehaviour
    {
        public GameObject ContentView;

        private void OnEnable()
        {
            // change default behavior, images should block gestures unless they are the gesture view for the gesture
            FingersScript.Instance.ComponentTypesToDenyPassThrough.Add(typeof(UnityEngine.UI.Image));
        }

        public void TapGestureUpdated(GestureRecognizer tapGesture)
        {
            if (tapGesture.State == GestureRecognizerState.Ended)
            {
                ContentView.SetActive(false);
            }
        }
    }
}
