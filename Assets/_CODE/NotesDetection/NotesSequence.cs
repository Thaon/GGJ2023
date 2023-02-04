using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotesSequence : MonoBehaviour
{
    #region member variables

    public List<float> _sequence;
    public UnityEvent OnNoteDetected, OnSequenceComplete;
    public GameObject _visualizer;

    private int _index = -1;
    private bool _detecting = false;

    #endregion

    private void Start()
    {
        if (_visualizer)
            _visualizer.SetActive(false);
    }

    void Update()
    {
        if (_detecting)
        {
            float noteDetected = Mathf.RoundToInt(PitchDetector.Instance.Frequency()); // from 3 to 10
            float noteToHit = Mathf.RoundToInt(_sequence[_index]);
            if (noteToHit == noteDetected)
            {
                OnNoteDetected?.Invoke();
                print("Note Detected");
                NextNote();
            }
        }
    }

    public void RestartSequence()
    {
        _index = -1;
    }

    public void StartDetectingSequence()
    {
        RestartSequence();
        PitchDetector.Instance.StartDetecting();
        _index++;
        _detecting = true;
        print("Initiating Detection for: " + _sequence[_index]);
        if (_visualizer)
        {
            _visualizer.SetActive(true);
            _visualizer.transform.localPosition = new Vector3(0, _sequence[_index], 1);
        }
    }

    public void NextNote()
    {
        if (_index == _sequence.Count - 1)
        {
            OnSequenceComplete?.Invoke();
            print("Sequence Completed");
            RestartSequence();
            _detecting = false;
            _visualizer.SetActive(false);
        }
        else
        {
            _index++;
            if (_visualizer)
            {
                _visualizer.transform.localPosition = new Vector3(0, _sequence[_index], 1);
            }
        }
    }
}
