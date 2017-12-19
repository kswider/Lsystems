using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;


public class TurtleController : MonoBehaviour
{
    [SerializeField]
    private GameObject turtle;
    [SerializeField]
    private Material material;

    private SentenceGenerator _sentenceGenerator;
    private Vector3 lastPosition;
    private Vector3 newPosition;

    private Vector3 direction = new Vector3(0, 1, 0);
    private float delta;
    private int countOfRepeat;
    private string sentenceToDraw;
    private static Dictionary<char, string> parameters;

    private LineRenderer _lineRenderer;

    // Use this for initialization
    void Start()
    {
        //_lineRenderer = _turtle.GetComponent<LineRenderer>();
        lastPosition = turtle.transform.position;


        Stack<LineRenderer> _lineRendererStack = new Stack<LineRenderer>();
       // Stack<int> _countStack = new Stack<int>();
        Stack<Vector3> positionStack = new Stack<Vector3>();
        Stack<Orientation> orientationStack = new Stack<Orientation>();
     //   int count = 0;
        Simulation sim = new Simulation(5);
        sim.evaluate(10);
        List <Command> commands = sim.translate();

        float scale = 1;
        Orientation orientation = new Orientation();
        foreach (Command command in commands)
        {

            switch (command.CommandName)
            {
                // 3D
                case "Forward":
                    newPosition = lastPosition;
                    newPosition += orientation.H * (float)command.parameters[0];

                    GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

                    cylinder.GetComponent<MeshRenderer>().material = material;

                    Vector3 newScale = cylinder.transform.localScale;
                    newScale.y = Vector3.Distance(lastPosition, newPosition)/2;
                    newScale.x = scale;
                    newScale.z = scale;
                    cylinder.transform.localScale = newScale;
            
                    cylinder.transform.position = Vector3.Lerp(lastPosition, newPosition, 0.5f);
                    cylinder.transform.up = newPosition - lastPosition;

                    lastPosition = newPosition;
                    break;
                case "Rotate U":
                    delta = (float)command.parameters[0];
                    orientation.RotateU(delta);
                    break;
                case "Rotate L":
                    delta = (float)command.parameters[0];
                    orientation.RotateL(delta);
                    break;
                case "Rotate H":
                    delta = (float)command.parameters[0];
                    orientation.RotateH(delta);
                    break;
                case "Dollar rotation":
                    orientation.DollarRotation();
                    break;
                case "Push position":
                  //  _lineRendererStack.Push(_lineRenderer);
                   // _countStack.Push(count);
                    positionStack.Push(lastPosition);
                    orientationStack.Push(new Orientation(orientation));
                 //   count = 0;
                //    Material material = _lineRenderer.material;

                //    GameObject branch = new GameObject("Branch");
                //    branch.transform.position = _lastPosition;
               //     _lineRenderer = branch.AddComponent<LineRenderer>();
              //      _lineRenderer.material = material;
              //      _lineRenderer.startWidth = .05f;
               //     _lineRenderer.endWidth = .05f;
                //    _lineRenderer.positionCount = 1;
                //    _lineRenderer.SetPosition(count, _lastPosition);
                    break;
                case "Pull position":
                //    _lineRenderer = _lineRendererStack.Pop();
                //    count = _countStack.Pop();
                    lastPosition = positionStack.Pop();
                    orientation = orientationStack.Pop();
                    break;
                case "Change width":
                    scale = (float)command.parameters[0];
                    break;


            }
            /* 2D
            switch (command.CommandName)
            {
                case "Forward":
                    _lineRenderer.positionCount++;
                    count++;
                    DrawLine(count);
                    break;
                case "Rotate X":

                    break;
                case "Rotate left":
                    _gamma = (float)command.parameters[0];
                    _direction = Quaternion.Euler(0, 0, _gamma) * _direction;
                    break;
                case "Rotate right":
                    _gamma = (float)command.parameters[0];
                    _direction = Quaternion.Euler(0, 0, -_gamma) * _direction;
                    break;
                case "Rotate Z":

                    break;
                case "Push position":
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
                case "Pull position":
                    _lineRenderer = _lineRendererStack.Pop();
                    count = _countStack.Pop();
                    _lastPosition = _positionStack.Pop();
                    _direction = _directionStack.Pop();
                    break;
            }
            */
        }
    }
    
    private void DrawLine(int index)
    {
        newPosition = lastPosition;
        newPosition += direction;
     //   lineRenderer.SetPosition(index, newPosition);
        lastPosition = newPosition;
    }  

}
