using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Problem
{
    public static Problem Instance {set; get;}
    public float maxTime;
    public float amtPerSec;
    public bool answered = false;

    public float answerX;
    public float answerY;
    public float answerZ;
    public float difficulty;
    int numOfAnswers;

    public string equations = "";
    public Problem(float _difficulty)
    {

      Instance = this;
      numOfAnswers = 1;
      _difficulty = difficulty;
      maxTime = 30 - (13 * Settings.Instance.difficulty );
      Debug.Log("diff: " + maxTime);
      amtPerSec = 1;
      GenRandomAnswer();
      equations = string.Format("{0} \n {1} \n {2}", GenerateSingleEquation(), "", "");

    }
    public float Round(float value, int digits)
    {
      float mult = Mathf.Pow(10.0f, (float)digits);
      return Mathf.Round(value * mult) / mult;
    }
    public void GenRandomAnswer()
    {
      float min = (Settings.Instance.difficulty * -200) + Random.Range(-50, 50);
      float max = (Settings.Instance.difficulty  * 200) + Random.Range(-50, 50);
      if (numOfAnswers == 1)
      {
      float m1 = Round(Random.Range(min, -1), 0);
      float m2 = Round (Random.Range(1, max), 0);
        answerX = Round(Random.Range(m1, m2), 0);
      }
      else if (numOfAnswers == 2)
      {
        answerX = Random.Range(difficulty * -100, difficulty * 100);
        answerY = Random.Range(difficulty * -100, difficulty * 100);
      }
      else if (numOfAnswers == 3)
      {
        answerX = Random.Range(difficulty * -100, difficulty * 100);
        answerY = Random.Range(difficulty * -100, difficulty * 100);
        answerZ = Random.Range(difficulty * -100, difficulty * 100);
      }
    }
    public string GenerateSingleEquation()
    {
        float min = (Settings.Instance.difficulty  * -200) + Random.Range(-50, 50);
        float max = (Settings.Instance.difficulty  * 200) + Random.Range(-50, 50);
        float num1 = Round(Random.Range(min, max),0);
        Debug.Log(num1);
        float num2 = 0;

        float op = Random.Range(0, 4);
        string oper = "";
        if (op == 0)
        {
          Debug.Log('+');
          oper = "+";
          num2 = num1 + answerX;

        }
        else if (op == 1)
        {
          Debug.Log('-');
          oper = "-";
          num2 = num1 - answerX;
        }
        else if (op == 2)
        {
          Debug.Log('*');
          oper = "*";
          num2 = num1 * answerX;
        }
        else if (op == 3)
        {
          Debug.Log('/');


          if (answerX != 0)
          {
            oper = "/";
            num2 = num1 / answerX;
          }
          else
          {
            oper = "*";
            num2 = num1 * answerX;
          }

        }
        num2 = Round(num2, 0);

        return string.Format("{0} {1} x = {2}", num1, oper, num2);




    }
    public bool checkAnswer(string x, string y)
    {
      switch(numOfAnswers)
      {
        case 1:
          if (x == answerX.ToString())
          {
             return true;
          }
          else
          {
            return false;
          }
        break;
      }
      return false;
    }
}
