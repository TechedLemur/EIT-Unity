using System.Collections;
using System.Collections.Generic;
using UnityEngine;





class State
{
    // Objects that are visible in this state
    List<GameObject> objects;
    // Potential animators in this state
    List<Animator> animators;

    State(List<GameObject> objects, List<Animator> animators)
    {
        this.objects = objects;
        this.animators = animators;
    }

    void Enter()
    {
        foreach (var o in objects)
        {
            o.SetActive(true);
        }
        foreach (var a in animators)
        {
            a.SetActive(true);
            a.enabled = true;
        }
    }

    void Exit()
    {
        foreach (var o in objects)
        {
            o.SetActive(false);
        }
        foreach (var a in animators)
        {
            // Disable and set animated object to initial position
            a.enabled = false;
            a.transform.position = a.rootPosition; // ? works ?
            a.transform.rotation = a.rootRotation; // ? works ?
            a.SetActive(false);
        }
    }

    void ToggleAnimations()
    {
        foreach (var a in animators)
        {
            a.enabled = !a.enabled;
        }
    }
}






public class StateMachine : MonoBehaviour
{
    public GameObject parent;
    public GameObject menu;

    // Buttons
    public GameObject nextButton;
    public GameObject prevButton;

    // Individual Parts
    public GameObject tableTop;
    public GameObject leg1;
    public GameObject leg2;
    public GameObject leg3;
    public GameObject leg4;
    public GameObject screw1;
    public GameObject screw2;
    public GameObject screw3;
    public GameObject screw4;
    // The Entire Table
    public GameObject table;

    // Large Screw
    public GameObject screwHologram;

    // Animation controllers
    public Animator screwAnimator;
    public Animator legAnimator;
    public Animator tableAnimator;

    // State Handling
    private List<State> states;
    private int state; // Index of states list



    // Start is called before the first frame update
    void Start()
    {
        states = new List<State>(){
            new State( // 0 - Place Start Position (tabletop)
                new List<GameObject>() { tableTop },
                new List<Animator>() { }
            ),
            new State( // 1- Insert Screws
                new List<GameObject>() { tableTop, screw1, screw2, screw3, screw4, screwHologram },
                new List<Animator>() { screwAnimator }
            ),
            new State( // 2 - Attach Legs
                new List<GameObject>() { tableTop, screw2, leg1, leg2, leg3, leg4 },
                new List<Animator>() { legAnimator }
            ),
            new State( // 3 - Flip Table
                new List<GameObject>() { table },
                new List<Animator>() { tableAnimator }
            ),
            new State( // 4 - Place Table
                new List<GameObject>() { table },
                new List<Animator>() { }
            )
        };

        ChangeToState(0);
    }


    // Update is called once per frame
    void Update()
    {

    }

    void ChangeToState(int newState)
    {
        if (newState < 0 || states.Count <= newState || newState == state) return;

        states[state].Exit();
        state = newState;
        states[state].Enter();
    }

    public void NextState()
    {
        ChangeToState(state + 1);
    }

    public void PreviousState()
    {
        ChangeToState(state - 1);
    }

    public void ToggleAnimations()
    {
        states[state].ToggleAnimations();
    }

    // Reset entire guide, go back to placing the table
    public void ResetPosition()
    {
        parent.transform.localPosition = Camera.main.transform.position + 3 * Vector3.Scale((menu.transform.position - Camera.main.transform.position), new Vector3(1, 0, 1));
        ChangeToState(0);
    }
}