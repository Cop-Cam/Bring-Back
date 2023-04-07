using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime; //using ink
using UnityEngine.EventSystems;

namespace DialogueSystem
{
    public class DialogueManager : GenericSingletonClass<DialogueManager>
    {
        [Header("Dialogue UI")]
        //[SerializeField] private GameObject dialoguePanel; //panel dialog //untuk mematikan dialog
        [SerializeField] private GameObject dialogueCanvas; //canvas dialog //untuk mematikan dialog
        [SerializeField] private TextMeshProUGUI dialogueText; //text dialog
        [SerializeField] private TextMeshProUGUI displayNameText; //nama karakter 
        [SerializeField] private Animator portraitAnimator; //animator sprite karakter
        [SerializeField] private float dialogueSpeed; //kecepatan dialog
        [SerializeField] private GameObject continueButton; //continue button

        [Header("Choices UI")]
        [SerializeField] private GameObject choiceButtonPrefab; //prefab button
        [SerializeField] private GameObject choiceGridLayout; //prefab button
        

        private GameObject[] choices; //background pilihan
        private TextMeshProUGUI[] choicesText; //text pilihan

        private Story currentStory;
        
        private bool dialogueIsPlaying; //mengecek dialog sedang berjalan atau tidak
        private bool dialogueIsWriting; //mengecek dialog sedang berjalan atau tidak

        private const string SPEAKER_TAG = "speaker";
        private const string PORTRAIT_TAG = "portrait";
        private const string LAYOUT_TAG = "layout";


        private void Start()
        {
            dialogueIsPlaying = false;
            dialogueIsWriting = false;
            //dialoguePanel.SetActive(false);
            dialogueCanvas.SetActive(false);

            // choicesText = new TextMeshProUGUI[choices.Length];
            // int index = 0;
            // foreach (GameObject choice in choices)
            // {
            //     choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            //     index++;
            // } 

            UIManager.Instance.AddGameObjectToDictionary(transform.parent.gameObject);

        }

        // void Update() //awalnya update saja
        // {
        //     if(!dialogueIsPlaying) return;
        // }


        public void EnterDialogue(TextAsset inkJSON)
        {
            InputManager.Instance.IsPlayerAllowedToDoPlayerMapsInput(false);

            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            //dialoguePanel.SetActive(true);
            dialogueCanvas.SetActive(true);

            //reset tagValue in protreit etc
            displayNameText.text = "???";
            portraitAnimator.Play("default");
            //layoutAnimator.Play("default");

            //mengecek apakah inkJSON file berisi
            if(currentStory.canContinue)
            {
                ContinueStory();
            }
            else
            {
                StartCoroutine(ExitDialogue());
                Debug.LogWarning("File inkJSON kosong! Apakah file sudah diisi?");
            }
        }

        private IEnumerator ExitDialogue()
        {
            yield return new WaitForSeconds(0.2f); //agar apabila tombol input sudah digunakan, tidak akan bertabrakan  //misal: tombol jump dan tombol next sama

            dialogueIsPlaying = false;
            //dialoguePanel.SetActive(false);
            dialogueCanvas.SetActive(false);
            dialogueText.text = "";

            InputManager.Instance.IsPlayerAllowedToDoPlayerMapsInput(true);
        }

        public void ContinueStory()
        {
            //agar bisa skip dengan rapi
            dialogueIsWriting = false;
            //StopAllCoroutines(); //menghentikan method WriteText

            StopCoroutine(WriteText()); //menghentikan method WriteText

            dialogueText.text = ""; //membersihkan text

            if(currentStory.canContinue)
            {
                StartCoroutine(WriteText());
                DisplayChoices();   
                HandleTags(currentStory.currentTags);
            }
            else
            {
                StartCoroutine(ExitDialogue());
            }
        }

        private void HandleTags(List<string> currentTags)
        {
            //string[] splitTag;
            foreach(string tag in currentTags)
            {
                string[] splitTag = tag.Split(':');
                if(splitTag.Length != 2)
                {
                    Debug.LogError("Tag could not be appropriately parsed: " + tag);
                }
                string tagKey = splitTag[0].Trim();
                string tagValue = splitTag[1].Trim();
                
                switch (tagKey)
                {
                    case SPEAKER_TAG:
                        displayNameText.text = tagValue;
                        break;
                    
                    case PORTRAIT_TAG:
                        portraitAnimator.Play(tagValue);
                        break;

                    case LAYOUT_TAG:
                        Debug.Log("Layout=" + tagValue);
                        break;

                    default:
                        Debug.Log("tagKey tidak mengandung tag tersebut!\n");
                        break;
                }
            }
        }

        private void SetChoiceButton(List<Choice> currentChoices)
        {
            int index = 0;
            foreach (Choice choice in currentChoices)
            {
                //choiceButtonPrefab.GetComponent<ChoiceButtonScript>().SetThisButtonChoiceIndex(index);
                choiceButtonPrefab.GetComponent<ButtonScript>().onClick.AddListener(() => MakeChoice(index));
                choiceButtonPrefab.transform.Find("Choice").transform.Find("ChoiceText").GetComponent<TextMeshProUGUI>().text = choice.text;

                //choices[index].gameObject.SetActive(true);
                //choicesText[index].text = choice.text;
                index++;
            } 
        }

        private void ClearChoiceButton()
        {
            foreach(Transform ChoiceButtonPrefab in choiceGridLayout.transform)
            {
                GameObject.Destroy(ChoiceButtonPrefab.gameObject);
            }
        }

        private void DisplayChoices()
        {
            if(currentStory.currentChoices.Count > 0)
            {
                continueButton.SetActive(false); //button continue dihilangkan sementara untuk pemaksaan memilih choice

                List<Choice> currentChoices = currentStory.currentChoices;

                // if(currentChoices.Count > choices.Length)
                // {
                //     Debug.LogError("Choices lebih banyak dari UI yang tersedia: " + currentChoices.Count);
                // } 
                SetChoiceButton(currentChoices);
                // int index = 0;
                // foreach (Choice choice in currentChoices)
                // {
                //     choices[index].gameObject.SetActive(true);
                //     choicesText[index].text = choice.text;
                //     index++;
                // } 

                //meghilangkan choice yang kelebihan
                // for(int i=index; i<choices.Length; i++)
                // {
                //     choices[i].gameObject.SetActive(false);
                // }

                //StartCoroutine(SelectFirstChoice());
            }
            else
            {
                // for(int index = choices.Length-1; index>0; index--)
                // {
                //     choices[index].gameObject.SetActive(false);
                //     choicesText[index].text = "";
                // } 
                ClearChoiceButton();

                continueButton.SetActive(true); //button continue diaktifkan
            }
        }


        // private IEnumerator SelectFirstChoice() //not used
        // {
        //     EventSystem.current.SetSelectedGameObject(null);
        //     yield return new WaitForEndOfFrame();
        //     EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
        // }

        public void MakeChoice(int choiceIndex)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);

            ContinueStory();
        }

        
        private IEnumerator WriteText() //overall mengetik teks satu2
        {
            string messageToDisplay = currentStory.Continue().ToString(); //memilih message saat ini

            dialogueIsWriting = true;

            for(int i = 0; i < messageToDisplay.Length; i++)
            {
                if(!dialogueIsWriting) //mengecek apakah dialog sedang ditulis
                {
                    break;
                }
                else
                {
                    dialogueText.text += messageToDisplay[i]; 
                    //nextMessageSound.Play();
                    yield return new WaitForSeconds(dialogueSpeed);
                }
            }
        }
    }
}
