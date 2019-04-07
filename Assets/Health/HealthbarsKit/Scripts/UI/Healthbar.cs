using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InsaneSystems.HealthbarsKit.UI
{
	public class Healthbar : MonoBehaviour
	{
		[SerializeField] Image fillImage;

		RectTransform rectTransform;

		Transform target;
		float maxHealthValue;
		float targetHeight = 1f;

		void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			rectTransform.anchorMin = new Vector2(0, 0);
			rectTransform.anchorMax = new Vector2(0, 0);
		}

		public void OnUpdate()
		{
			if (!target)
			{
				Destroy(gameObject);
				return;
			}

			rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(target.position + Vector3.up * targetHeight);
		}

		public void SetColorByFillValue()
		{
			fillImage.color = Color.Lerp(HealthbarsController.instance.MinHealthColor,
				HealthbarsController.instance.MaxHealthColor, fillImage.fillAmount);

			if (HealthbarsController.instance.UseHDRForBetterColoring)
				fillImage.color *= 2f;
		}

		public void SetupWithTarget(Transform newTarget, float targetMaxHealth)
		{
			target = newTarget;
			maxHealthValue = targetMaxHealth;
		}

		public void SetTargetHeight(float newHeight)
		{
			targetHeight = newHeight;
		}

		public void OnHealthChanged(float healthValue)
		{
			fillImage.fillAmount = healthValue / maxHealthValue;

			if (HealthbarsController.instance.SetColorByHealthPecents)
				SetColorByFillValue();

			if (fillImage.fillAmount <= 0)
				Destroy(gameObject);

			if (HealthbarsController.instance.HideHealthbarsIfHealthFull)
				gameObject.SetActive(fillImage.fillAmount < 1f);
		}
	}
}