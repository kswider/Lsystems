using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    private Button inputMenuButton;
    [SerializeField]
    private Button chooseSimulationMenuButton;
    [SerializeField]
    private Button exitButton;

    // Use this for initialization
    void Start () {
        inputMenuButton.onClick.AddListener(delegate { StartCoroutine(Scenes.Load("menu2")); });
        chooseSimulationMenuButton.onClick.AddListener(delegate { StartCoroutine(Scenes.Load("menu3")); });
        exitButton.onClick.AddListener(delegate { Application.Quit(); });
	}

    // Update is called once per frame
    void Update () {
		
	}
}
