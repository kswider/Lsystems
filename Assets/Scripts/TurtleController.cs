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

    private int _gamma;
    private int _countOfRepeat;
    private string _sentenceToDraw;
    private static Dictionary<char, string> parameters;
    private int up = 0;

    private int direction = 0;
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
        
        
        for(int i = 0; i < _countOfRepeat; i++)
        {
            _sentenceToDraw = _sentenceGenerator.generate(_sentenceToDraw);
        }

        //InvokeRepeating("Draw", 2f, 0.01f);
        //_lineRenderer.positionCount = _sentenceToDraw.Length;
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
                    if (direction == 3)
                        direction = 0;
                    else
                        direction++;
                    //_lineRenderer.positionCount--;
                    break;
                case '-':
                    if (direction == 0)
                        direction = 3;
                    else
                        direction--;
                    //_lineRenderer.positionCount--;
                    break;
            }
        }
    }
    
    private void DrawLine(int index)
    {
        switch (direction)
        {
            case 0:
                _newPosition = _lastPosition;
                _newPosition.y += .25f;
                _lineRenderer.SetPosition(index, _newPosition);
                //_turtle.transform.position = _newPosition;
                _lastPosition = _newPosition;
                break;
            case 1:
                _newPosition = _lastPosition;
                _newPosition.x += .25f;
                _lineRenderer.SetPosition(index, _newPosition);
                //_turtle.transform.position = _newPosition;
                _lastPosition = _newPosition;
                break;
            case 2:
                _newPosition = _lastPosition;
                _newPosition.y -= .25f;
                _lineRenderer.SetPosition(index, _newPosition);
                //_turtle.transform.position = _newPosition;
                _lastPosition = _newPosition;
                break;
            case 3:
                _newPosition = _lastPosition;
                _newPosition.x -= .25f;
                _lineRenderer.SetPosition(index, _newPosition);
                //_turtle.transform.position = _newPosition;
                _lastPosition = _newPosition;
                break;
        }
    }
    
    private void Draw()
    {
        if (_sentenceToDraw.Length > 0)
        {
            switch (_sentenceToDraw[0])
            {
                case 'F':
                    DrawLine(0);
                    break;
                case '+':
                    if (direction == 3)
                        direction = 0;
                    else
                        direction++;
                    break;
                case '-':
                    if (direction == 0)
                        direction = 3;
                    else
                        direction--;
                    break;
            }
            _sentenceToDraw = _sentenceToDraw.Remove(0, 1);
        }
    }

}
