using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;
using SFB;
using System.Collections;
//using UnityEditor;
//using System.Runtime.InteropServices;


public class MenuManager2 : MonoBehaviour
{
    /* TODO
    [DllImport("user32.dll")]
    private static extern void SaveFileDialog();
    [DllImport("user32.dll")]
    private static extern void OpenFileDialog();
    */
    [SerializeField]
    private UnityEngine.UI.Button goBackButton;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private InputField startingSequenceInputField;
    [SerializeField]
    private InputField stepsInputField;
    [SerializeField]
    private UnityEngine.UI.Button startButton;
    [SerializeField]
    private UnityEngine.UI.Button addNextProductionButton;
    [SerializeField]
    private UnityEngine.UI.Button loadDictionary;
    [SerializeField]
    private UnityEngine.UI.Button loadFromJsonButton;
    [SerializeField]
    private UnityEngine.UI.Button saveToJsonButton;
    [SerializeField]
    private GameObject productionsGrid;
    [SerializeField]
    private GameObject production;
    [SerializeField]
    private GameObject after;
    [SerializeField]
    private Toggle cyllinderToggle;
    [SerializeField]
    private Toggle coneToggle;
    [SerializeField]
    private InputField coneInputField;
    [SerializeField]
    private GameObject fileBrowserPrefab;
    private Animator cameraAnimator;
    private List<GameObject> productions;
    // Use this for initialization
    private Material material;
    void Start()
    {
        goBackButton.onClick.AddListener(delegate { SceneManager.LoadScene("menu"); });
        productions = new List<GameObject>();
        addNextProductionButton.onClick.AddListener(delegate { AddNextProduction(); });
        startButton.onClick.AddListener(StartSimulation);
        loadDictionary.onClick.AddListener(LoadDictionary);
        loadFromJsonButton.onClick.AddListener(LoadFromJson);
        saveToJsonButton.onClick.AddListener(SaveToJson);
        cameraAnimator = GameObject.Find("Main Camera").GetComponent<Animator>();

    }

