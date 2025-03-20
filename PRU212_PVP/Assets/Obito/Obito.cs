using UnityEngine;

public class Obito : CharacterController
{
    public Transform specialPoint;
    public GameObject[] kamuis;

    //public override void KickAttack()
    //{
    //    if (CanKick())
    //    {
    //        GetComponent<Animator>().SetTrigger("kick");
    //        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, kickRange);
    //        if (hit != null)
    //        {
    //            Debug.Log("Hit: " + hit.name);
    //            // You can add logic here to damage the other player or object
    //        }
    //        StartKickCooldown();
    //    }

    //}

    //public override void PunchAttack()
    //{
    //    if (CanPunch())
    //    {
    //        GetComponent<Animator>().SetTrigger("punch");
    //        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, punchRange);
    //        if (hit != null)
    //        {
    //            Debug.Log("Hit: " + hit.name);
    //            // You can add logic here to damage the other player or object
    //        }
    //        StartKickCooldown();
    //    }
    //}

    public override void SpecialAttack1()
    {
       if (CanSpecial1())
        {
            {
                GetComponent<Animator>().SetTrigger("special1");
                kamuis[FindKamui()].transform.position = specialPoint.position;
                kamuis[FindKamui()].GetComponent<Special1>().SetDirection(Mathf.Sign(transform.localScale.x));
                StartSpecial1Cooldown();
            }
        }
    }

    public void AddKamuis()
    {
        // Find the DartHolder object
        GameObject dartHolder = GameObject.Find("Special1Holder");

        if (dartHolder != null)
        {
            // Get all darts that are children of DartHolder
            darts = new GameObject[dartHolder.transform.childCount];
            for (int i = 0; i < dartHolder.transform.childCount; i++)
            {
                darts[i] = dartHolder.transform.GetChild(i).gameObject;
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

            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, punchRange, LayerMask.GetMask("Character"));

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
