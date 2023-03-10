using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime; //using ink


public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private TextAsset inkJSON; //asset cerita
    [SerializeField] private GameObject visualCue; //tanda apakah pemain dekat pada npc

    [Header("Wall")]
    [SerializeField] private GameObject[] progressionWall; //berguna untuk menutup jalan hingga pemain selesai berdialog
    //public bool thereIsWall;
    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(progressionWall.Length > 0)
        {
            foreach(GameObject wall in progressionWall)
            {
                wall.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(playerInRange)
        {
            visualCue.SetActive(true);
            //if(InputManager.GetInstance().GetInteractPressed())
            if(Input.GetKeyDown(KeyCode.E))
            {
                gameObject.GetComponent<Collider2D>().enabled = false;

                DialogueManager.Instance.EnterDialogue(inkJSON);

                foreach(GameObject wall in progressionWall)
                {
                    wall.SetActive(false);
                }
            }
        }
        else
        {
            visualCue.SetActive(false);
            
        }
        
    }

    
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
}
