using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class Tutorialmanager : MonoBehaviour
{

    [SerializeField] private UnityEngine.UI.Image TutorialPanel;
    [SerializeField] private List<GameObject> TutorialPage;
    private int currentPage;
    [SerializeField] private GameObject NextButton;

    private void Start()
    {
        Reset();
    }
    public void CallTutorial()
    {
        TutorialPanel.DOFade(1f, 1f);
        NextButton.SetActive(true);
        TutorialPage[currentPage].SetActive(true);
        GameManager.isPausing = true;
    }
    public void NextPage()
    {
        TutorialPage[currentPage].SetActive(false);
        currentPage++;
        if (currentPage < 3)
        {
            TutorialPage[currentPage].SetActive(true);
        }
        if (currentPage == 3)
        {
            TutorialPanel.DOFade(0f, 1f);
            NextButton.SetActive(false);
            StageChanger.Instance.tutorialOn2D = false;
            GameManager.isPausing = false;
        }
        if (currentPage < 6 && currentPage > 3)
        {
            TutorialPage[currentPage].SetActive(true);
            StageChanger.Instance.tutorialOn3D = true;
            GameManager.isWaiting = true;
        }
        if (currentPage == 6)
        {
            TutorialPanel.DOFade(0f, 1f);
            NextButton.SetActive(false);
            GameManager.isPausing = false;
            GameManager.isWaiting = false;
            StageChanger.Instance.tutorialOn3D = false;
            StageChanger.Instance.ResumeAllAgents();
        }
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
    }

}
