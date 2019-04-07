using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill")]
public class Skill : ScriptableObject
{
  public new string name;
  public GameObject prefab;
  public Sprite icon;
  public int baseDamage;




}
