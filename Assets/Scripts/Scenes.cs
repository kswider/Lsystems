using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Scenes
{

    public static Dictionary<char, string> Rules { get; set; }
    public static List<Atom> StartingSequence { get; set; }
    public static int Steps { get; set; }
    public static Dictionary<string, double> Parameters { get; set; }
    public static List<Production> Productions { get; set; }
    public static void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void Load(string sceneName, char paramKey, string paramValue)
    {
        Scenes.Rules = new Dictionary<char, string>();
        Scenes.Rules.Add(paramKey, paramValue);
        SceneManager.LoadScene(sceneName);
    }


}