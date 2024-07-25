using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEfect : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier;
    private Transform cameraTransform;
    private Vector3 cameraPreviusPosition;
    void Start()
    {
        cameraTransform = Camera.main.transform;
        cameraPreviusPosition = cameraTransform.position;
    }

   
    void LateUpdate()
    {
        float deltax = (cameraTransform.position.x - cameraPreviusPosition.x)*parallaxMultiplier;
        transform.Translate(new Vector3(deltax, 0, 0));
        cameraPreviusPosition = cameraTransform.position;
    }
}
