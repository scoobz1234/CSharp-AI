using UnityEngine;
using UnityEngine.AI;

public class Employee : AI_Controller
{
    public bool isAtDesk;
    public bool isWorking;
    public float currentIncome;
    public float baselineIncome;
    
    void OnEnable()
    {
        needs = gameObject.GetComponent<AI_Needs>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        GameManager.instance.Employees.Add(this);
        renderer = gameObject.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        //check if the employee is working
        CheckState();
        //check if the employee is at the desk and is working
        CheckIncomeStatus();
        //check states and call functions depending on state
        CheckEmployeeState(currentEmployeeState);
        //check needs and call functions depending on needs
        CheckEmployeeNeedState(currentNeedState);
    }

    void CheckState() 
    {
        isWorking = currentEmployeeState == EmployeeStates.Work ? true : false;
    }

    void CheckIncomeStatus() 
    {
        currentIncome = isWorking == true && isAtDesk == true ? baselineIncome : 0;
    }

    void CheckEmployeeState(EmployeeStates state) 
    {
        switch (state)
        {
            case EmployeeStates.Work:
                Work();
                if (Time.time >= employeeStateStartTime + 20f)
                {
                    ChangeEmployeeState(EmployeeStates.Idle);
                }

                if (currentNeedState != NeedsStates.Fine)
                {
                    ChangeEmployeeState(EmployeeStates.FulfillNeed);
                }
                break;

            case EmployeeStates.Idle:
                Idle();
                if (currentNeedState != NeedsStates.Fine)
                {
                    ChangeEmployeeState(EmployeeStates.FulfillNeed);
                }
                break;

            case EmployeeStates.FulfillNeed:
                FulfillNeed();
                if (currentNeedState == NeedsStates.Fine)
                {
                    ChangeEmployeeState(EmployeeStates.Work);
                }
                break;
        }
    }

    void CheckEmployeeNeedState(NeedsStates state) 
    {
        switch (state)
        {
            case NeedsStates.Fine:
                Fine();
                if (needs.gottaPee == true)
                {
                    ChangeNeedsState(NeedsStates.GottaPee);
                }
                break;

            case NeedsStates.GottaPee:
                GottaPee();
                break;

            case NeedsStates.Peeing:
                Peeing();
                if (needs.gottaPee == false)
                {
                    ChangeNeedsState(NeedsStates.Fine);
                }
                break;
        }
    }
}
