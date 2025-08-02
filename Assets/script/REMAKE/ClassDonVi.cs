using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClassDonVi : MonoBehaviour
{
    //kiểm tra đơn vị có đang được chọn để thực hiện các hành động khác.
    public bool isSelected = false;

    //chú thích: tên đơn vị/spear,sword,.../infantry,cavalry,archer,...
    public string NameArmy, TypeArmy, BranchArmy;

    //thông số 
    public int Atk, Def, Hp, Charge, Speed, NumberBlock, RangeAtk, Mass;

    // action turn/ turn
    public int MaxTurnSpeed, CurrentSpeed, MaxTurnAtk, CurrentAtk;

    // thời gian giữa mỗi lần di chuyển
    public float Time = 0.3f;

    //constructor
    private Coroutine CoroutineMove;

    public ClassDonVi(string nameArmy, string typeArmy, string branchArmy, int atk, int def, int hp,
        int charge, int speed, int maxTurnSpeed, int maxTurnAtk, int rangeAtk, int mass)
    {
        NameArmy = nameArmy;
        TypeArmy = typeArmy;
        BranchArmy = branchArmy;
        Atk = atk;
        Def = def;
        Hp = hp;
        Mass = mass;
        RangeAtk = rangeAtk;
        Charge = charge;
        Speed = speed;
        MaxTurnSpeed = maxTurnSpeed;
        CurrentSpeed = MaxTurnSpeed;
        MaxTurnAtk = maxTurnAtk;
        CurrentAtk = MaxTurnAtk;
    }

    public ClassDonVi()
    {
    }


    //Di chuyen
    public virtual void Move(Vector3 targetPosition)
    {
        if (CurrentSpeed > 0)
        {
            if (CoroutineMove != null)
                StopCoroutine(CoroutineMove);

            Vector3 StartPosition = new Vector3(
                Mathf.Round(transform.position.x / 100f) * 100f,
                Mathf.Round(transform.position.y / 100f) * 100f,
                transform.position.z
            );
            CoroutineMove = StartCoroutine(MoveRule(StartPosition, targetPosition));
            CurrentSpeed--;
        }
    }

    private IEnumerator MoveRule(Vector3 StartPosition, Vector3 target)
    {
        Vector3 currentPos = StartPosition;
        int buocX = Mathf.RoundToInt((target.x - StartPosition.x) / 100f);
        int buocY = Mathf.RoundToInt((target.y - StartPosition.y) / 100f);
        int stepX = (int)Mathf.Sign(buocX);
        int stepY = (int)Mathf.Sign(buocY);

        for (int i = 0; i < Mathf.Abs(buocX); i++)
        {
            currentPos += new Vector3(stepX * 100f, 0, 0);
            transform.position = currentPos;
            yield return new WaitForSeconds(Time);
            NumberBlock++;
        }

        for (int i = 0; i < Mathf.Abs(buocY); i++)
        {
            currentPos += new Vector3(0, stepY * 100f, 0);
            transform.position = currentPos;
            yield return new WaitForSeconds(Time);
            NumberBlock++;
        }

        if (NumberBlock < Speed)
        {
        }
    }

    public virtual void Attack(GameObject armyAttacked, GameObject armyAttacker)
    {
        ClassDonVi armyTakenDame = armyAttacked.GetComponent<ClassDonVi>();
        ClassDonVi armyDealerDame = armyAttacker.GetComponent<ClassDonVi>();
        int totalDame = Mathf.RoundToInt(Atk + ChargeDame());
        if (totalDame < 10) totalDame = 10;
        armyTakenDame.NhanSatThuong(totalDame);
        armyTakenDame.PhanDon(armyAttacker);
        if (CurrentAtk > 0)
        {
            CurrentAtk--;
        }
    }

    public virtual void NhanSatThuong(int satThuong)
    {
        int DameReduce = Mathf.RoundToInt(satThuong * DefenseRate());
        int totalDameDeal = satThuong - DameReduce;
        Hp -= totalDameDeal;
        if (totalDameDeal < 10) totalDameDeal = 10;
        if (Hp <= 0)
        {
            Destroy(gameObject, 2.5f);
        }
    }

    public float DefenseRate()
    {
        float DefenseRate = Def / (Def + 100);
        return DefenseRate;
    }

    public int ChargeDame()
    {
        int DameCharge = Mathf.RoundToInt(Charge + Mass) * NumberBlock;
        return DameCharge;
    }

    public void ResetLuot()
    {
        CurrentSpeed = 0;
        CurrentAtk = 0;
        NumberBlock = 0;
    }

    public void KhoiPhucLuot()
    {
        CurrentSpeed = MaxTurnSpeed;
        CurrentAtk = MaxTurnAtk;
    }

// Hàm tính sát thương đã có:
    // public virtual int TotalDame()
    // {
    //     int tongDame = NumberBlock * (Charge + Mass) + Atk;
    //     if (tongDame < 10) tongDame = 10;
    //
    //     int satThuongThuc = Mathf.RoundToInt(tongDame);
    //     return satThuongThuc;
    // }
    public virtual void PhanDon( GameObject armyGaySatThuong)
    {
        ClassDonVi armyDealerDame = armyGaySatThuong.GetComponent<ClassDonVi>();
        armyDealerDame.NhanSatThuong(DamePhanDon());
    }

    public int DamePhanDon()
    {
        int counterAttackDame = Mathf.RoundToInt(Atk * 0.5f);
        return counterAttackDame;
    }
}