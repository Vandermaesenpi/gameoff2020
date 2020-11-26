using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class IntroManager : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject skipIntro;
    public GameObject rayCastBlockerBuild;
    public GameObject rayCastBlockerPlay;
    public GameObject sun;
    public GameObject earth;
    public GameObject supernova;
    public Text dialogText;
    public Image faceScientistImage;
    public Image facePresidentImage;
    public int charPerWait;
    public int charPerVoice;
    public List<Sprite> facesScientist;
    public List<Sprite> facesPresident;
    public Animator animator;
    public Animator bootAnimator;
    public Animator tutorialScreenAnimator;
    public CanvasGroup controlUIgroup;
    public MoonRotator rotator;
    public BuildingSpot controlSpot;
    public GameObject houseButton;
    public GameObject researchButton;
    public GameObject leisureButton;
    public BuildingSpot startSpot1,startSpot2,startSpot3;

    public List<string> startDialog;
    public List<string> shieldUpDialog;
    public List<string> aloneInSpaceDialog;
    public List<string> controlTutorial;
    public List<string> buildProductionTutorial;
    public List<string> playTutorial;
    public List<string> buildHousingAlert;
    public List<string> buildCultureAlert;
    int currentStep = 0;
    int currentStepLine = 0;

    public Coroutine dialogRoutine;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroCoroutine());
    }

    

    public IEnumerator IntroCoroutine(){
        Analytics.CustomEvent("StartGame");
        controlUIgroup.alpha = 0;
        dialogRoutine = StartCoroutine(ProcessDialogLine(startDialog));
        while(currentStep == 0){
            yield return null;
        }
        animator.Play("ShieldUp");
        GM.I.sfx.PlayFaithfull(SFX.ShieldUp);
        yield return new WaitForSeconds(3f);
        dialogRoutine = StartCoroutine(ProcessDialogLine(shieldUpDialog));
        while(currentStep == 1){
            yield return null;
        }
        animator.Play("Supernova");
        GM.I.sfx.PlayFaithfull(SFX.SuperNova);
        yield return new WaitForSeconds(7f);
        GM.I.audioManager.canPlay = true;
        yield return new WaitForSeconds(9f);
        dialogRoutine = StartCoroutine(ProcessDialogLine(aloneInSpaceDialog));
        while(currentStep == 2){
            yield return null;
        }
        bootAnimator.Play("Boot");
        yield return new WaitForSeconds(1.5f);
        for (float i = 0; i < 100f; i++)
        {
            controlUIgroup.alpha = Mathf.Lerp(0,1,i/100f);
            yield return null;
        }
        bootAnimator.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        dialogRoutine = StartCoroutine(ProcessDialogLine(controlTutorial, false));
        while(currentStepLine != 3){
            yield return null;
        }
        tutorialScreenAnimator.Play("TimeKeeper");
        while(currentStepLine != 4){
            yield return null;
        }
        tutorialScreenAnimator.Play("Resources");
        while(currentStepLine != 5){
            yield return null;
        }
        tutorialScreenAnimator.Play("Population");
        while(currentStepLine != 6){
            yield return null;
        }
        tutorialScreenAnimator.Play("Idle");
        while(currentStepLine != 7){
            yield return null;
        }
        tutorialScreenAnimator.Play("BuildButton");
        while (!GM.I.ui.buildingMenu.gameObject.activeInHierarchy)
        {
            yield return null;
        }
        tutorialScreenAnimator.Play("Idle");
        rotator.ShowBuildingSpot(controlSpot.transform);
        rayCastBlockerBuild.SetActive(false);
        dialogRoutine = StartCoroutine(ProcessDialogLine(buildProductionTutorial, false));
        skipIntro.SetActive(false);
        while (startSpot1.currentBuilding == null ||startSpot2.currentBuilding == null ||startSpot3.currentBuilding == null)
        {
            yield return null;
        }
        GM.I.ui.CloseAllUI();
        GM.I.gameplay.PauseTime(true);
        dialogRoutine = StartCoroutine(ProcessDialogLine(playTutorial, false));
        rayCastBlockerPlay.SetActive(true);
        while(currentStepLine != 5){
            yield return null;
        }
        tutorialScreenAnimator.Play("Play");
        while (GM.I.gameplay.timePaused)
        {
            yield return null;
        }
        rayCastBlockerPlay.SetActive(false);
        tutorialScreenAnimator.Play("Idle");
        dialogBox.SetActive(false);
        while (!controlSpot.HighPopulated)
        {
            yield return null;
        }
        GM.I.ui.CloseAllUI();
        rayCastBlockerBuild.SetActive(true);
        GM.I.gameplay.PauseTime(true);
        dialogRoutine = StartCoroutine(ProcessDialogLine(buildHousingAlert));
        while(currentStepLine != 5){
            yield return null;
        }
        tutorialScreenAnimator.Play("BuildButton");
        while (!GM.I.ui.buildingMenu.gameObject.activeInHierarchy)
        {
            yield return null;
        }
        tutorialScreenAnimator.Play("Idle");
        rayCastBlockerBuild.SetActive(false);
        houseButton.SetActive(true);
        while (GM.I.people.culture > 0.9f)
        {
            yield return null;            
        }
        GM.I.ui.CloseAllUI();
        rayCastBlockerBuild.SetActive(true);
        GM.I.gameplay.PauseTime(true);
        dialogRoutine = StartCoroutine(ProcessDialogLine(buildCultureAlert));
        while(currentStepLine != 5){
            yield return null;
        }
        tutorialScreenAnimator.Play("BuildButton");
        while (!GM.I.ui.buildingMenu.gameObject.activeInHierarchy)
        {
            yield return null;
        }
        tutorialScreenAnimator.Play("Idle");
        researchButton.SetActive(true);
        leisureButton.SetActive(true);
        rayCastBlockerBuild.SetActive(false);
        Analytics.CustomEvent("IntroFinished");
        
    }

    public void SkipIntro(){
        StopCoroutine(dialogRoutine);
        GM.I.audioManager.canPlay = true;
        bootAnimator.gameObject.SetActive(false);
        tutorialScreenAnimator.Play("Idle");
        animator.enabled = false;
        dialogBox.SetActive(false);
        skipIntro.SetActive(false);
        rayCastBlockerBuild.SetActive(false);
        rayCastBlockerPlay.SetActive(false);
        sun.SetActive(false);
        earth.SetActive(false);
        supernova.SetActive(true);
        houseButton.SetActive(true);
        researchButton.SetActive(true);
        leisureButton.SetActive(true);
        controlUIgroup.alpha = 1;
        Analytics.CustomEvent("IntroSkipped");
    }

    public IEnumerator ProcessDialogLine(List<string> dialog, bool closeOnEnd = true){
        dialogBox.SetActive(true);
        currentStepLine = 0;
        for (int i = 0; i < dialog.Count; i++)
        {
            faceScientistImage.gameObject.SetActive(false);
            facePresidentImage.gameObject.SetActive(false);
            string currentLine = "";
            int voiceTimer = charPerVoice;
            int waitTimer = charPerWait;
            float pitch = 0;
            GM.I.sfx.Play(SFX.Talk, pitch);
            Image faceImage;
            List<Sprite> faces;
            if(dialog[i][0] == 'P'){
                faceImage = facePresidentImage;
                faces = facesPresident;
                pitch = 1f;
            }else{
                faceImage = faceScientistImage;
                faces = facesScientist;
                pitch = 1.2f;
            }
            faceImage.gameObject.SetActive(true);
            faceImage.sprite = faces[Random.Range(0, faces.Count)];
            for (int c = 2; c < dialog[i].Length; c++)
            {
                currentLine += dialog[i][c];
                dialogText.text = currentLine;
                waitTimer--;
                voiceTimer--;
                if(voiceTimer == 0 && dialog[i][c] != '.'){
                    GM.I.sfx.Play(SFX.Talk, pitch);
                    voiceTimer = charPerVoice;
                    faceImage.sprite = faces[Random.Range(0, faces.Count)];
                }
                if(waitTimer == 0){
                    
                    yield return null;
                    
                    waitTimer = charPerWait;
                }
                if(Input.GetMouseButtonDown(0)){
                    c = 10000000;
                    dialogText.text = dialog[i].Substring(2);
                    yield return null;
                }
            }

            faceImage.sprite = faces[0];

            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }

            currentStepLine ++;
        }
        if(closeOnEnd){
            dialogBox.SetActive(false);
        }
        currentStep ++;
    }
}
