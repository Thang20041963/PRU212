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
            GetComponent<Animator>().SetTrigger("special1");
            special1s.transform.position = special1Point.position;
            special1s.GetComponent<Special1>().SetDirection(Mathf.Sign(transform.localScale.x));
            StartSpecial1Cooldown();
        }
    }

    public override void SpecialAttack2()
    {
        if (CanSpecial2())
        {
            special2s.transform.position = special2Point.position;
            special2s.GetComponent<Special2>().SetDirection(Mathf.Sign(transform.localScale.x));
            StartSpecial2Cooldown();
        }
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
        // Find the DartHolder object
        GameObject special1Holder = GameObject.Find("Special1(Clone)") ? GameObject.Find("Special1(Clone)") : GameObject.Find("Special1");

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
        // Find the DartHolder object
        GameObject special2Holder = GameObject.Find("Special2(Clone)") ? GameObject.Find("Special2(Clone)") : GameObject.Find("Special2");


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
