using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    private float maxHealth;
    public float currentHealth;
    [SerializeField] private Color MinHealthColor;
    [SerializeField] private Color MaxHealthColor;
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private GameObject DamagePopup;
    [SerializeField] private Transform popupSpawn;
    [SerializeField] private GameObject statsPanel;
    public Stat[] stats;

    //[SerializeField] private
    public void Start()
    {

      stats = new Stat[4];
      float diff = Settings.Instance.difficulty;
      stats[0] = new Stat("Strength", "Modifies the damage you do with your attacks", 15 + 50 * diff);
      stats[1] = new Stat("Restitution", "Modifies your maximum health", 100 + 500 * diff);
      stats[2] = new Stat("Accuracy", "Modifies skill ratios", 0);
      stats[3] = new Stat("Spirit", "Modifies health regeneration each turn", 1 + 25 * diff);
      maxHealth = stats[1].baseValue;
      currentHealth = maxHealth;
      hpText.text = currentHealth.ToString();
      drawStats();
    }
public void Regen()
{
  if (currentHealth + stats[3].baseValue < maxHealth)
  {
    currentHealth += stats[3].baseValue;
    PopupDamage("+" + stats[3].baseValue);
  }


}
    public float getMaxHealth()
    {
      return maxHealth;
    }
    public float getCurrentHealth()
    {
      return currentHealth;
    }
    public void drawStats()
    {
       for (int i = 0; i < stats.Length; i++)
       {
         statsPanel.transform.GetChild(i + 1).GetChild(1).GetComponent<TextMeshProUGUI>().text = ""+stats[i].baseValue;
       }
    }

    public void TakeDamage(float amt)
    {
       currentHealth -= amt;

       hpText.text = currentHealth.ToString();
			fillImage.fillAmount = currentHealth / maxHealth;
      PopupDamage("-" + amt);
			fillImage.color = Color.Lerp(MinHealthColor,
				MaxHealthColor, fillImage.fillAmount);

    }
    public void PopupDamage(string txt)
    {
      GameObject dp = Instantiate(DamagePopup, popupSpawn.transform.position, popupSpawn.transform.rotation, popupSpawn);
      dp.transform.SetParent(popupSpawn);
      dp.GetComponent<DamagePopup>().updateText(txt);
    }

}
