using UnityEngine;

public class animatronicFREDDY : MonoBehaviour
{
    [SerializeField] private int stage = 1;

    [SerializeField] private Vector3 s1_pos;
    [SerializeField] private Vector3 s2_pos;
    [SerializeField] private Vector3 s3_pos;

    [SerializeField] private FnafDoor fnafdoor;
    [SerializeField] private Transform player;

    [SerializeField] private float stageTime = 10f;
    [SerializeField] private float warningTime = 10f;
    [SerializeField] private float chaseSpeed = 4f;

    private float timer;

    void Start()
    {
        if (s1_pos == null || s2_pos == null || s3_pos == null || fnafdoor == null || player == null)
        {
            Debug.LogError("Error", this);
            enabled = false;
            return;
        }

        GoToStage(1);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (stage == 1 && timer <= 0)
        {
            GoToStage(2);
        }
        else if (stage == 2 && timer <= 0)
        {
            GoToStage(3);
        }
        else if (stage == 3)
        {
            if (fnafdoor.IsOpen == false)
            {
                GoToStage(1);
            }
            else if (timer <= 0)
            {
                GoToStage(4);
            }
        }
        else if (stage == 4)
        {
            Vector3 target = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.position = Vector3.MoveTowards(transform.position, target, chaseSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 1.2f)
            {
                Debug.Log("You died");
                enabled = false;
            }
        }
    }

    void GoToStage(int newStage)
    {
        stage = newStage;
        timer = stageTime;
        Debug.Log("Freddy is now in stage " + stage);

        if (stage == 1)
        {
            transform.position = s1_pos;
        }
        if (stage == 2)
        {
            transform.position = s2_pos;
        }
        if (stage == 3)
        {
            transform.position = s3_pos;
            timer = warningTime;
        }
    }
}