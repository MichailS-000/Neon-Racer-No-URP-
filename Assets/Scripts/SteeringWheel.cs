using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
	[SerializeField] HingeJoint joint;

    float rot;

    public float GetAxis()
	{
        return rot / 180f;
	}

    void FixedUpdate()
    {
        rot = joint.angle;
    }
}