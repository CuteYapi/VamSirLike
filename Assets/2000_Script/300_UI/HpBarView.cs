using UnityEngine;
using UnityEngine.UI;

public class HpBarView : MonoBehaviour, IUI
{
    private RectTransform mViewRectTransform;
    private Slider mSlider;

    public void Initialize()
    {
        gameObject.SetActive(false);
        mViewRectTransform = GetComponent<RectTransform>();
        mSlider = GetComponent<Slider>();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        mViewRectTransform.position = position;
    }

    public void SetSliderRatio(float ratio)
    {
        mSlider.value = ratio;
    }
}
