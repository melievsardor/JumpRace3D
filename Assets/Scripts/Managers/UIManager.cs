using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour, IGameState
{
    [SerializeField]
    private CanvasGroup finishGroup;

    [SerializeField]
    private Text stateText;

    [SerializeField]
    private Text numberText0, numberText1, numberText2;

    [SerializeField]
    private Text nameText0, nameText1, nameText2;

    [SerializeField]
    private Image image0, image1, image2;

    [SerializeField]
    private List<Text> nameTexts = new List<Text>();

    [SerializeField]
    private Color playerPanelColor;

    [SerializeField]
    private Color enemyPanelColor;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Text currentLevelText;

    [SerializeField]
    private Text nextLevelText;


    private bool isFailed;

    private void Start()
    {
        GameManager.Instance.AddState(this);

        slider.maxValue = GameManager.Instance.GetGameStates.LevelItemCount;

        int level = GameManager.Instance.GetGameStates.Level;
        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();
    }

    public void OnClickStart()
    {
        GameManager.Instance.SetPlay();
    }

    public void OnClickContinue()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
    }


    public void Play()
    {
        
    }

    public void Failed()
    {
        isFailed = true;
        SetState("Level Failed");
    }

    public void Finish()
    {
        SetState("Completed Level");
    }

    private void SetState(string value)
    {
        var players = GameManager.Instance.GetPlayers;

        for (int i = 0; i < nameTexts.Count; i++)
        {

            if(isFailed)
            {
                if(players[i].IsPlayer)
                {
                    nameTexts[nameTexts.Count - 1].text = "You";

                    nameTexts[i].text = players[nameTexts.Count - 1].PlayerName;
                }
                else if(i != nameTexts.Count - 1)
                {
                    nameTexts[i].text = players[i].PlayerName;
                }
                
            }
            else
            {
                nameTexts[i].text = players[i].PlayerName;
            }
        }

        stateText.text = value;
        finishGroup.DOFade(1f, 1f);
        finishGroup.interactable = true;
        finishGroup.blocksRaycasts = true;
    }

    

    public void Leadrboard()
    {
        var players = GameManager.Instance.GetPlayers;

        int k = 0;
        for(int i = 0; i < players.Count; i++)
        {
            if(players[i].IsPlayer)
            {
                k = i;
                break;
            }
        }

        if (k == 0)
        {
            image0.color = playerPanelColor;
            image1.color = image2.color = enemyPanelColor;
        }
        else if (k == 1)
        {
            image1.color = playerPanelColor;
            image0.color = image2.color = enemyPanelColor;
        }
        else
        {
            image2.color = playerPanelColor;
            image0.color = image1.color = enemyPanelColor;
        }

        UpdateState(players, k);

    }

    private void UpdateState(List<PlayerController> players, int k)
    {
        if (k == 0 || k == 1)
        {
            nameText0.text = players[0].PlayerName;
            numberText0.text = 1.ToString();

            nameText1.text = players[1].PlayerName;
            numberText1.text = 2.ToString();

            nameText2.text = players[2].PlayerName;
            numberText2.text = 3.ToString();
        }
        else
        {
            nameText0.text = players[0].PlayerName;
            numberText0.text = 1.ToString();

            nameText1.text = players[1].PlayerName;
            numberText1.text = 2.ToString();

            nameText2.text = players[k].PlayerName;
            numberText2.text = (k + 1).ToString();
        }

        slider.value = slider.maxValue - players[k].Index;
        
    }

    
}
