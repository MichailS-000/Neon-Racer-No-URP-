using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Tumbler : MonoBehaviour
{
    public UnityEvent onActive;
	public UnityEvent onPassive;

	bool isActive = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("XRController"))
		{
			isActive = !isActive;
		}

		if (isActive)
			onActive.Invoke();
		else
			onPassive.Invoke();
	}
}