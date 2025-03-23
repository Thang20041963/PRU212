using UnityEngine;

public class ItachiMovement : CharacterController
{
    public GameObject special1s;
    public GameObject special2s;
    public Transform special1Point;
    public Transform special2Point;


    void Start()
    {
        AddSpecialSkill1();
        AddSpecialSkill2();
    }


    public override void SpecialAttack1()
    {
        if (CanSpecial1())
        {
            UseChakra(sp1Charka);
            GetComponent<Animator>().SetTrigger("special1");
            special1s.transform.position = special1Point.position;
            special1s.GetComponent<ItachiS1Script>().SetOwner(this.tag);
            special1s.GetComponent<ItachiS1Script>().SetDirection(Mathf.Sign(transform.localScale.x));
            StartSpecial1Cooldown();
        }
    }

    public override void SpecialAttack2()
    {
        if (CanSpecial2())
        {
            UseChakra(sp2Charka);
            setWaitState(true);
            Invoke(nameof(CallSpecial2), 0f);
        }
    }

    private void CallSpecial2()
    {
        if (special2s != null)
        {
            special2s.SetActive(true);
            special2s.GetComponent<ItachiS2Script>().SetOwner(this.tag);
            special2s.GetComponent<ItachiS2Script>().SetDamage(atk);
            Invoke(nameof(Special2Att), 1f);
        }
    }
    private void Special2Att()
    {

        special2s.GetComponent<ItachiS2Script>().ActivateSpecial2(Mathf.Sign(transform.localScale.x));
        setWaitState(false);
        Invoke(nameof(Disable), 1.5f);
    }
    private void Disable()
    {
        special2s.gameObject.SetActive(false);
    }

    public void AddSpecialSkill1()
    {
        GameObject sp1holder = GameObject.Find("ItachiS1(Clone)");

        if (sp1holder != null)
        {
            special1s = sp1holder;
            sp1holder.SetActive(false);
        }
        else
        {
            Debug.LogError("S1 not found! Make sure it's in the scene.");
        }
    }

    public void AddSpecialSkill2()
    {
        GameObject sp2holder = GameObject.Find("ItachiS2(Clone)");

        if (sp2holder != null)
        {
            special2s = sp2holder;
            sp2holder.SetActive(false);
        }
        else
        {
            Debug.LogError("S2 not found! Make sure it's in the scene.");
        }
    }
}