    void Update()
    {
        if (cyllinderToggle.isOn)
            coneInputField.interactable = false;
        else
            coneInputField.interactable = true;

        //Stopping Camera Animation
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (cameraAnimator.enabled)
                cameraAnimator.enabled = false;
            else
                cameraAnimator.enabled = true;
        }
    }

    private void LoadDictionary()
    {
        String path = StandaloneFileBrowser.OpenFilePanel("Load Dictionary for your Lsystem", "", "json", false)[0];
        String myJson;
        if (path.Length != 0)
        {
            Scenes.Dictionary = new SerializableDictionary();
            myJson = File.ReadAllText(path);
            JObject json = JObject.Parse(myJson);

            JArray entries = JArray.Parse(json["Dictionary"].ToString());
            foreach (JObject entry in entries)
            {
                String letter = entry["Letter"].ToString();
                Debug.Log(letter);
                String command = entry["Command"].ToString();
                Debug.Log(command);
                
                JArray arguments = JArray.Parse(entry["Arguments"].ToString());
                List<Equation> equations = new List<Equation>();
                
                foreach(String argument in arguments.Select(a => (string)a).ToList<String>())
                {
                    equations.Add(new Equation(argument));
                    Debug.Log(argument);
                }
                Scenes.Dictionary.Add(letter[0], new FutureCommand(command, equations));
            }
        }
    }

    
    private void SaveToJson()
    {
        String path = StandaloneFileBrowser.SaveFilePanel("Save Lsystem to JSON file", "", "MyLsystem.json", "json");
        if (path.Length != 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter();
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Starting sequence");
                writer.WriteValue(startingSequenceInputField.text);
                writer.WritePropertyName("Steps");
                writer.WriteValue(stepsInputField.text);
                writer.WritePropertyName("Productions");
                writer.WriteStartArray();
                foreach(GameObject prod in productions)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("Before");
                    writer.WriteValue(prod.transform.Find("Header/Before/BeforeInputField").GetComponent<InputField>().text[0]);
                    writer.WritePropertyName("Guard");
                    writer.WriteValue(prod.transform.Find("Header/Guard/GuardInputField").GetComponent<InputField>().text);
                    writer.WritePropertyName("Afters");
                    writer.WriteStartArray();
                    foreach (Transform afterGameObject in prod.GetComponentsInChildren<Transform>().Where(t => t.name == "After(Clone)"))
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName("Probability");
                        writer.WriteValue(afterGameObject.Find("Probability/ProbabilityInputField").GetComponent<InputField>().text);
                        writer.WritePropertyName("State");
                        writer.WriteValue(afterGameObject.Find("State/StateInputField").GetComponent<InputField>().text);
                        writer.WriteEndObject();
                    }
                    writer.WriteEndArray();
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();

            }
            File.WriteAllText(path, sw.ToString());
        }
    }

    private void LoadFromJson()
    {
        String path = StandaloneFileBrowser.OpenFilePanel("Select JSON file containing Lsystem", "", "json", false)[0];
        String myJson;
        if (path.Length != 0)
        {
            myJson = File.ReadAllText(path);
            DeleteAllProductions();
            JObject json = JObject.Parse(myJson);
            startingSequenceInputField.text = json["Starting sequence"].ToString();
            stepsInputField.text = json["Steps"].ToString();

            JArray productions = JArray.Parse(json["Productions"].ToString());

            foreach(JObject jprod in productions)
            {

                GameObject prodGO = AddNextProduction(jprod["Before"].ToString(), jprod["Guard"].ToString());
                JArray afters = JArray.Parse(jprod["Afters"].ToString());
                foreach(JObject jafter in afters)
                {
                    GameObject newAfter = Instantiate(after, prodGO.transform.Find("AftersGrid"));
                    Button deleteButton = newAfter.transform.Find("DeleteButton").GetComponent<Button>();
                    deleteButton.onClick.AddListener(delegate { DeleteAfter(newAfter); });
                    newAfter.transform.Find("Probability/ProbabilityInputField").GetComponent<InputField>().text = jafter["Probability"].ToString();
                    newAfter.transform.Find("State/StateInputField").GetComponent<InputField>().text = jafter["State"].ToString();
                }
            }
        }
    }

    private void DeleteAllProductions()
    {
        foreach(GameObject prod in productions)
        {
            Destroy(prod);
        }
        productions = new List<GameObject>();
    }

    private GameObject AddNextProduction(String before = "", String guard = "")
    {
        GameObject newProduction = Instantiate(production, productionsGrid.transform);
        productions.Add(newProduction);
        Button deleteButton = newProduction.transform.Find("Header/DeleteButton").GetComponent<Button>();
        deleteButton.onClick.AddListener(delegate { DeleteProduction(newProduction); });
        Button addNextAfterButton = newProduction.transform.Find("Header/AddNextAfterButton").GetComponent<Button>();
        addNextAfterButton.onClick.AddListener(delegate { addNextAfter(newProduction); });

        newProduction.transform.Find("Header/Before/BeforeInputField").GetComponent<InputField>().text = before;
        newProduction.transform.Find("Header/Guard/GuardInputField").GetComponent<InputField>().text = guard;
        return newProduction;
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
        canvas.enabled = false;
        PassParamtersFromInputs();
        Scenes.SimulationNumber = 6;
        StartCoroutine(Scenes.LoadAdditive("main"));
        
    }


    private void PassParamtersFromInputs()
    {
        Scenes.StartingSequence = Simulation.GenerateStateFromSting(startingSequenceInputField.text);
        Scenes.Steps = Int32.Parse(stepsInputField.text);
        Scenes.Productions = new List<Production>();
        foreach (GameObject productionGameObject in productions)
        {
            char before = productionGameObject.transform.Find("Header/Before/BeforeInputField").GetComponent<InputField>().text[0];
            List<Rule> guards = new List<Rule> { new Rule(productionGameObject.transform.Find("Header/Guard/GuardInputField").GetComponent<InputField>().text) };
            List<SimpleProduction> simpleProductions = new List<SimpleProduction>();
            foreach (Transform afterGameObject in productionGameObject.GetComponentsInChildren<Transform>().Where(t => t.name == "After(Clone)"))
            {
                String afterString = afterGameObject.Find("State/StateInputField").GetComponent<InputField>().text;
                double probability = Double.Parse(afterGameObject.Find("Probability/ProbabilityInputField").GetComponent<InputField>().text);
                simpleProductions.Add(new SimpleProduction(Simulation.GenerateFutureStateFromSting(afterString), probability));
            }

            Scenes.Productions.Add(new Production(guards, before, simpleProductions));
        }

        if (cyllinderToggle.isOn)
        {
            Scenes.DrawingApproach = 0;
        }
        else
        {
            Scenes.DrawingApproach = 1;
            Scenes.WidthDecreaseRate = (float)Double.Parse(coneInputField.text);
        }
    }

}
