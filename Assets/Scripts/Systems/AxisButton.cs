using System;
using UnityEngine;

/// <summary>
/// <para>Represent an axis in Unity that is interpreted each frame to listen for button events.</para>
/// <para>NOTE: This class will NOT WORK if you don't call OnUpdate() properly.</para>
/// </summary>
public class AxisButton {
	private string axisName;
	private float threshold = 0.5f;

	//Variables updated during Update
	private float axisValue;
	private bool getButtonDown;
	private bool getButtonUp;

	public string AxisName => axisName;
	public float Thresold => threshold;

	public AxisButton(string axisName) {
        GameManager.update += OnUpdate;
		if (axisName == null)
			throw new ArgumentNullException("axisName");
		this.axisName = axisName;
	}

	public AxisButton(string axisName, float threshold) : this(axisName) {
		if (threshold < 0 || threshold > 1)
			throw new ArgumentOutOfRangeException("threshold");
		this.threshold = threshold;
	}

	public void OnUpdate() {
		float prevAxisValue = axisValue;
		//axisValue = Input.GetAxis(axisName);
		//axisValue = Input.GetAxis(axisName);

		getButtonDown = (axisValue >= threshold && prevAxisValue < threshold);
		getButtonUp = (axisValue < threshold && prevAxisValue >= threshold);
	}

	public bool GetButton() => axisValue >= threshold;
	public bool GetButtonDown() => getButtonDown;
	public bool GetButtonUp() => getButtonUp;
}