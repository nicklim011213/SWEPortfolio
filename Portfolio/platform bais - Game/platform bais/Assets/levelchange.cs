using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelchange : MonoBehaviour {
public Rect button = new Rect(15,15,200,110);
 public string buttonLabel = "Retry";
 public string levelToLoad = "test_level";

 private void OnGUI(){
  if (GUI.Button (button, buttonLabel)) {
      Application.LoadLevel (levelToLoad);
    }
 }
}﻿