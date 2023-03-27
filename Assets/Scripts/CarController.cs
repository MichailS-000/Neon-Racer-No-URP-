using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
	private float horizontalInput, verticalInput;
	private float currentSteerAngle, currentbreakForce;
	private bool isBreaking;

	// Settings
	[SerializeField] private float motorForce, breakForce, maxSteerAngle;

	[SerializeField] SteeringWheel steeringWheel;
	[SerializeField] Transform rig;

	// Wheel Colliders
	[SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
	[SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

	// Wheels
	[SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
	[SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

	bool isControlling = false;

	float leftTriggerValue = 0;
	float rightTriggerValue = 0;


	private void FixedUpdate() {
		if (isControlling)
			GetInput();
		else
			ZeroInput();

		HandleMotor();
		HandleSteering();
		UpdateWheels();
	}

	private void ZeroInput()
	{
		horizontalInput = 0;
		verticalInput = 0;
		isBreaking = false;
	}

	private void GetInput() {
		// Steering Input
		horizontalInput = -steeringWheel.GetAxis();

		// Acceleration Input
		InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.trigger, out rightTriggerValue);
		InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.trigger, out leftTriggerValue);

		verticalInput = -rightTriggerValue + leftTriggerValue;

		// Breaking Input
		InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primaryButton, out isBreaking);

	}

	private void HandleMotor() {
		frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
		frontRightWheelCollider.motorTorque = verticalInput * motorForce;
		currentbreakForce = isBreaking ? breakForce : 0f;
		ApplyBreaking();
	}

	private void ApplyBreaking() {
		frontRightWheelCollider.brakeTorque = currentbreakForce;
		frontLeftWheelCollider.brakeTorque = currentbreakForce;
		rearLeftWheelCollider.brakeTorque = currentbreakForce;
		rearRightWheelCollider.brakeTorque = currentbreakForce;
	}

	private void HandleSteering() {
		currentSteerAngle = maxSteerAngle * horizontalInput;
		frontLeftWheelCollider.steerAngle = currentSteerAngle;
		frontRightWheelCollider.steerAngle = currentSteerAngle;
	}

	private void UpdateWheels() {
		UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
		UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
		UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
		UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
	}

	private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
		Vector3 pos;
		Quaternion rot; 
		wheelCollider.GetWorldPose(out pos, out rot);
		wheelTransform.rotation = rot;
		wheelTransform.position = pos;
	}

	public void Activate(bool val)
	{
		isControlling = val;
		rig.parent = val ? transform : null;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isControlling = true;
			rig.parent = transform;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isControlling = false;
			rig.parent = null;
		}
	}
}