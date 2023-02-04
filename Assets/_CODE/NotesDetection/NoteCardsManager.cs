using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System.Threading.Tasks;

public class NoteCardsManager : Singleton<NoteCardsManager>
{
    #region member variables

    public float _notesOffset;
    public GameObject _cardPrefab, _pitchTargetPrefab, _pitchIndicatorPrefab;

    private List<GameObject> _cards = new List<GameObject>();
    private GameObject _indicator;

    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        if (_indicator)
        {
            _indicator.transform.localPosition = Vector3.Lerp(_indicator.transform.localPosition, new Vector3(-3f, (PitchDetector.Instance.Frequency() / 10) + _notesOffset, -1f), .1f);
        }
    }

    public void ShowCardsPanel()
    {
        GetComponent<CanvasGroup>().DOFade(1f, .5f).From(0f);
        transform.DOMoveY(0, .5f).From(-2f);
    }

    public async void HideCardsPanel()
    {
        GetComponent<CanvasGroup>().DOFade(0f, .3f).From(1f);
        transform.DOMoveY(-2f, .3f).From(0f);
        await Task.Delay(300);
        // clear indicator
        Destroy(_indicator);
        // clear previous cards
        _cards.ForEach(card => Destroy(card.gameObject));
        _cards.Clear();
    }

    public async void HideFirstCard()
    {
        GameObject toRemove = _cards[0];
        if (_cards.Count > 0)
        {
            toRemove.GetComponent<CanvasGroup>().DOFade(0f, .5f).From(1f);
            toRemove.transform.DOMoveY(4f, .5f).From(0f);
        }
        _cards.RemoveAt(0);
        if (_cards.Count > 0)
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                GameObject go = _cards[i];

                go.transform.DOMoveX(go.transform.localPosition.x - 1.5f, .3f);
                go.GetComponent<CanvasGroup>().DOFade(1f - (i / 3f), .5f);
            };
        }
        await Task.Delay(500);
        Destroy(toRemove);
    }

    public async void InitCards(List<float> notes)
    {
        // clear previous cards
        _cards.ForEach(card => Destroy(card.gameObject));
        _cards.Clear();

        // spawn new cards
        for (int i = 0; i < notes.Count; i++)
        {
            float note = notes[i];
            await Task.Delay(500);

            GameObject go = Instantiate(_cardPrefab, transform);
            go.transform.localPosition = new Vector3((i * 1.5f) - 1.5f, 0, 0);
            go.GetComponent<CanvasGroup>().DOFade(1f - (i / 3f), .5f).From(0f);
            go.transform.DOMoveY(0, .5f).From(-2f);

            _cards.Add(go);

            GameObject target = Instantiate(_pitchTargetPrefab, go.transform);
            target.transform.localPosition = new Vector3(0f, (note / 10f) + _notesOffset, -1f);
            await Task.Delay(200);
        };

        _indicator = Instantiate(_pitchIndicatorPrefab, transform);
        _indicator.GetComponent<CanvasGroup>().DOFade(1f, .5f).From(0f);
    }

    public void UpdateTargetComplation(float percent)
    {
        if (_cards[0])
        {
            _cards[0].GetComponentInChildren<TargetCompletionTracker>().SetCompletion(percent);
        }
    }

}
