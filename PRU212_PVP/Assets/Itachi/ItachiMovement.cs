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
    //        StartPunchCooldown();
    //    }
    //}

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

    //public override void ThrowDart()
    //{
    //    if (CanThrowDart())
    //    {
    //        GetComponent<Animator>().SetTrigger("throwDart");
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
