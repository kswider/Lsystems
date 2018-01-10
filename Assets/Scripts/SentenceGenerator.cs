using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SentenceGenerator
    {
        public List<OldRule> Rules { get; set; }

        public SentenceGenerator()
        {
            Rules = new List<OldRule>();
        }
    public string generate(string input)
    {
        string nextSentence = "";
        foreach (char letter in input)
        {
            bool applicated = false;
            foreach (OldRule rule in Rules)
            {
                if (letter.Equals(rule.before))
                {
                    nextSentence += rule.after;
                    applicated = true;
                    break;
                }
            }
            if (!applicated)
                nextSentence += letter;
        }
        return nextSentence;
    }
}
