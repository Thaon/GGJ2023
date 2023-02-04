using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System.Threading.Tasks;

public class NoteCardsManager : Singleton<NoteCardsManager>
{
    #region member variables

    public GameObject _cardPrefab, _pitchTargetPrefab;

    private List<GameObject> _cards = new List<GameObject>();

    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShowCardsPanel()
    {
        GetComponent<CanvasGroup>().DOFade(1f, .5f).From(0f);
        transform.DOMoveY(0, .5f).From(-2f);
    }

    public void HideCardsPanel()
    {
        GetComponent<CanvasGroup>().DOFade(0f, .3f).From(1f);
        transform.DOMoveY(-2f, .3f).From(0f);
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
            await Task.Delay(1000);

            GameObject go = Instantiate(_cardPrefab, transform);
            go.transform.localPosition = new Vector3((i * 1.5f) - 1.5f, 0, 0);
            go.GetComponent<CanvasGroup>().DOFade(1f - (i / 3f), .5f).From(0f);
            go.transform.DOMoveY(0, .5f).From(-2f);

            _cards.Add(go);

            await Task.Delay(500);
            GameObject target = Instantiate(_pitchTargetPrefab, go.transform);
            target.transform.localPosition = new Vector3(0f, note, -1f);
        };

        await Task.Delay(500);
        HideFirstCard();
        await Task.Delay(500);
        HideFirstCard();
        await Task.Delay(500);
        HideFirstCard();
    }
}
