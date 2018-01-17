using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Class representing production with features such as probability, guard and parameters.
/// </summary>
[Serializable]
    public class Production
    {

    [UnityEngine.SerializeField]
    private List<Rule> guards;

    public List<Rule> GetGuards()
    {
        return guards;
    }

    [UnityEngine.SerializeField]
    private char before;

    public char GetBefore()
    {
        return before;
    }

    [UnityEngine.SerializeField]
    private List<SimpleProduction> after;

    public List<SimpleProduction> GetAfter()
    {
        return after;
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="guards">Defines rules when atom can or can not be applied to production</param>
    /// <param name="before">Defines letter by which we recognize atoms that can be applied to production</param>
    /// <param name="after">Defines list of possible results of productions. Sum of probabilities in this lists must be equal to 1.0</param>
    public Production(List<Rule> guards, char before, List<SimpleProduction> after)
        {
            this.guards = guards;
            this.before = before;
            this.after = after;
        }


        
        /// <summary>
        /// Method returning effect of production
        /// </summary>
        /// <param name="parameters">parameters of atom applied to production</param>
        /// <returns>List of Atoms produced by this porduction</returns>
        public List<Atom> getAfter(List<Double> parameters)
        {
            System.Random random = new System.Random();
            Double randDouble = random.NextDouble();
            Double acc = 0;
            List<Atom> ret = new List<Atom>();

            for (int i=0; i < GetAfter().Count; i++)
            {
                acc += GetAfter()[i].GetProbability();
                if(randDouble <= acc)
                {
                    foreach(FutureAtom nAtom in GetAfter()[i].GetAfter())
                    {
                        ret.Add(nAtom.evaluate(parameters));
                    }
                    return ret;
                }
            }
            return ret;
        }

        /// <summary>
        /// Method for checking if Atom can be applied to production
        /// </summary>
        /// <param name="atom">Atom to check</param>
        /// <returns>true if atom can be applied to production, false otherwise</returns>
        public Boolean passesGuards(Atom atom)
        {
            Boolean isOK = false;

            Debug.Log(atom.GetLetter());
            for(int i=0; i < GetGuards().Count(); i++)
            {
                if (atom.GetLetter() == GetBefore() && GetGuards()[i].apply(atom.GetParameters()))
                {
                    isOK = true;
                }
            }
            return isOK;
        }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    