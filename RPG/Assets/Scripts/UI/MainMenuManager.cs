using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour 
{
    public Image blackTexture;
    public Texture2D cursorTexture;
    public GameObject newGameMenu;
    public GameObject buttons;
    public Text save1;

    private Button[] mainMenuButtons;
    private Button[] saveButtons;
    private Button[] currButtons;
    private int selectedIndex = 0;
    private bool changed = false;
    private bool rapidSelect = false;
    private float rapidSelectDuration = 0.4f;
    private float rapidChangeDuration = 0.05f;
    private float axisValue = 0;

    private float fadeInDuration = 0f;
    private float timeBeforeStartingToFadeIn = 0f;

	void Start () 
	{
        mainMenuButtons = buttons.GetComponentsInChildren<Button>();
        saveButtons = newGameMenu.GetComponentsInChildren<Button>();

        currButtons = mainMenuButtons;

        StartCoroutine(Fade(0));
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
	
	void Update () 
	{
        if(InputManager.Controller)
        {         
            if(currButtons == mainMenuButtons)
            {
                axisValue = Input.GetAxis("Vertical") + Input.GetAxis("VDpad");
            }
            else
            {
                axisValue = -Input.GetAxis("Horizontal") - Input.GetAxis("HDpad");
            }

            if(Input.GetButtonDown("A"))
            {
                currButtons[selectedIndex].onClick.Invoke();
            }
            if(Mathf.Abs(axisValue) == 1 && !changed)
            {
                if (rapidSelect)
                {
                    StartCoroutine(RapidChangeWait());
                }
                else
                {
                    StartCoroutine(RapidSelectWait());
                }

                changed = true;
                
                selectedIndex -= Mathf.RoundToInt(axisValue);
                selectedIndex = Mathf.Clamp(selectedIndex, 0, currButtons.Length - 1);
            }
            if(Mathf.Abs(axisValue) != 1)
            {
                StopCoroutine(RapidSelectWait());
                changed = false;
                rapidSelect = false;
            }
            currButtons[selectedIndex].Select();
        }
	}

    IEnumerator RapidSelectWait()
    {
        yield return new WaitForSeconds(rapidSelectDuration);
        changed = false;
        rapidSelect = true;
    }

    IEnumerator RapidChangeWait()
    {
        yield return new WaitForSeconds(rapidChangeDuration);
        changed = false;
    }

    public void NewGameClicked()
    {
        currButtons = saveButtons;

        PlayAudio();

        newGameMenu.GetComponent<CanvasGroup>().alpha = 1;
        newGameMenu.GetComponent<CanvasGroup>().interactable = true;

        buttons.GetComponent<CanvasGroup>().alpha = 0;
        buttons.GetComponent<CanvasGroup>().interactable = false;

        /*DataController.data.LoadData(0);
        save1.text = "Save 1 " + DataController.data.currentSave.hoursPlayed + " hours played.";*/
    }

    public void PlayAudio()
    {
        print("Played sound clip.");
    }

    public void SaveClicked(int id)
    {
        DataController.data.LoadData(id);
        //StartCoroutine(Fade(1, FadeCallback));  
        FadeCallback();
    }

    public void ExitGameClicked()
    {
        Application.Quit();
    }

    IEnumerator Fade(int direction, Action callback = null)
    {
        blackTexture.transform.SetAsLastSibling();
        yield return new WaitForSeconds(timeBeforeStartingToFadeIn);

        for(float i = 1; i > 0; i -= 0.01f)
        {
            Color c = blackTexture.color;
            c.a = Mathf.Abs(direction - i);
            blackTexture.color = c;
            yield return new WaitForSeconds(fadeInDuration / 100);
        }

        blackTexture.transform.SetAsFirstSibling();
        if(callback != null) callback();
        StopCoroutine(Fade(0));
    }

    void FadeCallback()
    {
        SceneManager.LoadScene(1);
    }
}
