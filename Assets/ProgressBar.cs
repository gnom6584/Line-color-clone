using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    private RectTransform _rectTransform;

    public void Awake() => _rectTransform = GetComponent<RectTransform>();

    public void OnProgressChanged(Single progress) => _rectTransform.transform.localScale = new Vector2(progress, 1.0f);

}
