using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager3 : MonoBehaviour {
    [SerializeField]
    private Button simulation1Button;
    [SerializeField]
    private Button simulation2Button;
    [SerializeField]
    private Button simulation3Button;
    [SerializeField]
    private Button goBackButton;
    [SerializeField]
    private Canvas canvas;
    private Animator cameraAnimator;
    // Use this for initialization
    void Start() {
        goBackButton.onClick.AddListener(delegate { SceneManager.LoadScene("menu"); });
        simulation1Button.onClick.AddListener(delegate { StartDrawing(1); });
        simulation2Button.onClick.AddListener(delegate { StartDrawing(2); });
        simulation3Button.onClick.AddListener(delegate { StartDrawing(4); });
        cameraAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();
    }

    void Update()
    {
        //Stopping Camera Animation
        if (Input.GetKeyDown(KeyCode.P))
        {        
            if (cameraAnimator.enabled)
                cameraAnimator.enabled = false;
            else
                cameraAnimator.enabled = true;
        }
    }

    void StartDrawing(int treeNumber)
    {
        Scenes.Parameters = new Dictionary<string, double>();
        switch (treeNumber)
        {
            case 1:
                Scenes.Parameters.Add("r1", 0.9);
                Scenes.Parameters.Add("r2", 0.6);
                Scenes.Parameters.Add("a0", 45);
                Scenes.Parameters.Add("a2", 45);
                break;
            case 2:
                Scenes.Parameters.Add("r1", 0.9);
                Scenes.Parameters.Add("r2", 0.9);
                Scenes.Parameters.Add("a0", 45);
                Scenes.Parameters.Add("a2", 45);
                break;
            case 3:
                Scenes.Parameters.Add("r1", 0.9);
                Scenes.Parameters.Add("r2", 0.8);
                Scenes.Parameters.Add("a0", 45);
                Scenes.Parameters.Add("a2", 45);
                break;
            case 4:
                Scenes.Parameters.Add("r1", 0.9);
                Scenes.Parameters.Add("r2", 0.7);
                Scenes.Parameters.Add("a0", 30);
                Scenes.Parameters.Add("a2", -30);
                break;
        }
        Scenes.DrawingApproach = 1;
        Scenes.WidthDecreaseRate = 0.707f;
        canvas.enabled = false;
        Scenes.SimulationNumber = 5;
        Scenes.Steps = 10;
        StartCoroutine(Scenes.LoadAdditive("main"));
        
    }

}
