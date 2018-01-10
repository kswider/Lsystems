using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _addRuleButton;
    [SerializeField]
    private InputField _startingSequenceInputField;
    [SerializeField]
    private InputField _beforeInputField;
    [SerializeField]
    private InputField _afterInputField;

    // Use this for initialization
    void Start () {
        _startButton.onClick.AddListener(StartDrawing);
        _addRuleButton.onClick.AddListener(AddRule);
	}

    // Update is called once per frame
    void Update () {
		
	}

    void StartDrawing()
    {
        //Scenes.StartingSequence = _startingSequenceInputField.text;
        Scenes.Load("main");
    }

    private void AddRule()
    {
        Scenes.Rules.Add(_beforeInputField.text[0], _afterInputField.text);
        _beforeInputField.text = "";
        _afterInputField.text = "";
    }
}
