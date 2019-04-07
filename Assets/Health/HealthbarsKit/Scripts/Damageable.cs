using System.Collections;
using System.Collections.Generic;
using InsaneSystems.HealthbarsKit.UI;
using UnityEngine;

namespace InsaneSystems.HealthbarsKit
{
	public class Damageable : MonoBehaviour
	{
		public event HealthbarsController.HealthChangedAction healthChangedEvent;

		[SerializeField] [Range(0.1f, 1000f)] float maxHealth = 100;
		float health;

		void Awake()
		{
			health = maxHealth;
		}

		void Start()
		{
			AddHealthbarToThisObject();
		}

		void AddHealthbarToThisObject()
		{
			var healthBar = HealthbarsController.instance.AddHealthbar(gameObject, maxHealth);
			healthChangedEvent += healthBar.OnHealthChanged;

			OnHealthChanged();
		}

		void OnHealthChanged()
		{
			if (healthChangedEvent != null)
				healthChangedEvent(health);
		}

		public void TakeDamage(float damage)
		{
			health = Mathf.Clamp(health - damage, 0, maxHealth);

			OnHealthChanged();

			if (health == 0)
				Die();
		}

		public void Die()
		{
			Destroy(gameObject);
		}
	}
}