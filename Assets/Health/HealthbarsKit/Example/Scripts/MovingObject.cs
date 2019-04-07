using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InsaneSystems.HealthbarsKit.Example
{
	public class MovingObject : MonoBehaviour
	{
		void Update()
		{
			transform.position += transform.forward * Time.deltaTime;
		}
	}
}