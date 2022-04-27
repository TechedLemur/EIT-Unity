using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;

public class StateMachine : MonoBehaviour
{

    public GameObject bordPlate;
    public GameObject ben1;
    public GameObject ben2;
    public GameObject ben3;
    public GameObject ben4;
    public GameObject skrue1;
    public GameObject skrue2;
    public GameObject skrue3;
    public GameObject skrue4;
    public GameObject nextButton;
    public GameObject prevButton;


    public Animator tableAnimator;
    public Animator benAnimator;
    public Animator skruAnimator;
    public GameObject bord;
    public GameObject bigSkru;
    public GameObject parent;
    public GameObject menu;

    private List<GameObject> ben;
    private List<GameObject> skruer;

    private int state = 0;


    // Start is called before the first frame update
    void Start()
    {
        skruer = new List<GameObject>() { skrue1, skrue2, skrue3, skrue4 };
        ben = new List<GameObject>() { ben1, ben2, ben3, ben4 };

        this.State0();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MeshVisible()
    {
        var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();
        observer.DisplayOption = SpatialAwarenessMeshDisplayOptions.Visible;
    }
    public void MeshOcclusion()
    {
        var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();
        observer.DisplayOption = SpatialAwarenessMeshDisplayOptions.Occlusion;
    }
    public void MeshNone()
    {
        var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();
        observer.DisplayOption = SpatialAwarenessMeshDisplayOptions.None;
    }


    private void State0()
    {
        parent.SetActive(true);
        bordPlate.SetActive(true);
        foreach (var b in this.ben)
        {
            b.SetActive(false);
        }
        foreach (var s in this.skruer)
        {
            s.SetActive(false);
        }
        this.resetTablePosition();

        prevButton.SetActive(false);
        nextButton.SetActive(true);
        bigSkru.SetActive(false);

        tableAnimator.enabled = false;
    }

    private void State1()
    {
        foreach (var b in this.ben)
        {
            b.SetActive(false);
        }
        foreach (var s in this.skruer)
        {
            s.SetActive(true);
        }
        prevButton.SetActive(true);
        bigSkru.SetActive(true);
    }

    private void State2()
    {
        foreach (var b in this.ben)
        {
            b.SetActive(true);
        }
        foreach (var s in this.skruer)
        {
            s.SetActive(true);
        }
        nextButton.SetActive(true);
        tableAnimator.enabled = false;
        this.resetTablePosition();
        bigSkru.SetActive(false);
        benAnimator.enabled = true;

    }

    private void State3()
    {

        benAnimator.enabled = false;
        ben1.transform.localPosition = new Vector3(0, -4, 0);
        ben1.transform.localRotation = Quaternion.Euler(0, 0, 0);
        tableAnimator.enabled = true;
    }

    private void State4()
    {


        tableAnimator.enabled = false;
        bord.transform.localPosition = new Vector3(0, (float)3.5, (float)-5);
        bord.transform.localRotation = Quaternion.Euler(0, 0, 0);
        nextButton.SetActive(false);
    }

    private void UpdateState()
    {
        switch (this.state)
        {
            case 0:
                this.State0();
                break;
            case 1:
                this.State1();
                break;
            case 2:
                this.State2();
                break;
            case 3:
                this.State3();
                break;
            case 4:
                this.State4();
                break;
            default:
                break;
        }
    }


    public void NextState()
    {
        this.state += 1;



        this.UpdateState();



    }
    public void PreviousState()
    {
        this.state -= 1;



        this.UpdateState();

    }

    private void resetTablePosition()
    {
        bord.transform.localPosition = new Vector3(0, 0, 0);
        bord.transform.localRotation = Quaternion.Euler(180, 0, 0);
    }

    public void ToggleAnimations()
    {
        if (tableAnimator.enabled)
        {
            tableAnimator.enabled = false;
            this.resetTablePosition();
        }
        else if (this.state == 3)
        {
            tableAnimator.enabled = true;
        }

        if (benAnimator.enabled)
        {
            benAnimator.enabled = false;
            ben1.transform.localPosition = new Vector3(0, -4, 0);
            ben1.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        else if (this.state == 2)
        {
            benAnimator.enabled = true;
        }

        if (skruAnimator.enabled)
        {
            skruAnimator.enabled = false;
            skrue4.transform.localPosition = new Vector3((float)-5.193, (float)-0.665, (float)5.196);
        }

        else if (this.state == 1)
        {
            skruAnimator.enabled = true;
        }


    }

    public void ResetPosition()
    {
        parent.transform.localPosition = Camera.main.transform.position + 3 * Vector3.Scale((menu.transform.position - Camera.main.transform.position), new Vector3(1, 0, 1));
        parent.transform.localRotation = Quaternion.Euler(0, 0, 0);
        this.state = 0;
        this.UpdateState();
    }
}
