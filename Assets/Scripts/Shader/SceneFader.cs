using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using TMPro;

public class SceneFader : MonoBehaviour
{
    public float fadeDuration = 1f;
    public TextMeshProUGUI text;
    private int _fadeAmount = Shader.PropertyToID("_FadeAmount");
    private int _useRadialWipe = Shader.PropertyToID("_UseShutters");

    private int? _lastEffect;
    private Image _image;
    private int day = 0;
    private Material _material;
    public bool fadeIn = false;
    public bool fadeOut = false;

    void Awake()
    {
        _image = GetComponent<Image>();
        Material mat = _image.material;
        _image.material = new Material(mat);
        _material = _image.material;

        _lastEffect = _useRadialWipe;
    }

    void Start()
    {
        WorldTime.instance.Sunset += SpendNight;
        WorldTime.instance.Day += UpdateDay;
    }

    void OnDisable()
    {
        WorldTime.instance.Sunset -= SpendNight;
        WorldTime.instance.Day -= UpdateDay;
    }

    void UpdateDay(int day)
    {
        this.day = day;
    }

    void FadeIn()
    {
        _material.SetFloat(0,1f);
        _material.SetFloat(_fadeAmount, 1f);
        StartCoroutine(HandleFade(1f,0f));
    }
    void FadeOut()
    {
        _material.SetFloat(0,1f);
        _material.SetFloat(_fadeAmount, 0f);
        StartCoroutine(HandleFade(0f,1f));
    }

    void SpendNight()
    {
        StartCoroutine(HandleFadeScreen());
    }

    IEnumerator HandleFadeScreen()
    {
        
        FadeIn();
        yield return new WaitForSeconds(1f);
        WorldTime.instance.SpendNight();
        text.gameObject.SetActive(true);
        text.SetText("Vous allez vous coucher maintenant que la nuit est tombée");
        yield return new WaitForSeconds(2f);
        text.SetText("Jour " +day+" est terminé");
        yield return new WaitForSeconds(1f);
        text.gameObject.SetActive(false);
        FadeOut();

    }
    IEnumerator HandleFade(float targetAmount, float startAmount)
    {
        float elapsedTime = 0f;
        while(elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float lerpeAmount = Mathf.Lerp(startAmount,targetAmount,(elapsedTime / fadeDuration));
            _material.SetFloat(_fadeAmount,lerpeAmount);
            yield return null;
        }
        _material.SetFloat(_fadeAmount,targetAmount);
    }

}
