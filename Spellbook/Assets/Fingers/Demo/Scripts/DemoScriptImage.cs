﻿//
// Fingers Gestures
// (c) 2015 Digital Ruby, LLC
// http://www.digitalruby.com
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DigitalRubyShared
{
    public class DemoScriptImage : MonoBehaviour
    {
        public Vector2 WorldUnitsInCamera;
        public Vector2 WorldToPixelAmount;
        public FingersImageGestureHelperComponentScript ImageScript;
        public ParticleSystem MatchParticleSystem;
        public AudioSource AudioSourceOnMatch;
        bool hasDrawned = false;
        bool firstTime = true;
        float lastX = 0;
        float lastY = 0;
        public Button ResetButton;
        public Text SwipeInstructionText;
        
        private void LinesUpdated(object sender, System.EventArgs args)
        {
            hasDrawned = true;
            //Debug.LogFormat("Lines updated, new point: {0},{1}", ImageScript.Gesture.FocusX, ImageScript.Gesture.FocusY);
            lastX = ImageScript.Gesture.FocusX;
            lastY = ImageScript.Gesture.FocusY;
        }

        private void LinesCleared(object sender, System.EventArgs args)
        {
            //Debug.LogFormat("Lines cleared!");
        }

        private void Start()
        {
           ImageScript.LinesUpdated += LinesUpdated;
           ImageScript.LinesCleared += LinesCleared; 
            
            //Finding Pixel To World Unit Conversion Based On Orthographic Size Of Camera
            WorldUnitsInCamera.y = gameObject.GetComponent<Camera>().orthographicSize * 2;
            WorldUnitsInCamera.x = WorldUnitsInCamera.y * Screen.width / Screen.height;

            WorldToPixelAmount.x = Screen.width / WorldUnitsInCamera.x;
            WorldToPixelAmount.y = Screen.height / WorldUnitsInCamera.y;

            ResetButton.onClick.AddListener(ResetSwipe);
        }

        private void LateUpdate()
        {
            /*if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                ImageScript.Reset();
            }
            else if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {*/
            if (hasDrawned && firstTime)
            {
                hasDrawned = false;
                ImageGestureImage match = ImageScript.CheckForImageMatch();
                if (match != null)
                {
                firstTime = false;
                    var shape = MatchParticleSystem.shape;
                    MatchParticleSystem.transform.localPosition = ConvertToWorldUnits(lastX, lastY);
                    MatchParticleSystem.Play();
                    Debug.Log("Match Found: " + match.Name);
                    SwipeInstructionText.text = "You casted " + match.Name;
                    //AudioSourceOnMatch.Play();
                }
                else
                {
                    Debug.Log("No match found!");
                }
            }
                // TODO: Do something with the match
                // You could get a texture from it:
                // Texture2D texture = FingersImageAutomationScript.CreateTextureFromImageGestureImage(match);
            //}
        }
        public Vector3 ConvertToWorldUnits(float x, float y)
        {
            Vector3 result = new Vector3(); ;
            result.x = ((x / WorldToPixelAmount.x) - (WorldUnitsInCamera.x / 2)) +
            GetComponent<Camera>().transform.position.x;
            result.y = ((y / WorldToPixelAmount.y) - (WorldUnitsInCamera.y / 2)) +
            GetComponent<Camera>().transform.position.y;
            return result;
        }

        private void ResetSwipe()
        {
            SceneManager.LoadScene("MainPlayerScene");
            Debug.Log("Reset Swipe");
            firstTime = true;
            hasDrawned = false;
            ImageScript.Reset();
        }
    }
}
