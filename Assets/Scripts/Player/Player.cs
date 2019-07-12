using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{
    
    public float moveSpeed = 5;
    // heath E13, 23
    public int heath = 20; // heath of player 
    public Image heathBar;


    Camera viewCamera;
    PlayerController controller;
    GunController gunController;


    
    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
    }
   
    public void TakeDamage(int amount) // E13
    {
        heath -= amount;
        heathBar.fillAmount = heath/10f ; // fill the heath Bar
        if(heath <= 0) // nếu máu == 0 thì gọi thần chết DieBumbum đến !
        {
            DieBumBum();
            SceneManager.LoadScene("Menu");
        }
    }
    
    void DieBumBum() // E13
    {
        Destroy(gameObject); // Destroy Player
    }

    void Update()
    {
        // Movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        // Look input
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin,point,Color.red);
            controller.LookAt(point);
        }

        // Weapon input
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}