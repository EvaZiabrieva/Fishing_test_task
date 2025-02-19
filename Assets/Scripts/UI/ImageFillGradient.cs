using UnityEngine;
using UnityEngine.UI;

public class ImageFillGradient : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _image;
    private void Update()
    {
        _image.color = _gradient.Evaluate(_image.fillAmount);
    }
}
