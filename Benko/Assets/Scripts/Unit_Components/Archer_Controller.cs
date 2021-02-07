using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Archer_Controller : MonoBehaviour
{
    private Transform target;
    public GameObject Arrow;
    public GameObject ArrowStartPoint;
    private isSelected IsSelected;
    private Node currentNode;
    private Node lastNode;
    GameController gameController;
    CustomGrid customGrid;
    Vector3 lastMove = new Vector3(0, 0, 0);
    Snap snap;
    public GameObject DiePX;
    private bool isDead;
    void Start()
    {
        InvokeRepeating("UpdateTarget", 1f, 0.5f); //0f, 0.01f
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        customGrid = gameController.GetComponent<CustomGrid>();
        snap = gameObject.GetComponent<Snap>();
        isDead = false;
        IsSelected = GetComponent<isSelected>();

        currentNode = customGrid.nodes[customGrid.gridIndexFromPos(transform.position.x, transform.position.z)].GetComponent<Node>();
        lastNode = currentNode;
        currentNode.isOccupied = true;
        snap.SnapToGrid();
    }
    void Update()
    {
        if (!isDead)
        {
            if (gameObject.GetComponent<Health>().Currenthealth <= 0)
            {
                Die();
            }

            Vector3 move = new Vector3(0, 0, 0);

            if (IsSelected.IsSelected)
            {
                Vector3 vSnap = snap.vectorToClosestSnapPoint();

                // Node nodeToLeft = null;
                // Node nodeToRight = null;
                // Node nodeAbove = null;
                // Node nodeBelow = null;
                bool leftIsFree = false;
                bool rightIsFree = false;
                bool aboveIsFree = false;
                bool belowIsFree = false;
                try
                {
                    Node n = customGrid.nodes[customGrid.gridIndexFromPos(transform.position.x, transform.position.z)].GetComponent<Node>().adjacents[3].GetComponent<Node>();
                    if (!n.isObstacle && !n.isOccupied)
                        leftIsFree = true;
                }
                catch (System.NullReferenceException) { }
                try
                {
                    Node n = customGrid.nodes[customGrid.gridIndexFromPos(transform.position.x, transform.position.z)].GetComponent<Node>().adjacents[1].GetComponent<Node>();
                    if (!n.isObstacle && !n.isOccupied)
                        rightIsFree = true;
                }
                catch (System.NullReferenceException) { }
                try
                {
                    Node n = customGrid.nodes[customGrid.gridIndexFromPos(transform.position.x, transform.position.z)].GetComponent<Node>().adjacents[0].GetComponent<Node>();
                    if (!n.isObstacle && !n.isOccupied)
                        aboveIsFree = true;
                }
                catch (System.NullReferenceException) { }
                try
                {
                    Node n = customGrid.nodes[customGrid.gridIndexFromPos(transform.position.x, transform.position.z)].GetComponent<Node>().adjacents[2].GetComponent<Node>();
                    if (!n.isObstacle && !n.isOccupied)
                        belowIsFree = true;
                }
                catch (System.NullReferenceException) { }

                if ((leftIsFree && ((Input.GetKey("a") && (lastMove.z == 0 || vSnap == Vector3.zero)))
                    || (lastMove.x < 0 && vSnap != Vector3.zero)))
                {
                    move.x -= GetComponent<UnitAttributes>().walkspeed * Time.deltaTime;
                    GetComponent<UnitAnimator>().Run();

                }
                else if ((rightIsFree && Input.GetKey("d") && (lastMove.z == 0 || vSnap == Vector3.zero))
                    || (lastMove.x > 0 && vSnap != Vector3.zero))
                {
                    move.x = GetComponent<UnitAttributes>().walkspeed * Time.deltaTime;
                    GetComponent<UnitAnimator>().Run();

                }
                else if ((aboveIsFree && Input.GetKey("w") && (lastMove.x == 0 || vSnap == Vector3.zero))
                    || (lastMove.z > 0 && vSnap != Vector3.zero))
                {
                    move.z = GetComponent<UnitAttributes>().walkspeed * Time.deltaTime;
                    GetComponent<UnitAnimator>().Run();

                }
                else if ((belowIsFree && Input.GetKey("s") && (lastMove.x == 0 || vSnap == Vector3.zero))
                    || (lastMove.z < 0 && vSnap != Vector3.zero))
                {
                    move.z = -GetComponent<UnitAttributes>().walkspeed * Time.deltaTime;
                    GetComponent<UnitAnimator>().Run();

                }
                if ((!Input.GetKey("d") || (vSnap.x <= move.x && !rightIsFree)) && lastMove.x > 0 && vSnap.x >= 0 && vSnap.x <= move.x)
                {
                    move.x = vSnap.x;

                }
                else if ((!Input.GetKey("a") || (vSnap.x >= move.x && !leftIsFree)) && lastMove.x < 0 && vSnap.x <= 0 && vSnap.x >= move.x)
                {
                    move.x = vSnap.x;

                }
                else if ((!Input.GetKey("w") || (vSnap.z <= move.z && !aboveIsFree)) && lastMove.z > 0 && vSnap.z >= 0 && vSnap.z <= move.z)
                {
                    move.z = vSnap.z;

                }
                else if ((!Input.GetKey("s") || (vSnap.z >= move.z && !belowIsFree)) && lastMove.z < 0 && vSnap.z <= 0 && vSnap.z >= move.z)
                {
                    move.z = vSnap.z;
                }
            }

            Vector3 nextPos = transform.position;
            int posIndex = gameController.gridIndexFromPos(nextPos.x + move.x, nextPos.z);
            if (posIndex != -1 && posIndex < gameController.gameObject.GetComponent<CustomGrid>().nodes.Length)
            {
                GameObject currentNode = gameController.gameObject.GetComponent<CustomGrid>().nodes[posIndex];
                if (!currentNode.GetComponent<Node>().isObstacle)
                {
                    nextPos.x += move.x;
                }
            }
            posIndex = gameController.gridIndexFromPos(nextPos.x, nextPos.z + move.z);
            if (posIndex != -1 && posIndex < gameController.gameObject.GetComponent<CustomGrid>().nodes.Length)
            {
                GameObject currentNode = gameController.gameObject.GetComponent<CustomGrid>().nodes[posIndex];
                if (!currentNode.GetComponent<Node>().isObstacle)
                {
                    nextPos.z += move.z;
                }
            }

            lastMove = nextPos - transform.position;
            transform.position = nextPos;

            Node _lastNode = currentNode;
            currentNode = customGrid.nodes[customGrid.gridIndexFromPos(transform.position.x, transform.position.z)].GetComponent<Node>();
            if(currentNode != lastNode) {
                lastNode.isOccupied = false;
                currentNode.isOccupied = true;
            }
            lastNode = _lastNode;

            Vector3 face = new Vector3(lastMove.x, lastMove.y, lastMove.z);
            if (face.sqrMagnitude != 0)
            {
                float damping = 10f;
                face.y = 0;
                var targetRotation = Quaternion.LookRotation(face);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping);
            }
            else
            {
            }
            if (target == null && lastMove == Vector3.zero)
            {
                GetComponent<UnitAnimator>().Idle();
            }
            if (target == null) return;
            if (lastMove == Vector3.zero)
            {
                GetComponent<UnitAnimator>().Attack();

                Vector3 dir = target.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                float damping = 10f;
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * damping);
                // Vector3 rotation = lookRotation.eulerAngles;
                // transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
                //Shoot();
                //print("shoot");
            }
            GetComponent<UnitAttributes>().firecountdwon -= Time.deltaTime;
            if (GetComponent<UnitAttributes>().firecountdwon <= 1)
            {
                GetComponent<UnitAttributes>().firecountdwon = 1 / GetComponent<UnitAttributes>().firerate;
                //print("Shoot");
                //Shoot();
            }
        }
    }
    void UpdateTarget()
    {
        if (!isDead)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float ShortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;
            foreach (GameObject enemy in enemies)
            {
                float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (DistanceToEnemy < ShortestDistance)
                {
                    ShortestDistance = DistanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
            if (nearestEnemy != null && ShortestDistance <= GetComponent<UnitAttributes>().attackrange)
            {
                target = nearestEnemy.transform;
            }
            else target = null;
        }
    }
    public void Shoot()
    {
        if (!isDead)
        {
            GameObject go = Instantiate(Arrow, ArrowStartPoint.transform.position, ArrowStartPoint.transform.rotation);
            //print(go.transform.name);
            int damage = (int)GetComponent<UnitAttributes>().damage;
            go.GetComponent<ArrowController>().Seek(target, damage);
        }
    }
    public void Die()
    {
        GetComponent<UnitAnimator>().Death();
        GameObject go = Instantiate(DiePX, new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), Quaternion.identity); //instanciate Die Particle
        Destroy(go, 1.0f);
        gameObject.tag = "Untagged";
        IsSelected.IsSelected = false;
        isDead = true;
        Destroy(gameObject, 2.5f);
        currentNode.isOccupied = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GetComponent<UnitAttributes>().attackrange);
    }
}
