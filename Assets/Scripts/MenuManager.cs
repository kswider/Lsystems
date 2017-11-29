using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    private Button _startButton;
    // Use this for initialization

    public string stringToEdit = "Hello World";
    void OnGUI()
    {
        stringToEdit = GUI.TextField(new Rect(10, 10, 200, 20), stringToEdit, 25);
    }
    void Start () {
        _startButton.onClick.AddListener(StartDrawing);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartDrawing()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
