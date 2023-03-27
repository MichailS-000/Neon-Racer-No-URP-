using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
	[SerializeField] HingeJoint joint;

    float rot;

    public float GetAxis()
	{
        return Mathf.Abs(rot / 180f) > 0.05f ? rot / 180f : 0;
	}

    void FixedUpdate()
    {
        rot = joint.angle;
    }
}