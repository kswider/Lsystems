using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

namespace Assets.Scripts
{
    class SentenceGenerator
    {
        public List<Rule> Rules { get; set; }

        public SentenceGenerator()
        {
            Rules = new List<Rule>();
        }
        public string generate(string input)
        {
            string nextSentence = "";
            foreach (char letter in input)
            {
                foreach (Rule rule in Rules)
                {
                    if (letter.Equals(rule.before))
                    {
                        nextSentence += rule.after;
                        break;
                    }
                    else
                        nextSentence += letter;
                }
            }
            return nextSentence;
        }
    }
}
