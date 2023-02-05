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

    public float _frequencyOffset;

    private float _frequency = -1;
    private MicrophoneFeed _mic;

    #endregion

    private void Start()
    {
        _mic = FindObjectOfType<MicrophoneFeed>();
    }

    void Update()
    {
        float freq = PitchDetectorGetFreq(0);
        if (_mic.useMicrophone)
            _frequency = _frequencyOffset + (freq / 50);
        else
            _frequency = -1;
        
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
