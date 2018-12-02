using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbetsplatsScript : MonoBehaviour
{

    private UIScript GUIManager;
    [SerializeField] private GameObject arbetare;
    const float intervalToCheckAwesomeness = 10.0f;
    const float youMustBeThisAwesome = 0.8f; //0-1 inclusive
    const float intervalToCheckBadBehaviour = 10.0f;
    const float youMustBeThisBad = 0.8f; //0-1 inclusive

    public bool Aktiv
    {
        get { return arbetare.activeSelf; } //Kanske lägga på mer logik framöver
    }
    public bool HasAwesomeWorker
    {
        get { return arbetare.GetComponent<Arbetare>().seeminglyAwesome; }
    }

    private void Start()
    {
        GUIManager = GameObject.FindObjectOfType<UIScript>();
        //AddWorker();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (IsPlayer(col))
        {
            if (Aktiv)
            {
                int indexOfQA = Random.Range(0,
                    (listOfQuestions.Count <= listOfAnswers.Count ? listOfQuestions.Count - 1 : listOfAnswers.Count - 1));  //Om de är olika längd så utgår vi ifrån kortaste av de 2 så att vi har en rad i båda listorna
                int indexOfStoopidA = Random.Range(0, listOfErik.Count);
                GUIManager.ShowDialogue(listOfQuestions[indexOfQA],
                                        HasAwesomeWorker ? listOfAnswers[indexOfQA] : indexOfStoopidA < listOfErik.Count - 1 ? listOfErik[indexOfStoopidA] : Random.Range(1, 101).ToString());

            }
            else if (col.gameObject.GetComponent<PlayerMovement>().CarryingWorker)
            {
                AddWorker();
                col.gameObject.GetComponent<PlayerMovement>().DropOffWorker();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        //if (IsPlayer(col))
        //    RemoveWorker();
    }

    private bool IsPlayer(Collider2D col)
    {
        return (col.tag == "Player");
    }

    public void AddWorker()
    {
        arbetare.SetActive(true);
        arbetare.GetComponent<Arbetare>().SetAwesome((Random.value > 0.5f)); //Hälften av gångerna är det en awesome person
        if (HasAwesomeWorker)
            TurnAwesome();
        else
            TurnBad();
    }

    public void RemoveWorker()
    {
        StahpInvoke();
        arbetare.SetActive(false);
    }

    private void CheckIfWorkerLostHisFlair()
    {
        if (Random.value < youMustBeThisAwesome)
        {
            StahpInvoke();
            arbetare.GetComponent<Arbetare>().SetAwesome(false);
            TurnBad();
        }
    }

    private void DoStuffBadWorkersDo()
    {
        if (Random.value > youMustBeThisBad)
        {
            arbetare.GetComponent<Arbetare>().DoBadStuff();
        }
    }

    private void TurnAwesome()
    {
        InvokeRepeating("CheckIfWorkerLostHisFlair", 0, intervalToCheckAwesomeness); //var X sekund kollar vi 
    }

    private void TurnBad()
    {
        InvokeRepeating("DoStuffBadWorkersDo", 0, intervalToCheckBadBehaviour);
    }

    private void StahpInvoke()
    {
        CancelInvoke("CheckIfWorkerLostHisFlair");
        CancelInvoke("DoStuffBadWorkersDo");
    }

    private List<string> listOfQuestions = new List<string> {
"How are you today?",
"Any problems here?",
"How are the kids?",
"Did you resolve that issue we had?",
"How do you like it here?",
"Could you help me with something?",
"What do you think about our company?",
"Any suggestions on how to improve our sales?",
"What did you have for lunch?",
"Are you satisfied with your collegues?",
"Do you feel that your tasks are challenging enough?",
"Do you know what time it is?",
"We have a meeting soon.",
"You are here early today.",
"Have you finished your report?",
"The company is doing bad right now and we might have to get rid of people.",
"I am collecting money for a gift to Steve's birthday.",};


    private List<string> listOfAnswers = new List<string> {
"Fine how are you?",
"Not at all!",
"Great!",
"Yes, it was tricky but we found a good solution.",
"This workplace is great!",
"Yes of course, let me just finish up here.",
"Very nice and feels good to do something important.",
"Acctually, I have a small list of suggestions here if you want to have a look?",
"Todays lunch at the cantina. I can recommend that.",
"Yes, they are very helpful and have plenty of experience.",
"I think there is a good balance.",
"It is time to work, haha!",
"Yes, I will be there in time.",
"I have a lot to do these days.",
"Almost done!",
"That sounds bad, maybe some extra work will help.",
"Here is something from me too.",};

    private List<string> listOfErik = new List<string> {
"Bananas!",
"I hate you.",
"...no.",
"&%?$@!",
"AAAAAaargh",
"Goff goff",
"error syntax",
"My collegues smell bad.",
"I can not help you right now.",
"Stay a while and listen.",
"The important thing is that we are happy.",
"What?",
"...",
"No?",
"Yes.",
"Sounds great.",
"Ok.",
"Where am I?",
"Who am I?",
"Too... many.... numbers...",
"LOL",
"Please, do not talk to me.",
"Do not watch my screen",
"I do not have to answer that!",
"Zzzzzz",
"Crush you enemies, see them driven before you, and hear the lamentations of their women.",
"Why?",
"help",};

}
