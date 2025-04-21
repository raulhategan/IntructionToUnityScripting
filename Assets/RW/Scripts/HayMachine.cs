using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayMachine : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed;
    public float horizontalBoundary = 22;
    public GameObject hayBalePrefab; // 1
    public Transform haySpawnpoint; // 2
    public float shootInterval; // 3
    private float shootTimer; // 4
    public Transform modelParent; // 1

    // 2
    public GameObject blueModelPrefab;
    public GameObject yellowModelPrefab;
    public GameObject redModelPrefab;


    void Start()
    {
        LoadModel();

    }

    private void LoadModel()
    {
        Destroy(modelParent.GetChild(0).gameObject); // 1

        switch (GameSettings.hayMachineColor) // 2
        {
            case HayMachineColor.Blue:
                Instantiate(blueModelPrefab, modelParent);
                break;

            case HayMachineColor.Yellow:
                Instantiate(yellowModelPrefab, modelParent);
                break;

            case HayMachineColor.Red:
                Instantiate(redModelPrefab, modelParent);
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
        UpdateShooting();


    }

    private void UpdateMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // 1

        if (horizontalInput < 0 && transform.position.x > -horizontalBoundary) // 1
        {
            transform.Translate(transform.right * -movementSpeed * Time.deltaTime);
        }
        else if (horizontalInput > 0 && transform.position.x < horizontalBoundary) // 2
        {
            transform.Translate(transform.right * movementSpeed * Time.deltaTime);
        }
    }

    private void UpdateShooting()
    {
        shootTimer -= Time.deltaTime; // 1

        if (shootTimer <= 0 && Input.GetKey(KeyCode.Space)) // 2
        {
            shootTimer = shootInterval; // 3
            ShootHay(); // 4
        }
        SoundManager.Instance.PlayShootClip();

    }

    private void ShootHay()
    {
        Instantiate(hayBalePrefab, haySpawnpoint.position, Quaternion.identity);
    }

}
