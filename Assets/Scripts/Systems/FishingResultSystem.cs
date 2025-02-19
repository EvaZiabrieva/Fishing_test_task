using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Taking responsibility for updating the UI results
/// </summary>
public class FishingResultSystem : MonoBehaviour, ISystem, IUpdatable
{
    [SerializeField] private Canvas _resultsCanvas;
    [SerializeField] private GameObject _statistic;
    [SerializeField] private Text _pointsText;
    [SerializeField] private Text _weightText;
    [SerializeField] private Text _totalScoreText;

    [SerializeField] private float _distanceOffset;
    [SerializeField] private float _canvasFollowingSpeed;
    [SerializeField] private Text _resultTitle;
    [SerializeField] private Button _continueButton;
    [SerializeField] private string _successesfulTitle;
    [SerializeField] private string _failTitle;

    private FishingProgressSystem _progressSystem;
    private UpdatableSystem _updatableSystem;
    private float _totalScore = 0;
    public bool IsInitialized => _progressSystem != null;

    public void Initialize()
    {
        _updatableSystem = SystemsContainer.GetSystem<UpdatableSystem>();
        _progressSystem = SystemsContainer.GetSystem<FishingProgressSystem>();

        _progressSystem.OnFishingFinished += ShowResults;
        _progressSystem.OnGetResults += ShowStatistic;

        _continueButton.onClick.AddListener(_progressSystem.OnContinue);
        _continueButton.onClick.AddListener(HideCanvas);
    }

    public void Shutdown()
    {
        _progressSystem.OnFishingFinished -= ShowResults;
        _continueButton.onClick.RemoveListener(_progressSystem.OnContinue);
        _continueButton.onClick.RemoveListener(HideCanvas);
    }

    private void ShowResults(bool isSuccessful)
    {
        _updatableSystem.RegisterUpdatable(this);
        _resultsCanvas.gameObject.SetActive(true);

        if (isSuccessful)
        {
            _resultTitle.text = _successesfulTitle;
        }
        else
        {
            _resultTitle.text = _failTitle;
        }
    }
    private void ShowStatistic(float points, float weight)
    {
        _statistic.SetActive(true);

        _pointsText.text = $"Earned: {points} points";
        _weightText.text = $"Fish weigth: {weight} kilo";
        UpdateTotalScore(points);
    }
    private void UpdateTotalScore(float points)
    {
        _totalScore += points;
        _totalScoreText.text = _totalScore.ToString();
    }
    private void HideCanvas()
    {
        _updatableSystem.UnRegisterUpdatable(this);
        _resultsCanvas.gameObject.SetActive(false);
        _statistic.SetActive(false);
    }

    public void ExecuteUpdate()
    {
        Camera camera = Camera.main;

        Vector3 fakeForward = camera.transform.forward;
        fakeForward.y = 0.0f;
        fakeForward.Normalize();

        _resultsCanvas.transform.position = Vector3.Lerp(_resultsCanvas.transform.position, camera.transform.position + fakeForward * _distanceOffset, Time.deltaTime * _canvasFollowingSpeed);
        _resultsCanvas.transform.LookAt(camera.transform);
        _resultsCanvas.transform.Rotate(0, 180, 0);
    }
}
