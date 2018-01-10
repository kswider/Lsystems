using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager2 : MonoBehaviour
{
    [SerializeField]
    private InputField startingSequenceInputField;
    [SerializeField]
    private InputField stepsInputField;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button addNextProductionButton;
    [SerializeField]
    private GameObject productionsGrid;
    [SerializeField]
    private GameObject production;
    [SerializeField]
    private GameObject after;

    private List<GameObject> productions;
    // Use this for initialization
    private Material material;
    void Start()
    {
        //material = Resources.Load("Materials/Barks/Bark_b9", typeof(Material)) as Material;
        // GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //cylinder.GetComponent<MeshRenderer>().material = material;
        //GameObject cone = CreateCone.Create(2, 1, 0.5f);
        //cone.GetComponent<MeshRenderer>().material = material;
        productions = new List<GameObject>();
        addNextProductionButton.onClick.AddListener(AddNextProduction);
        startButton.onClick.AddListener(StartSimulation);
        Scenes.Productions = new List<Production>();
       // parameters1Button.onClick.AddListener(delegate { StartDrawing(1); });
       // parameters2Button.onClick.AddListener(delegate { StartDrawing(2); });
       // parameters3Button.onClick.AddListener(delegate { StartDrawing(3); });
       // parameters4Button.onClick.AddListener(delegate { StartDrawing(4); });
    }



    private void AddNextProduction()
    {  
        GameObject newProduction = Instantiate(production, productionsGrid.transform);
        productions.Add(newProduction);
        Button deleteButton = newProduction.transform.Find("Header/DeleteButton").GetComponent<Button>();
        deleteButton.onClick.AddListener(delegate { DeleteProduction(newProduction); });
        Button addNextAfterButton = newProduction.transform.Find("Header/AddNextAfterButton").GetComponent<Button>();
        addNextAfterButton.onClick.AddListener(delegate { addNextAfter(newProduction); });
    }

    private void DeleteProduction(GameObject newProduction)
    {
        productions.Remove(newProduction);
        Destroy(newProduction);
    }
    private void addNextAfter(GameObject newProduction)
    {
        GameObject newAfter = Instantiate(after, newProduction.transform.Find("AftersGrid"));
        Button deleteButton = newAfter.transform.Find("DeleteButton").GetComponent<Button>();
        deleteButton.onClick.AddListener(delegate { DeleteAfter(newAfter); });
    }

    private void DeleteAfter(GameObject newAfter)
    {
        Destroy(newAfter);
    }

    private void StartSimulation()
    {
        
        Scenes.StartingSequence = Simulation.GenerateStateFromSting(startingSequenceInputField.text);
        Scenes.Steps = Int32.Parse(stepsInputField.text);
        foreach(GameObject productionGameObject in productions)
        {
            char before = productionGameObject.transform.Find("Header/Before/BeforeInputField").GetComponent<InputField>().text[0];
            List<Rule> guards = new List<Rule> { new Rule(productionGameObject.transform.Find("Header/Guard/GuardInputField").GetComponent<InputField>().text) };
            List<SimpleProduction> simpleProductions = new List<SimpleProduction>();
            foreach (Transform afterGameObject in productionGameObject.GetComponentsInChildren<Transform>().Where(t => t.name == "After(Clone)"))
            {
                String afterString = afterGameObject.Find("State/StateInputField").GetComponent<InputField>().text;
                double probability = Double.Parse(afterGameObject.Find("Probability/ProbabilityInputField").GetComponent<InputField>().text);
                simpleProductions.Add(new SimpleProduction(Simulation.GenerateFutureStateFromSting(afterString),probability));
            }

            Scenes.Productions.Add(new Production(guards, before, simpleProductions));
        }
        Scenes.Load("main");

    }

    void StartDrawing(int treeNumber)
    {
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
        Scenes.Load("main");
    }
}
