using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField] Vector3 glassStart, glassEnd;
	[SerializeField] float glassSpeed = 0.2f;
    [SerializeField] Transform glass;
	[SerializeField] HingeJoint joint;
	[SerializeField] float openRot = -90;

	float startRotation;
	float value;

	private void Start()
	{
		startRotation = transform.localRotation.eulerAngles.y;
	}

	public void MoveGlass(float dir)
	{
		value += glassSpeed * dir * Time.deltaTime;
		glass.position = Vector3.Lerp(glassStart, glassEnd, value);
	}

    public void DoorState(bool open)
	{
		if (Mathf.Abs(startRotation - transform.localRotation.eulerAngles.y) < 10 && !open)
		{
			joint.limits = new JointLimits() { min = 0};
		}
		else
		{
			joint.limits = new JointLimits() { min = openRot };
		}
	}
}