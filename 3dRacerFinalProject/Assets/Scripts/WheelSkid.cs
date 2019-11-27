using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSkid : MonoBehaviour
{

    [SerializeField] float intensityModifier = 1.5f;
    Skidmarks skidMarkController;
    CarController playerCar;

    int lastSkidId = -1;

    // Start is called before the first frame update
    void Start()
    {
        skidMarkController = FindObjectOfType<Skidmarks>();
        playerCar = GetComponentInParent<CarController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float intensity = playerCar.sideSlipAmount;
        if (intensity < 0)
        {
            intensity = -intensity;
        }
        if (intensity > 0.15f)
        {
            lastSkidId = skidMarkController.AddSkidMark(transform.position, transform.up, intensity * intensityModifier, lastSkidId);
        }
        else
        {
            lastSkidId = -1;
        }
    }
}
