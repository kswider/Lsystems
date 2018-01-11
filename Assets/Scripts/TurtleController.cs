using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TurtleController : MonoBehaviour
{
    private Material material;
    private Vector3 lastPosition;
    private Vector3 newPosition;
    private float scale;
    
    private Orientation orientation;
    private float delta;
    private static Dictionary<char, string> parameters;
    /* it was used for 2D trees
    private Vector3 direction;
    private int count;
    private LineRenderer lineRenderer; 
    */

    // Use this for initialization
    void Start()
    {
        // parameters initialization
        lastPosition = new Vector3(0, 0, 0);
        Stack<Vector3> positionStack = new Stack<Vector3>();
        Stack<Orientation> orientationStack = new Stack<Orientation>();
        scale = 1;
        orientation = new Orientation();
        material = Resources.Load("Materials/Barks/Bark_b9", typeof(Material)) as Material;
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
        Simulation sim = new Simulation(6);
        sim.evaluate(Scenes.Steps);
        List <Command> commands = sim.translate();
        
        foreach (Command command in commands)
        {

            switch (command.CommandName)
            {
                // 3D
                case "Forward":
                    newPosition = lastPosition;
                    newPosition += orientation.H * (float)command.parameters[0];
                    DrawLine(lastPosition, newPosition);
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
                    positionStack.Push(lastPosition);
                    orientationStack.Push(new Orientation(orientation));
                    break;
                case "Pull position":
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
        //GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //cylinder.GetComponent<MeshRenderer>().material = material;
        //float distance = Vector3.Distance(newPosition,lastPosition);
        GameObject cone = CreateCone.Create(1, scale, scale * 0.707f);
        cone.GetComponent<MeshRenderer>().material = material;
        //Vector3 newScale = cylinder.transform.localScale;
        Vector3 newScale = cone.transform.localScale;
        newScale.y = Vector3.Distance(lastPosition, newPosition);
        //newScale.x = scale;
        //newScale.z = scale;
        //cylinder.transform.localScale = newScale;
        cone.transform.localScale = newScale;

        cone.transform.position = lastPosition;
        cone.transform.up = newPosition - lastPosition ;
        //cylinder.transform.position = Vector3.Lerp(lastPosition, newPosition, 0.5f);
        //cylinder.transform.up = newPosition - lastPosition;

    }
}
