using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleRotateDistanceSlider : MonoBehaviour
{
    private Slider scaleSlider;
    private Slider rotateSlider;
    private Slider distanceSlider; // New slider for adjusting distance
    public float scaleMinValue;
    public float scaleMaxValue;
    public float rotMinValue;
    public float rotMaxValue;
    public float distanceMinValue;
    public float distanceMaxValue;

    public Camera arCamera;

    void Start()
    {
        scaleSlider = GameObject.Find("ScaleSlider").GetComponent<Slider>();
        scaleSlider.minValue = scaleMinValue;
        scaleSlider.maxValue = scaleMaxValue;
        scaleSlider.onValueChanged.AddListener(ScaleSliderUpdate);

        rotateSlider = GameObject.Find("RotateSlider").GetComponent<Slider>();
        rotateSlider.minValue = rotMinValue;
        rotateSlider.maxValue = rotMaxValue;
        rotateSlider.onValueChanged.AddListener(RotateSliderUpdate);

        distanceSlider = GameObject.Find("DistanceSlider").GetComponent<Slider>(); // Assuming you have a slider named "DistanceSlider"
        distanceSlider.minValue = distanceMinValue;
        distanceSlider.maxValue = distanceMaxValue;
        distanceSlider.onValueChanged.AddListener(DistanceSliderUpdate);
    }

    void ScaleSliderUpdate(float value)
    {
        transform.localScale = new Vector3(value, value, value);
    }

    void RotateSliderUpdate(float value)
    {
        transform.localEulerAngles = new Vector3(transform.rotation.x, value, transform.rotation.z);
    }

    void DistanceSliderUpdate(float value)
    {
        Vector3 currentPosition = transform.position;
        Vector3 cameraForward = arCamera.transform.forward;

        // Debug log to check slider value
        Debug.Log("DistanceSlider Value: " + value);

        // Set the new position based on the distance slider value
        transform.position = currentPosition + cameraForward * value;
    }
}
