using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /// <summary>
    /// Class representing simple production. Never used as stand alone class (only applied to Production)
    /// </summary>
    [Serializable]
    public class SimpleProduction
    {
    [UnityEngine.SerializeField]
    private List<FutureAtom> after;

    public List<FutureAtom> GetAfter()
    {
        return after;
    }

    [UnityEngine.SerializeField]
    private double probability;

    public double GetProbability()
    {
        return probability;
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="after">FutreAtom that will be produced by this Production</param>
    /// <param name="probability">Probabbility of applying this production. Its impotant to keep sum of probabilities of simpleproductions with smae before and guard equal to 1.0</param>
    public SimpleProduction(List<FutureAtom> after, Double probability)
        {
            this.after = after;
            this.probability = probability;
        }
    }