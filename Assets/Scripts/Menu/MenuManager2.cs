using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager2 : MonoBehaviour
{
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

    void StartDrawing(int treeNumber)
    {
        switch (treeNumber)
        {
            case 1:
                Scenes.addParameter("r1", 0.9);
                Scenes.addParameter("r2", 0.6);
                Scenes.addParameter("a0", 45);
                Scenes.addParameter("a2", 45);
                break;
            case 2:
                Scenes.addParameter("r1", 0.9);
                Scenes.addParameter("r2", 0.9);
                Scenes.addParameter("a0", 45);
                Scenes.addParameter("a2", 45);
                break;
            case 3:
                Scenes.addParameter("r1", 0.9);
                Scenes.addParameter("r2", 0.8);
                Scenes.addParameter("a0", 45);
                Scenes.addParameter("a2", 45);
                break;
            case 4:
                Scenes.addParameter("r1", 0.9);
                Scenes.addParameter("r2", 0.7);
                Scenes.addParameter("a0", 30);
                Scenes.addParameter("a2", -30);
                break;
        }
        Scenes.Load("main");
    }
}
