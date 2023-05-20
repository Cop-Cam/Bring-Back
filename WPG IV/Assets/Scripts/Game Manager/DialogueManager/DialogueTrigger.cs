using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime; //using ink

namespace DialogueSystem
{
    public class DialogueTrigger : InteractableObjects
    {
        [SerializeField] private TextAsset inkJSON; //asset cerita
        
        public override void OnInteracted()
        {
            if(DialogueManager.Instance.dialogueIsPlaying)
            {
                DialogueManager.Instance.ContinueStory();
                return;
            }

            DialogueManager.Instance.EnterDialogue(inkJSON);
        }
        
        /*
        // Update is called once per frame
        private void Update()
        {
            if(playerInRange)
            {
                visualCue.SetActive(true);
                
                if(Input.GetKeyDown(KeyCode.E))
                {
                    gameObject.GetComponent<Collider2D>().enabled = false;

                    DialogueManager.Instance.EnterDialogue(this.inkJSON);
                }
            }
            else
            {
                visualCue.SetActive(false);
                
            }
        }
        */

        /*
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Player") //&& gameObject.tag != "Npc") //untuk dialog npc unskipable
            {
                //playerInRange = true;
                Debug.Log("gas");
                DialogueManager.Instance.EnterDialogue(inkJSON);
                gameObject.GetComponent<Collider2D>().enabled = false;

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                playerInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                playerInRange = false;
            }
        }
        */

        
    }
}
