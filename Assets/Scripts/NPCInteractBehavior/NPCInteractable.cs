using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteractable : MonoBehaviour
{
    public Text dialogueText;
    public Text nameText;
    public GameObject NPCTextSystem;
    public string NpcName;

    public bool NPCIsInteracting = false;
    public GameObject player;
    
    public GameObject gateVFX;
    public GameObject nuwaVFX;
    GameObject gate;
    bool isDestroyed = false;

    public List<string> dialogues = new List<string>()
        {
            "Hi I'm Tim.",
            "Welcome to this game world.",
            "Please go to the memorial building to start the game!" 
        };
     int index = 0;

     // Start is called before the first frame update
    void Start()
    {   
        gate = GameObject.FindGameObjectWithTag("Gate");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && NPCIsInteracting){
            Interact();
        }
    }

     public void Interact(){
        player.GetComponent<PlayerInteract>().isInteracting = true;
        player.GetComponent<PlayerInteract>().playerInteractButton.SetActive(false);
        //player.GetComponent<CharacterController>().enabled = false;
        if(index < dialogues.Count){
            NewDialogue(dialogues[index]);
            index += 1;
        } else {
            NPCIsInteracting = false;
            NPCTextSystem.SetActive(false);
            index = 0;
            player.GetComponent<PlayerInteract>().isInteracting = false;
            player.GetComponent<PlayerInteract>().playerInteractButton.SetActive(true);
            //player.GetComponent<CharacterController>().enabled = true;
            if(!isDestroyed){
                if(gate != null)
                {
                    Destroy(gate, 0.5f);
                    Instantiate(gateVFX, gate.transform.position, gate.transform.rotation);
                }
                isDestroyed = true;
            }
            Destroy(gameObject);
            Instantiate(nuwaVFX, transform.position, transform.rotation);
        }
    }
    
    void NewDialogue(string next){
        //Debug.Log("Start Dialogue");
        dialogueText.text = next;
        nameText.text = NpcName;
        NPCTextSystem.SetActive(true);
        
    }
}
