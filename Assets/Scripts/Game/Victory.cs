using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Victory : MonoBehaviour
{
    public float maxExp;
    public float exp;
    public int level;
    public Slider expS;

    // Start is called before the first frame update
    void Start()
    {
       if (PlayerPrefs.HasKey("MaxExp")){
        maxExp = PlayerPrefs.GetFloat("MaxExp");
       }
       else
       {
         maxExp = 100;
       }
       if (PlayerPrefs.HasKey("Exp")){
        exp = PlayerPrefs.GetFloat("Exp");
       }
       else
       {
         exp = 0;
       }
       if (PlayerPrefs.HasKey("Level")){
        level = PlayerPrefs.GetInt("Level");
       }
       else
       {
         level = 1;
       }

      float expGain = (Random.Range(15, 25) / 100) * maxExp;
      exp += expGain;
      if (exp >= maxExp)
      {
        level++;
        exp = 0;
        maxExp += 100;
      }
      Settings.Instance.SaveChanges(exp, maxExp, level);
      expS.value = exp / maxExp;
    }


}
