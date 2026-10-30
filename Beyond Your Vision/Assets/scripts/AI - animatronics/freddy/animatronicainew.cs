using UnityEngine;

public class animatronicainew : MonoBehaviour
{
    [SerializeField] private int stage = 1;
    [Range(0, 20)] [SerializeField] private int aiLevel = 10;

    [SerializeField] private Vector3 s1_pos;
    [SerializeField] private Vector3 s2_pos;
    [SerializeField] private Vector3 s3_pos;

    [SerializeField] private FnafDoor fnafdoor;
    [SerializeField] private Transform player;
    [SerializeField] private GameOver gameOver;

    [SerializeField] private float stageTime = 10f;
    [SerializeField] private float warningTime = 10f;
    [SerializeField] private float chaseSpeed = 4f;

    private float timer;
    private bool canMove;

    void Start()
    {
        if (s1_pos == null || s2_pos == null || s3_pos == null || fnafdoor == null || player == null || gameOver == null)
        {
            Debug.LogError("Freddy", this);
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
            TryToMove(2);
        }
        else if (stage == 2 && timer <= 0)
        {
            TryToMove(3);
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
                gameOver.Show();
                enabled = false;
            }
        }
    }

    void TryToMove(int nextStage)
    {
        if (aiLevel <= 0)
        {
            timer = stageTime;
            return;
        }

        if (canMove == false)
        {
            bool flip = Random.Range(1, 21) <= aiLevel;

            if (flip == false)
            {
                Debug.Log("Freddy : false");
                canMove = true;
                timer = stageTime;
                return;
            }
        }

        Debug.Log("Freddy : true");
        GoToStage(nextStage);
    }

    void GoToStage(int newStage)
    {
        stage = newStage;
        timer = stageTime;
        canMove = false;
        Debug.Log("Freddy is now in stage " + stage);

        if (stage == 1) transform.position = s1_pos;
        if (stage == 2) transform.position = s2_pos;

        if (stage == 3)
        {
            transform.position = s3_pos;
            timer = warningTime;
        }
    }
}
