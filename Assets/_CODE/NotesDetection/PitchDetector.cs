using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

[RequireComponent(typeof(AudioSource))]
public class PitchDetector : Singleton<PitchDetector>
{
    #region member variables

    public GameObject _visualizer;

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
        if (_visualizer)
            _visualizer.SetActive(false);
    }

    void Update()
    {
        float freq = PitchDetectorGetFreq(0);
        _frequency = freq / 50;

        if (_visualizer)
            _visualizer.transform.localPosition = new Vector3(0, freq / 50, 0);
    }

    public float Frequency()
    {
        return _frequency;
    }

    public void StartDetecting()
    {
        _mic.useMicrophone = true;
        _visualizer.SetActive(true);
    }

    public void StopDetecting()
    {
        _mic.useMicrophone = false;
        _visualizer.SetActive(false);
    }
}
