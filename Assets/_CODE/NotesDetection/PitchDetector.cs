using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

[RequireComponent(typeof(AudioSource))]
public class PitchDetector : Singleton<PitchDetector>
{
    #region member variables

    [DllImport("AudioPluginDemo")]
    private static extern float PitchDetectorGetFreq(int index);

    [DllImport("AudioPluginDemo")]
    private static extern int PitchDetectorDebug(float[] data);

    private float _frequency;
    private MicrophoneFeed _mic;

    #endregion

    private void Start()
    {
        _mic = FindObjectOfType<MicrophoneFeed>();
    }

    void Update()
    {
        float freq = PitchDetectorGetFreq(0);
        _frequency = freq / 50;

            //_visualizer.transform.localPosition = Vector3.Lerp(_visualizer.transform.localPosition, new Vector3(0, freq / 50, 0), .1f);
    }

    public float Frequency()
    {
        return _frequency;
    }

    public void StartDetecting()
    {
        _mic.useMicrophone = true;
    }

    public void StopDetecting()
    {
        _mic.useMicrophone = false;
    }
}
