using UnityEngine;

public class Kakashi : CharacterController
{
    public GameObject special1s;
    public GameObject special2s;
    public Transform special1Point;
    public Transform special2Point;

    private void Start()
    {
        AddSpecial1();
        AddSpecial2();
    }

    public override void SpecialAttack1()
    {
        if (CanSpecial1())
        {
            UseChakra(sp1Charka);
            GetComponent<Animator>().SetTrigger("special1");
            special1s.transform.position = special1Point.position;
            special1s.GetComponent<Special1>().SetOwner(this.tag);
            special1s.GetComponent<Special1>().SetDirection(Mathf.Sign(transform.localScale.x));
            StartSpecial1Cooldown();
        }
    }

    public override void SpecialAttack2()
    {
        if (CanSpecial2())
        {
            special2s.SetActive(true);
            special2s.GetComponent<Special2>().SetOwner(this.tag);
            special2s.GetComponent<Special2>().SetDamage(atk);
            Invoke(nameof(Special2Att), 1f);
        }
    }

    private void Special2Att()
    {

        special2s.GetComponent<Special2>().ActivateSpecial2(Mathf.Sign(transform.localScale.x));
        setWaitState(false);
        Invoke(nameof(Disable), 1.5f);
    }
    private void Disable()
    {
        special2s.gameObject.SetActive(false);

    }

    //private int FindSpecial1()
    //{
    //    for (int i = 0; i < special1s.Length; i++)
    //    {
    //        if (!special1s[i].gameObject.activeInHierarchy)
    //            return i;
    //    }
    //    return 0;
    //}

    //private int FindSpecial2()
    //{
    //    for (int i = 0; i < special2s.Length; i++)
    //    {
    //        if (!special2s[i].gameObject.activeInHierarchy)
    //            return i;
    //    }
    //    return 0;
    //}

    public void AddSpecial1()
    {
        string Special1Name = (this.tag == "Player1") ? "Special1_P1" : "Special1_P2";
        GameObject special1Holder = GameObject.Find(Special1Name) ;

        if (special1Holder != null)
        {
            // Get all darts that are children of DartHolder
            special1s = special1Holder;
            special1Holder.SetActive(false);
        }
        else
        {
            Debug.LogError("Special1 holder not found! Make sure it's in the scene.");
        }
    }

    public void AddSpecial2()
    {
        string Special2Name = (this.tag == "Player1") ? "Special2_P1" : "Special2_P2";
        GameObject special2Holder = GameObject.Find(Special2Name);

        if (special2Holder != null)
        {
            
            // Get all darts that are children of DartHolder
            special2s = special2Holder;
            special2Holder.SetActive(false);
        }
        else
        {
            Debug.LogError("Special2 holder not found! Make sure it's in the scene.");
        }
    }

}
