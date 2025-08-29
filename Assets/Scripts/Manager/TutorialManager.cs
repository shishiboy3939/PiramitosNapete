using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using StarterAssets;
using UnityEngine.UI;

public class Tutorialmanager : MonoBehaviour
{

    [SerializeField] private UnityEngine.UI.Image TutorialPanel;
    [SerializeField] private List<GameObject> TutorialPage;
    private int currentPage;
    [SerializeField] private GameObject NextButton;
    [SerializeField] FirstPersonController fpc;
    [SerializeField] private GraphicRaycaster raycaster; // 未指定なら自身から取得
    private GraphicRaycaster _ray;

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        //チュートリアル画面で操作を禁止する
        //StageChangerのChangeStages関数の最後にGameManager.isWaitingをfalseにする処理があり、
        //それとの兼ね合いで応急処置として一旦こうしてる
        if (currentPage < 9)
        {
            GameManager.isWaiting = true;
        }

        //3D画面で、3DチュートリアルがtrueのときGameManager.isWaitingをtrueに
        //ここまで条件を増やさなくて良いと思うけど、どんなバグが起こるか分からないから一応
        if (currentPage == 9 && GameManager.now2Dor3D == 1 && StageChanger.Instance.tutorialOn3D)
        {
            GameManager.isWaiting = true;
        }
    }

    public bool IsEnabled => _ray != null && _ray.enabled;
    public void CallTutorial()
    {
        TutorialPanel.DOFade(1f, 1f);
        NextButton.SetActive(true);
        TutorialPage[currentPage].SetActive(true);
        SetEnabled(true);
    }
    public void NextPage()
    {
        TutorialPage[currentPage].SetActive(false);
        currentPage++;
        if (currentPage < 9)
        {
            TutorialPage[currentPage].SetActive(true);
        }
        if (currentPage == 9)
        {
            TutorialPanel.DOFade(0f, 1f);
            NextButton.SetActive(false);
            StageChanger.Instance.tutorialOn2D = false;
            GameManager.isWaiting = false;
            SetEnabled(false);
        }
        if (currentPage < 17 && currentPage > 9)
        {
            TutorialPage[currentPage].SetActive(true);
            StageChanger.Instance.tutorialOn3D = true;
            GameManager.isWaiting = true;
            SetEnabled(true);
        }
        if (currentPage == 17)
        {
            TutorialPanel.DOFade(0f, 1f);
            NextButton.SetActive(false);
            GameManager.isPausing = false;
            GameManager.isWaiting = false;
            StageChanger.Instance.tutorialOn3D = false;
            StageChanger.Instance.ResumeAllAgents();
            fpc.autoForward = false;
            SoundManager.Instance.FootStepPlay(SoundManager.Instance.SE_FootStep);
            SetEnabled(false);
        }
    }
    public void SetEnabled(bool on)
    {
        if (_ray) _ray.enabled = on;
    }
    public void Reset()
    {
        StageChanger.Instance.tutorialOn2D = true;
        StageChanger.Instance.tutorialOn3D = true;
        foreach (GameObject r in TutorialPage)
        {
            r.SetActive(false);
        }
        NextButton.SetActive(false);
        TutorialPanel.DOFade(0f, 0f);
        currentPage = 0;
        _ray = raycaster ? raycaster : GetComponent<GraphicRaycaster>();
        if (_ray == null) _ray = gameObject.AddComponent<GraphicRaycaster>();
    }

}
