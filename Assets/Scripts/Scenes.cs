using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Scenes
{
    public static int SimulationNumber { get; set; }
    public static Dictionary<char, string> Rules { get; set; }
    public static List<Atom> StartingSequence { get; set; }
    public static int Steps { get; set; }
    public static Dictionary<string, double> Parameters { get; set; }
    public static List<Production> Productions { get; set; }
    public static SerializableDictionary Dictionary { get; set; }
    public static int DrawingApproach { get; set; }
    public static float WidthDecreaseRate { get; set; }
    public static IEnumerator Load(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            yield return null;
        }
    }

    public static IEnumerator LoadAdditive(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!async.isDone)
        {
            yield return null;
        }
    }

    public static IEnumerator LoadAdditiveGoThroughEachStep(string sceneName,double timeBetweenFrames)
    {
        int repeat = Scenes.Steps;
        for (int i = 1; i <= repeat; i++)
        {
            Scenes.Steps = i;
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            while (!async.isDone)
            {
                yield return null;
            }
            if(i<repeat)
                GameObject.Find("Canvas/GoBackButton").SetActive(false);
            if (i>1)
                SceneManager.UnloadSceneAsync("main");
            yield return new WaitForSeconds((float)timeBetweenFrames);
            
        }


    }
}