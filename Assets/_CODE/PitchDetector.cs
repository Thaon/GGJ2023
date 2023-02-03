using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchDetector : MonoBehaviour
{
    #region member variables

    public float rmsVal;
    public float dbVal;
    public float pitchVal;
    public GameObject _visualIndicator;

    private const int QSamples = 1024;
    private const float RefValue = 0.1f;
    private const float Threshold = 0.02f;

    float[] _samples;
    private float[] _spectrum;
    private string _micName;
    private AudioSource _audioSource;
    private float _previousPitchVal = 0f;

    #endregion

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(RecordMic());
    }

    private IEnumerator RecordMic()
    {
        Initialize();
        yield return new WaitForSeconds(3f);
        StartCoroutine(RecordMic());
    }

    void Initialize()
    {
        Microphone.End(_micName);

        _samples = new float[QSamples];
        _spectrum = new float[QSamples];

        string micName = "";
        foreach (string device in Microphone.devices)
        {
            if (device.Length > 0)
            {
                micName = device;
                break;
            }
        }
        _micName = micName;

        _audioSource.clip = Microphone.Start(_micName, true, 10, 44100);
        _audioSource.Play();
    }

    void Update()
    {
        if (_micName.Length > 0) AnalyzeSound();

        //Debug.Log("RMS: " + rmsVal.ToString("F2"));
        //Debug.Log(dbVal.ToString("F1") + " dB");
        //Debug.Log(pitchVal.ToString("F0") + " Hz");
    }

    void AnalyzeSound()
    {
        _audioSource.GetOutputData(_samples, 0); // fill array with samples
        int i;
        float sum = 0;
        for (i = 0; i < QSamples; i++)
        {
            sum += _samples[i] * _samples[i]; // sum squared samples
        }
        rmsVal = Mathf.Sqrt(sum / QSamples); // rms = square root of average
        dbVal = 20 * Mathf.Log10(rmsVal / RefValue); // calculate dB
        if (dbVal < -160) dbVal = -160; // clamp it to -160dB min
                                        // get sound spectrum
        GetComponent<AudioSource>().GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        var maxN = 0;
        for (i = 0; i < QSamples; i++)
        { // find max 
            if (!(_spectrum[i] > maxV) || !(_spectrum[i] > Threshold))
                continue;

            maxV = _spectrum[i];
            maxN = i; // maxN is the index of max
        }
        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < QSamples - 1)
        { // interpolate index using neighbours
            var dL = _spectrum[maxN - 1] / _spectrum[maxN];
            var dR = _spectrum[maxN + 1] / _spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        pitchVal = Mathf.SmoothStep(_previousPitchVal, freqN * AudioSettings.outputSampleRate / QSamples, .1f); // convert index to frequency
        _previousPitchVal = pitchVal;
        if (_visualIndicator)
        {
            _visualIndicator.transform.localPosition = new Vector3(0, pitchVal / 80, 0);
        }
    }

    private void OnDestroy()
    {
        Microphone.End(_micName);
    }
}
