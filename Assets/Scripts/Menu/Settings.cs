using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{
  public float difficulty;
public static Settings Instance {set; get;}
   public void Start()
   {
    Instance = this;
     DontDestroyOnLoad(gameObject);
      difficulty = 0.0f;

   }

  public void ChangeDifficulty(Slider s)
  {
    difficulty = s.value;
    PlayerPrefs.SetFloat("Difficulty", s.value);
  }

  public void SaveChanges(float exp, float expMax, int level)
  {
     PlayerPrefs.SetFloat("Exp", exp);
    PlayerPrefs.SetFloat("MaxExp", expMax);
    PlayerPrefs.SetInt("Level", level);
  }

}
