using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class TurtleController : MonoBehaviour
{
    private Material material;
    private Vector3 lastPosition;
    private Vector3 newPosition;
    private float scale;
    
    private Orientation orientation;
    private float delta;
    private static Dictionary<char, string> parameters;

    [SerializeField]
    private Button goBackButton;
    /* it was used for 2D trees
    private Vector3 direction;
    private int count;
    private LineRenderer lineRenderer; 
    */

    // Use this for initialization
    void Start()
    {
        goBackButton.onClick.AddListener(goBackToMenu);
        // parameters initialization
        lastPosition = new Vector3(0, 0, 0);
        Stack<Vector3> positionStack = new Stack<Vector3>();
        Stack<Orientation> orientationStack = new Stack<Orientation>();
        Stack<float> scaleStack = new Stack<float>();
        scale = 1;
        orientation = new Orientation();
        material = Resources.Load("Materials/Barks/bark02", typeof(Material)) as Material;
        /* it was used for 2D trees
        private Orientation orientation;
        direction = new Vector3(0,1,0);
        count = 0;
        lineRenderer = new LineRenderer();
        Stack<LineRenderer> _lineRendererStack = new Stack<LineRenderer>();
        */

        //Atom a = new Atom('A', new List<double> { 2.0, 4.9 });
        //Debug.Log("JSON: " + JsonUtility.ToJson(a).ToString());

        // Creating sentence to draw
        Simulation sim = new Simulation(Scenes.SimulationNumber);
        sim.evaluate(Scenes.Steps);
        List <Command> commands = sim.translate();
        
        foreach (Command command in commands)
        {

                switch (command.GetCommandName())
                {
                    // 3D
                    case "Forward":
                        newPosition = lastPosition;
                        newPosition += orientation.H * ((float)command.GetParameters()[0] * 2);
                        DrawLine(lastPosition, newPosition);
                        lastPosition = newPosition;
                        break;
                    case "Rotate U":
                        delta = (float)command.GetParameters()[0];
                        orientation.RotateU(delta);
                        break;
                    case "Rotate U2":
                        delta = -(float)command.GetParameters()[0];
                        orientation.RotateU(delta);
                        break;
                    case "Rotate L":
                        delta = (float)command.GetParameters()[0];
                        orientation.RotateL(delta);
                        break;
                    case "Rotate L2":
                        delta = -(float)command.GetParameters()[0];
                        orientation.RotateL(delta);
                        break;
                    case "Rotate H":
                        delta = (float)command.GetParameters()[0];
                        orientation.RotateH(delta);
                        break;
                    case "Rotate H2":
                        delta = -(float)command.GetParameters()[0];
                        orientation.RotateH(delta);
                        break;
                    case "Dollar rotation":
                        orientation.DollarRotation();
                        break;
                    case "Push position":
                        positionStack.Push(lastPosition);
                        orientationStack.Push(new Orientation(orientation));
                        scaleStack.Push(scale);
                        break;
                    case "Pull position":
                        lastPosition = positionStack.Pop();
                        orientation = orientationStack.Pop();
                        scale = scaleStack.Pop();
                        break;
                    case "Change width":
                        scale = (float)command.GetParameters()[0] / 10;
                        break;
                }

            
            /* 2D
            switch (command.CommandName)
            {
                case "Forward":
                    lineRenderer.positionCount++;
                    count++;
                    DrawLine(count);
                    break;
                case "Rotate X":

                    break;
                case "Rotate left":
                    gamma = (float)command.parameters[0];
                    direction = Quaternion.Euler(0, 0, delta) * direction;
                    break;
                case "Rotate right":
                    gamma = (float)command.parameters[0];
                    direction = Quaternion.Euler(0, 0, -delta) * direction;
                    break;
                case "Push position":
                    lineRendererStack.Push(lineRenderer);
                    countStack.Push(count);
                    positionStack.Push(lastPosition);
                    directionStack.Push(direction);
                    count = 0;
                    Material material = _lineRenderer.material;

                    GameObject branch = new GameObject("Branch");
                    branch.transform.position = lastPosition;
                    lineRenderer = branch.AddComponent<LineRenderer>();
                    lineRenderer.material = material;
                    lineRenderer.startWidth = .05f;
                    lineRenderer.endWidth = .05f;
                    lineRenderer.positionCount = 1;
                    lineRenderer.SetPosition(count, lastPosition);
                    break;
                case "Pull position":
                    lineRenderer = lineRendererStack.Pop();
                    count = countStack.Pop();
                    lastPosition = positionStack.Pop();
                    direction = directionStack.Pop();
                    break;
            }
            */
        }
    }

    private void goBackToMenu()
    {
        GameObject.Find("MenuCanvas").GetComponent<Canvas>().enabled = true;
        SceneManager.UnloadSceneAsync("main");
    }

    /* 2D
    private void DrawLine(int index)
    {
        newPosition = lastPosition;
        newPosition += direction;
        lineRenderer.SetPosition(index, newPosition);
        lastPosition = newPosition;
    }  
    */

    private void DrawLine(Vector3 lastPosition, Vector3 newPosition)
    {
        // Cylinder approach
        if (Scenes.DrawingApproach == 0)
        {
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.transform.parent = gameObject.transform;
            cylinder.GetComponent<MeshRenderer>().material = material;
            float distance = Vector3.Distance(newPosition, lastPosition);
            Vector3 newScale = cylinder.transform.localScale;
            newScale.y = distance / 2;
            newScale.x = scale;
            newScale.z = scale;
            cylinder.transform.localScale = newScale;
            cylinder.transform.position = Vector3.Lerp(lastPosition, newPosition, 0.5f);
            cylinder.transform.up = newPosition - lastPosition;
        }

        // Cone approach
        else
        {
            GameObject cone = CreateCone.Create(1, scale, scale * Scenes.WidthDecreaseRate);
            cone.transform.parent = gameObject.transform;
            cone.GetComponent<MeshRenderer>().material = material;
            Vector3 newScale = cone.transform.localScale;
            newScale.y = Vector3.Distance(lastPosition, newPosition);
            cone.transform.localScale = newScale;
            cone.transform.position = lastPosition;
            cone.transform.up = newPosition - lastPosition;
        }

    }
}
