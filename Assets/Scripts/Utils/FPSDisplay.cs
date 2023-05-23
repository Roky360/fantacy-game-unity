using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    /// <summary>
    /// "##" will be replaced with the actual FPS counter. The rest of the string can be customised.
    /// </summary>
    [SerializeField] GameObject fpsText;

    private const string NumPlaceholder = "##";
    [SerializeField] string textPattern = $"{NumPlaceholder} FPS";

    private float _deltaTime;
    private TextMeshProUGUI _textMesh;

    private void Start()
    {
        _textMesh = fpsText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
        float fps = 1.0f / _deltaTime;
        _textMesh.text = textPattern.Replace(NumPlaceholder, ((int)fps).ToString());
    }
}