using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbetsplatsScript : MonoBehaviour {

    private UIScript GUIManager;
    private bool hasWorker = false;
    [SerializeField] private bool hasAwesomeWorker = false;
    [SerializeField] private GameObject arbetare;

    public bool Aktiv
    {
        get { return arbetare.activeSelf; } //Kanske lägga på mer logik framöver
    }
    public bool HasAwesomeWorker
    {
        get { return hasAwesomeWorker; }
    }

    private void Start()
    {
        GUIManager = GameObject.FindObjectOfType<UIScript>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (IsPlayer(col) && hasWorker)
        {
            //AddWorker();
            GUIManager.ShowDialogue(listOfQuestions[Random.Range(0, listOfQuestions.Count - 1)], 
                                    listOfAnswers[Random.Range(0, listOfAnswers.Count - 1)]);
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
        hasAwesomeWorker = (Random.value > 0.5f); //Hälften av gångerna är det en awesome person
    }

    public void RemoveWorker()
    {
        arbetare.SetActive(false);
    }


    public List<string> listOfQuestions = new List<string> { "What was the last thing you created?",
"Who is a better cook your mother or grandmother?",
"What book do you wish would be turned into a movie?",
"Is there an app that you hate but use anyways?",
"What was the last thing that you fixed?",
"Could you survive in the wilderness for a month ? ",
"What are some songs you know by heart ? ",
"What piece of technology brings you the most joy ? ",
"If you could rename the street you lived on, what would you rename it ? ",
"What do your clothes say about you ? ",
"What’s your “going to bed” routine ? ",
"What animal best represents your personality ? ",
"What’s the fullest you’ve ever been ? ",
"What behaviors make you think a person is creepy ? ",
"Who in the movie business seems the most down to earth ? ",
"What’s the funniest or most amazing cell phone cover you have seen ? ",
"Which musical artist is greatly overrated ? ",
"What company or franchise do you wish would go out of business ? ",
"What’s the perfect temperature to set the thermostat at ? ",
"Who around you has the worst luck ? ",
"What’s your favorite sounding word ? ",
"What’s the weirdest thing that has happened to you in a car ? ",
"If you could fit your whole life into one picture what would it look like ? ",
"What’s something that can’t be found or bought on the internet ? ",
"What should the first colony on another planet be called ? ",
"If you put out a magazine, what would you name it and what would be in it ? ",
"If you were challenged to a duel, what weapons would you choose ? ",
"What villain do you really feel for?",
"What is the most interesting thing you could do with 400 pounds of cheddar cheese ? ",
"What did you think you were good at but are actually quite bad at ? ",
"What in a trailer automatically makes you assume a movie will be horrible ? ",
"What game do you wish you could act out in real life ? ",
"What are you really happy about being terrible at ? ",
"What would be the funniest thing to fill a piñata with ?}", };


    public List<string> listOfAnswers = new List<string> { "We need to rent a room for our party.",
"My Mum tries to be cool by saying that she likes all the same things that I do.",
"He didn’t want to go to the dentist, yet he went anyway.",
"The body may perhaps compensates for the loss of a true metaphysics.",
"He told us a very exciting adventure story.",
"Malls are great places to shop; I can find everything I need under one roof.",
"She did not cheat on the test, for it was not the right thing to do.",
"If I don’t like something, I’ll stay away from it.",
"Everyone was busy, so I went to the movie alone.",
"The book is in front of the table.",
"Let me help you with your baggage.",
"Is it free?",
"She did her best to help him.",
"She advised him to come back at once.",
"I am happy to take your donation; any amount will be greatly appreciated.",
"The lake is a long way from here.",
"There was no ice cream in the freezer, nor did they have money to go to the store.",
"Sixty-Four comes asking for bread.",
"They got there early, and they got really good seats.",
"We have never been to Asia, nor have we visited Africa.",};
}
