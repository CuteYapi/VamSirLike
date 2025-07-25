using TMPro;
using UnityEngine;
using System.Collections;

public class DamageView : MonoBehaviour, IUI
{
    private TextMeshProUGUI mText;

    private float mTimer = 2.5f;

    private Coroutine mCoFadeOut;

    private RectTransform mViewRectTransform;

    public void Initialize()
    {
        mText = GetComponent<TextMeshProUGUI>();
        mViewRectTransform = GetComponent<RectTransform>();
    }

    public void Open()
    {
        gameObject.SetActive(true);

        mCoFadeOut = StartCoroutine(CoFadeOut());
    }

    public void Close()
    {
        mText.text = string.Empty;
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        mViewRectTransform.position = position;
    }

    public void SetText(int text)
    {
        mText.text = text.ToString();
    }

    private IEnumerator CoFadeOut()
    {
        yield return new WaitForSeconds(mTimer);

        Close();
    }
}
