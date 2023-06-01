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
        

        //private GameObject[] choices; //background pilihan
        //private TextMeshProUGUI[] choicesText; //text pilihan

        private Story currentStory;
        
        public bool dialogueIsPlaying {get; private set;} //mengecek dialog sedang berjalan atau tidak
        private bool dialogueIsWriting; //mengecek dialog sedang berjalan atau tidak
        private bool dialogueIsInChoice;  //mengecek apakah dialog sedang dalam posisi memilih atau choice

        private const string SPEAKER_TAG = "speaker";
        private const string PORTRAIT_TAG = "portrait";
        private const string LAYOUT_TAG = "layout";


        private void Start()
        {
            dialogueIsPlaying = false;
            dialogueIsWriting = false;
            dialogueIsInChoice = false;
            //dialoguePanel.SetActive(false);
            dialogueCanvas.SetActive(false);

            // choicesText = new TextMeshProUGUI[choices.Length];
            // int index = 0;
            // foreach (GameObject choice in choices)
            // {
            //     choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            //     index++;
            // } 

            UIManager.Instance.AddGameObjectToDictionary(this.gameObject);

        }

        // void Update() //awalnya update saja
        // {
        //     if(!dialogueIsPlaying) return;
        // }


        public void EnterDialogue(TextAsset inkJSON)
        {
            if(dialogueIsPlaying)
            {
                return;
            }
            GameManager.Instance.PauseGame(true);

            InputManager.Instance.IsPlayerAllowedToMove(false);

            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            //dialoguePanel.SetActive(true);
            dialogueCanvas.SetActive(true);

            //reset tagValue in protreit etc
            displayNameText.text = "???";
            //portraitAnimator.Play("default");
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
            GameManager.Instance.PauseGame(false);

            yield return new WaitForSecondsRealtime(0.2f); //agar apabila tombol input sudah digunakan, tidak akan bertabrakan  //misal: tombol jump dan tombol next sama

            dialogueIsPlaying = false;
            //dialoguePanel.SetActive(false);
            dialogueCanvas.SetActive(false);
            dialogueText.text = "";

            InputManager.Instance.IsPlayerAllowedToMove(true);
        }

        public void ContinueStory()
        {
            //for skipping to display full dialogue
            if(dialogueIsWriting)
            {
                dialogueIsWriting = false;
                return;
            }

            //for waiting player choice in choice text
            if(dialogueIsInChoice)
            {
                return;
            }


            //agar bisa skip dengan rapi
            dialogueIsWriting = false;

            // Below line is not working becoz coroutine always make new instance
            // another workaround is storing coroutine reference then 
            // stop it using the reference
            //StopCoroutine(WriteText());// 

            StopAllCoroutines(); //menghentikan method WriteText

            dialogueText.text = ""; //membersihkan text

            if(currentStory.canContinue)
            {
                StartCoroutine(WriteText());
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

        private IEnumerator SelectFirstChoice(GameObject firstChoiceButtonPrefab) //not used
        {
            // Event System requires we clear it first, then wait
            // for at least one frame before we set the current selected object
            EventSystem.current.SetSelectedGameObject(null);
            //yield return new WaitForEndOfFrame();
            yield return new WaitForSecondsRealtime(0.1f);
            EventSystem.current.SetSelectedGameObject(firstChoiceButtonPrefab);
        }


        private void SetChoiceButton(List<Choice> currentChoices)
        {
            int index = 0;
            foreach (Choice choice in currentChoices)
            {
                //used instead index coz for some reason using index return reference instead value
                int tempInt = index;

                var _button = Instantiate(choiceButtonPrefab, choiceGridLayout.transform);  
                //choiceButtonPrefab.GetComponent<ChoiceButtonScript>().SetThisButtonChoiceIndex(index);

                _button.transform.Find("Choice").transform.Find("ChoiceText").GetComponent<TextMeshProUGUI>().text = choice.text;
            
                _button.GetComponent<ButtonScript>().onClick.AddListener(() => MakeChoice(tempInt));

                //choices[index].gameObject.SetActive(true);
                //choicesText[index].text = choice.text;


                //Instantiate(choiceButtonPrefab, choiceGridLayout.transform);

                if(tempInt == 0) StartCoroutine(SelectFirstChoice(_button));

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
                dialogueIsInChoice = true;

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
            // else
            // {
            //     ClearChoiceButton();

            //     continueButton.SetActive(true); //button continue diaktifkan
            // }
        }

        public void MakeChoice(int choiceIndex)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);

            dialogueIsInChoice = false;

            ContinueStory();

            ClearChoiceButton();

            continueButton.SetActive(true); //button continue diaktifkan
        }

        
        private IEnumerator WriteText() //overall mengetik teks satu2
        {
            string messageToDisplay = currentStory.Continue().ToString(); //memilih message saat ini

            dialogueIsWriting = true;

            for(int i = 0; i < messageToDisplay.Length; i++)
            {
                if(!dialogueIsWriting) //mengecek apakah dialog sedang ditulis
                {
                    DisplayFullText(messageToDisplay);
                    break;
                }
                else
                {
                    dialogueText.text += messageToDisplay[i]; 
                    //nextMessageSound.Play();
                    yield return new WaitForSecondsRealtime(dialogueSpeed);
                }
            }

            DisplayChoices();   
        }

        private void DisplayFullText(string messageToDisplay)
        {
            dialogueText.text = ""; //membersihkan text
            dialogueText.text = messageToDisplay;
        }
    }
}
