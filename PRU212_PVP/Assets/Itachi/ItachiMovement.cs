using UnityEngine;

public class ItachiMovement : CharacterController
{

    //public Transform dartPoint;
    //public Transform attackPoint;
    //public float punchRange = 0.3f;
    //public float kickRange = 0.5f;
    // public GameObject[] darts;

    public GameObject[] special1s;
    public GameObject[] special2s;
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
            GetComponent<Animator>().SetTrigger("special1");
            special1s[FindSpecial1()].transform.position = special1Point.position;
            special1s[FindSpecial1()].GetComponent<Special1>().SetDirection(Mathf.Sign(transform.localScale.x));
            StartSpecial1Cooldown();
        }
    }

    public override void SpecialAttack2()
    {
        if (CanSpecial2())
        {
            special2s[FindSpecial2()].transform.position = special2Point.position;
            special2s[FindSpecial2()].GetComponent<Special2>().SetDirection(Mathf.Sign(transform.localScale.x));
            StartSpecial2Cooldown();
        }
    }

    private int FindSpecial1()
    {
        for (int i = 0; i < special1s.Length; i++)
        {
            if (!special1s[i].gameObject.activeInHierarchy)
                return i;
        }
        return 0;
    }

    private int FindSpecial2()
    {
        for (int i = 0; i < special2s.Length; i++)
        {
            if (!special2s[i].gameObject.activeInHierarchy)
                return i;
        }
        return 0;
    }
    public void AddSpecialSkill1()
    {
        // Find the DartHolder object
        GameObject sp1holder = GameObject.Find("ItachiS1(Clone)");

        if (sp1holder != null)
        {
            Debug.LogError("spq1");
            special1s = new GameObject[sp1holder.transform.childCount];
            for (int i = 0; i < sp1holder.transform.childCount; i++)
            {
                special1s[i] = sp1holder.transform.GetChild(i).gameObject;
            }
        }
        else
        {
            Debug.LogError("DartHolder not found! Make sure it's in the scene.");
        }
    }

    public void AddSpecialSkill2()
    {
        // Find the DartHolder object
        GameObject sp2holder = GameObject.Find("ItachiS2(Clone)");

        if (sp2holder != null)
        {
            Debug.LogError("sp2ww");
            special2s = new GameObject[sp2holder.transform.childCount];
            for (int i = 0; i < sp2holder.transform.childCount; i++)
            {
                special2s[i] = sp2holder.transform.GetChild(i).gameObject;
            }
        }
        else
        {
            Debug.LogError("DartHolder not found! Make sure it's in the scene.");
        }
    }

    
}
