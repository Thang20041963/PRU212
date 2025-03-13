using UnityEngine;

public class Kakashi : CharacterController
{
    public GameObject[] special1s;
    public GameObject[] special2s;
    public Transform special1Point;
    public Transform special2Point;

    private void Start()
    {
        //// Find the DartHolder object
        //GameObject dartHolder = GameObject.Find("DartHolder");

        //if (dartHolder != null)
        //{
        //    // Get all darts that are children of DartHolder
        //    darts = new GameObject[dartHolder.transform.childCount];
        //    for (int i = 0; i < dartHolder.transform.childCount; i++)
        //    {
        //        darts[i] = dartHolder.transform.GetChild(i).gameObject;
        //    }
        //}
        //else
        //{
        //    Debug.LogError("DartHolder not found! Make sure it's in the scene.");
        //}
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

}
