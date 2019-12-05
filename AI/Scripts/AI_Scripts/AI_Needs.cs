using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AI_Needs : MonoBehaviour
{
    public bool gottaPee;
    public NavMeshAgent agent;

    [Range(0, 100)]
    public float bladder, hunger, focus, social, comfort, room;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        bladder = Random.Range(30, 80);
    }

    void Update()
    {
        CheckBladder();
    }

    public void GoBackToWork()
    {

    }

    void CheckBladder() 
    {
        bladder -= 1 * Time.deltaTime;

        gottaPee = bladder <= 10 ? true : false;
    }
}
