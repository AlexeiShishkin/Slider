using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _changeDuration;
    [SerializeField] private Player _player;

    private Slider _slider;
    private Coroutine _changing;
    private float _changeSpeed;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _changeSpeed = _slider.maxValue / _changeDuration;
    }

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int value, int maxValue)
    {
        float targetValue = (float)value / maxValue;

        if (_changing != null)
        {
            StopCoroutine(_changing);
        }

        _changing = StartCoroutine(Change(targetValue));
    }

    private IEnumerator Change(float targetValue)
    {
        while (_slider.value != targetValue)
        {
            yield return _slider.value = Mathf.MoveTowards(_slider.value, targetValue, _changeSpeed * Time.deltaTime);
        }

        _changing = null;
    }
}
