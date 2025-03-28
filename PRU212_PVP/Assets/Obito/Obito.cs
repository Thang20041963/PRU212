using UnityEngine;

public class Obito : CharacterController
{
    public Transform specialPoint;
    public GameObject[] kamuis;

    public void Start()
    {
        AddKamuis();
    }

    public override void SpecialAttack1()
    {
       if (CanSpecial1())
        {
            {
                UseChakra(sp1Charka);
                GetComponent<Animator>().SetTrigger("special1");
                kamuis[FindKamui()].transform.position = specialPoint.position;
                kamuis[FindKamui()].GetComponent<ObitoSpecial1>().SetOwner(this.tag);
                kamuis[FindKamui()].GetComponent<ObitoSpecial1>().SetDirection(Mathf.Sign(transform.localScale.x));
                StartSpecial1Cooldown();
            }
        }
    }

    public void AddKamuis()
    {
        string KamuisName = (this.tag == "Player1") ? "Special1Holder_P1" : "Special1Holder_P2";
        // Find the DartHolder object
        GameObject kamuistHolder = GameObject.Find(KamuisName);

        if (kamuistHolder != null)
        {
            // Get all darts that are children of DartHolder
            kamuis = new GameObject[kamuistHolder.transform.childCount];
            for (int i = 0; i < kamuistHolder.transform.childCount; i++)
            {
                kamuis[i] = kamuistHolder.transform.GetChild(i).gameObject;
            }
        }
        else
        {
            Debug.LogError("Special1Holder not found! Make sure it's in the scene.");
        }
    }


    private int FindKamui()
    {
        for (int i = 0; i < kamuis.Length; i++)
        {
            if (!kamuis[i].gameObject.activeInHierarchy)
                return i;
        }
        return 0;
    }

    public override void SpecialAttack2()
    {
        if (CanSpecial2())
        {
            GetComponent<Animator>().SetTrigger("special2");
            UseChakra(sp2Charka);
            Collider2D hit = Physics2D.OverlapCircle(specialPoint.position, 1f, LayerMask.GetMask("Character"));

            if (hit != null)
            {
                CharacterController target = hit.GetComponent<CharacterController>();

                if (target != null && target != this && target.GetComponent<Animator>().GetBool("block") == false)
                {
                    target.TakeDamage(atk);
                    Debug.Log($"special2 hit: {hit.name}") ;
                }
            }

            StartSpecial2Cooldown();
        }
    }

    //public override void ThrowDart()
    //{
    //    if (CanThrowDart())
    //    {
    //        GetComponent<Animator>().SetTrigger("dart");
    //        darts[FindDart()].transform.position = dartPoint.position;
    //        darts[FindDart()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    //        StartDartCooldown();
    //    }
    //}
    //private int FindDart()
    //{
    //    for (int i = 0; i < darts.Length; i++)
    //    {
    //        if (!darts[i].gameObject.activeInHierarchy)
    //            return i;
    //    }
    //    return 0;
    //}
}
