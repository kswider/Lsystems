using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

public class TurtleController : MonoBehaviour
{
    [SerializeField]
    private GameObject _turtle;

    private SentenceGenerator _sentenceGenerator;
    private Vector3 _lastPosition;
    private Vector3 _newPosition;

    private Vector3 _direction = new Vector3(0, 0.25f, 0);
    private int _gamma = 90;
    private int _countOfRepeat;
    private string _sentenceToDraw;
    private static Dictionary<char, string> parameters;

    private LineRenderer _lineRenderer;

    // Use this for initialization
    void Start()
    {
        _lineRenderer = _turtle.GetComponent<LineRenderer>();
        _lastPosition = _turtle.transform.position;

        _gamma = 90;
        _countOfRepeat = 4;
        _sentenceGenerator = new SentenceGenerator();
        //_sentenceGenerator.Rules.Add(new Rule('F', "FF-F--F-F"));

        _sentenceToDraw = Scenes.getSceneStartingSentence();
        // ładowanie reguł z poprzedniej sceny i dodawanie do generatora
        parameters = Scenes.getSceneRules();
        foreach(KeyValuePair<char, string> entry in parameters)
        {
            _sentenceGenerator.Rules.Add(new Rule(entry.Key,entry.Value));
        }
        
        // generowanie ciągu
        for(int i = 0; i < _countOfRepeat; i++)
        {
            _sentenceToDraw = _sentenceGenerator.generate(_sentenceToDraw);
        }

        int position = 1;
        foreach(char letter in _sentenceToDraw)
        {
            switch (letter)
            {
                case 'F':
                    DrawLine(position);
                    _lineRenderer.positionCount++;
                    position++;
                    break;
                case '+':
                    _direction =  Quaternion.Euler(0,0, _gamma) * _direction;
                    break;
                case '-':
                    _direction = Quaternion.Euler(0, 0, -_gamma) * _direction;
                    break;
            }
        }
    }
    
    private void DrawLine(int index)
    {
        _newPosition = _lastPosition;
        _newPosition += _direction;
        _lineRenderer.SetPosition(index, _newPosition);
        _lastPosition = _newPosition;
    }  

}
