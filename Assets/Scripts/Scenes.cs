using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Scenes
{

    private static Dictionary<char, string> rules;
    private static string startingSentence;
    private static Dictionary<string,double> parameters;
    public static void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void Load(string sceneName, char paramKey, string paramValue)
    {
        Scenes.rules = new Dictionary<char, string>();
        Scenes.rules.Add(paramKey, paramValue);
        SceneManager.LoadScene(sceneName);
    }

    public static string getSceneStartingSentence()
    {
        return startingSentence;
    }

    public static void setSceneStartingSentence(string startingSentence)
    {
        Scenes.startingSentence = startingSentence;
    }

    public static Dictionary<char, string> getSceneRules()
    {
        return rules;
    }

    public static void addRule(char paramKey, string paramValue)
    {
        if (rules == null)
            Scenes.rules = new Dictionary<char, string>();
        Scenes.rules.Add(paramKey, paramValue);
    }

    public static Dictionary<string, double> getSceneParameters()
    {
        return parameters;
    }
    public static void addParameter(string paramKey, double paramValue)
    {
        if (parameters == null)
            parameters = new Dictionary<string, double>();
        parameters.Add(paramKey, paramValue);
    }
}