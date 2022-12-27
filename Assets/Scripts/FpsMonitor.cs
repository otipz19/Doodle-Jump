using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMonitor : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text fpsText;

    private void Update()
    {
        fpsText.text = Time.deltaTime.ToString();
    }
}
