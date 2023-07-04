using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    private int cut = 0;
    private float displayCut = 0;

    [SerializeField]
    private TextMeshProUGUI pointText;

    [SerializeField]
    private TextMeshProUGUI percentText;

    [SerializeField]
    private Image newLawnButton;

    [SerializeField]
    private TextMeshProUGUI newLawnText;

    [SerializeField]
    private GrassSpawner grassSpawner;

    public static Game instance;

    private bool percentEnabled = false;
    private bool newLawnEnabled = false;

    public float seed = 0;
    
    void Awake()
    {
        instance = this;
        seed = Random.Range(-100000, 100000);
    }

    public void Cut()
    {
        cut++;
    }

    private void Update()
    {
        displayCut = Mathf.Lerp(displayCut, cut, 5 * Time.deltaTime);
        int percent = (int)Mathf.Floor((Mathf.Ceil(displayCut) / grassSpawner.count) * 100f);
        pointText.text = percent.ToString();
        if (!percentEnabled && percent >= 85)
        {
            percentEnabled = true;
            pointText.DOFade(1f, 8f);
            percentText.DOFade(1f, 8f);
        }
        if (!newLawnEnabled && percent >= 100)
        {
            newLawnEnabled = true;
            newLawnButton.gameObject.SetActive(true);
            newLawnButton.DOFade(1f, 4f);
            newLawnText.DOFade(1f, 2f);
        }
    }

    public void NewLawn()
    {
        seed = Random.Range(-100000, 100000);
        cut = 0;
        grassSpawner.Generate();
    }
}
