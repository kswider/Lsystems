using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager2 : MonoBehaviour
{

    [SerializeField]
    private Button parameters1Button;
    [SerializeField]
    private Button parameters2Button;
    [SerializeField]
    private Button parameters3Button;
    [SerializeField]
    private Button parameters4Button;
    // Use this for initialization
    void Start()
    {
        parameters1Button.onClick.AddListener(delegate { StartDrawing(1); });
        parameters2Button.onClick.AddListener(delegate { StartDrawing(2); });
        parameters3Button.onClick.AddListener(delegate { StartDrawing(3); });
        parameters4Button.onClick.AddListener(delegate { StartDrawing(4); });
    }


    void StartDrawing(int treeNumber)
    {
        switch (treeNumber)
        {
            case 1:
                Scenes.addParameter("r1", 0.9);
                Scenes.addParameter("r2", 0.6);
                Scenes.addParameter("a0", 45);
                Scenes.addParameter("a2", 45);
                break;
            case 2:
                Scenes.addParameter("r1", 0.9);
                Scenes.addParameter("r2", 0.9);
                Scenes.addParameter("a0", 45);
                Scenes.addParameter("a2", 45);
                break;
            case 3:
                Scenes.addParameter("r1", 0.9);
                Scenes.addParameter("r2", 0.8);
                Scenes.addParameter("a0", 45);
                Scenes.addParameter("a2", 45);
                break;
            case 4:
                Scenes.addParameter("r1", 0.9);
                Scenes.addParameter("r2", 0.7);
                Scenes.addParameter("a0", 30);
                Scenes.addParameter("a2", -30);
                break;
        }
        Scenes.Load("main");
    }
}
