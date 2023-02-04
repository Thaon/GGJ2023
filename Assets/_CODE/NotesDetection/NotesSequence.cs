using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NotesSequence : MonoBehaviour
{
    #region member variables

    public float _timeBetweenNotesRecognized = 1f;
    public List<float> _sequence;
    public UnityEvent OnNoteDetected, OnSequenceComplete;

    private int _index = -1;
    private bool _detecting = false;
    private bool _canDetect = false;
    private float _gotNoteTimer = 0f;

    #endregion

    void Update()
    {
        if (_detecting && _canDetect)
        {
            float noteDetected = Mathf.RoundToInt(PitchDetector.Instance.Frequency()); // from 3 to 10
            float noteToHit = Mathf.RoundToInt(_sequence[_index]);
            if (noteToHit == noteDetected)
            {
                if (_gotNoteTimer > 1f)
                {
                    OnNoteDetected?.Invoke();
                    print("Note Detected");
                    NextNote();
                    _gotNoteTimer = 0f;
                }
                else
                {
                    _gotNoteTimer += Time.deltaTime;
                }
            }
            else
            {
                _gotNoteTimer -= Time.deltaTime;
                _gotNoteTimer = Mathf.Clamp01(_gotNoteTimer);
            }
            NoteCardsManager.Instance.UpdateTargetComplation(_gotNoteTimer);
        }
        //_visualizer.transform.localPosition = Vector3.Lerp(_visualizer.transform.localPosition, new Vector3(0, _sequence[_index], 1), .1f);
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
        print("Initiating Detection for: " + (_sequence[_index]));
        _canDetect = false;
        StartCoroutine(NextnoteActiveCO());

        // notify notes card canvas
        NoteCardsManager.Instance.ShowCardsPanel();
        NoteCardsManager.Instance.InitCards(_sequence);
    }

    public void NextNote()
    {
        if (_index == _sequence.Count - 1)
        {
            OnSequenceComplete?.Invoke();
            print("Sequence Completed");
            RestartSequence();
            _detecting = false;
            NoteCardsManager.Instance.HideCardsPanel();
        }
        else
        {
            _index++;
            print("Initiating Detection for: " + (_sequence[_index]));
            _canDetect = false;
            StartCoroutine(NextnoteActiveCO());
            NoteCardsManager.Instance.HideFirstCard();
        }
    }

    private IEnumerator NextnoteActiveCO()
    {
        yield return new WaitForSeconds(_timeBetweenNotesRecognized);
        _canDetect = true;
    }
}
