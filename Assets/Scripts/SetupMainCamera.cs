using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupMainCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = GetComponent<Camera>();
        UpdateCameraAspect();
    }

    private void UpdateCameraAspect()
    {
        float targetAspect = 16f / 9f;
        float currentAspect = (float)Screen.width / Screen.height;

        float scaleFactor = currentAspect / targetAspect;

        Rect rect = mainCamera.rect;

        if (scaleFactor < 1f)
        {
            rect.width = 1f;
            rect.height = scaleFactor;
            rect.x = 0f;
            rect.y = (1f - scaleFactor) / 2f;
        }
        else
        {
            rect.width = 1f / scaleFactor;
            rect.height = 1f;
            rect.x = (1f - 1f / scaleFactor) / 2f;
            rect.y = 0f;
        }

        mainCamera.rect = rect;
    }
}
