using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    string question;
    string[] options;
    int correct;

    public Question(string question, string[] options, int corr)
    {
        if (options.Length != 4)
            throw new System.Exception("Wrong number of options");
        this.question = question;
        this.options = options;
        correct = corr;
    }

    public string GetQuestion()
    {
        return (string)question.Clone();
    }

    public string[] GetOptions() {
        return (string[])options.Clone();
    }

    public int GetCorrect()
    {
        return correct;
    }

    public override bool Equals(object obj)
    {
        return obj is Question question &&
               base.Equals(obj) &&
               this.question == question.question &&
               EqualityComparer<string[]>.Default.Equals(options, question.options) &&
               correct == question.correct;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), question, options, correct);
    }
}
