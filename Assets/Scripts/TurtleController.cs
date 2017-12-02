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
    private float _gamma;
    private int _countOfRepeat;
    private string _sentenceToDraw;
    private static Dictionary<char, string> parameters;

    private LineRenderer _lineRenderer;

    // Use this for initialization
    void Start()
    {
        _lineRenderer = _turtle.GetComponent<LineRenderer>();
        _lastPosition = _turtle.transform.position;

        _gamma = 25.7f;
        _countOfRepeat = 7;
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


        Stack<LineRenderer> _lineRendererStack = new Stack<LineRenderer>();
        Stack<int> _countStack = new Stack<int>();
        Stack<Vector3> _positionStack = new Stack<Vector3>();
        Stack<Vector3> _directionStack = new Stack<Vector3>();
        int count = 0;
        foreach(char letter in _sentenceToDraw)
        {
            switch (letter)
            {
                case 'F':
                    _lineRenderer.positionCount++;
                    count++;
                    DrawLine(count);
                    break;
                case '+':
                    _direction =  Quaternion.Euler(0,0, _gamma) * _direction;
                    break;
                case '-':
                    _direction = Quaternion.Euler(0, 0, -_gamma) * _direction;
                    break;
                case '[':
                    _lineRendererStack.Push(_lineRenderer);
                    _countStack.Push(count);
                    _positionStack.Push(_lastPosition);
                    _directionStack.Push(_direction);
                    count = 0;
                    Material material = _lineRenderer.material;

                    GameObject branch = new GameObject("Branch");
                    branch.transform.position = _lastPosition;
                    _lineRenderer = branch.AddComponent<LineRenderer>();
                    _lineRenderer.material = material;
                    _lineRenderer.startWidth = .05f;
                    _lineRenderer.endWidth = .05f;                     
                    _lineRenderer.positionCount = 1;
                    _lineRenderer.SetPosition(count, _lastPosition);
                    break;
                case ']':
                    _lineRenderer = _lineRendererStack.Pop();
                    count = _countStack.Pop();
                    _lastPosition = _positionStack.Pop();
                    _direction = _directionStack.Pop();
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
