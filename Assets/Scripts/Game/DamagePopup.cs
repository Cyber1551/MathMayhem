using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI value;
    private Animation anim;
    private void Start()
    {
      anim = GetComponent<Animation>();
      StartCoroutine(DestroyObject());
    }
    public void updateText(string val)
    {
      value.text = "" + val;
    }
    IEnumerator DestroyObject()
    {
      yield return new WaitForSeconds(anim.clip.length);
      Destroy(gameObject);
    }

}
