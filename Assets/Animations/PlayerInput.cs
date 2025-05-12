using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Animator animator;
    
    // Parâmetros do Animator
    private readonly int speedParam = Animator.StringToHash("Speed");
    private readonly int standParam = Animator.StringToHash("Stand");
    
    // Configurações
    public float walkSpeed = 1f;
    public float runSpeed = 2f;
    public float acceleration = 2f;
    public float deceleration = 2f;
    
    private float currentSpeed = 0f;
    private bool isStanding = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on this GameObject");
        }
    }

    void Update()
    {
        HandleMovementInput();
        HandleStandInput();
        
        // Atualiza o parâmetro de velocidade no Animator
        animator.SetFloat(speedParam, currentSpeed);
    }

    private void HandleMovementInput()
    {
        if (isStanding) return; // Não permite movimento enquanto está em FemaleStanding

        // Teclas para movimento
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Correr (FastRun)
                currentSpeed = Mathf.MoveTowards(currentSpeed, runSpeed, acceleration * Time.deltaTime);
            }
            else
            {
                // Andar (Walking)
                currentSpeed = Mathf.MoveTowards(currentSpeed, walkSpeed, acceleration * Time.deltaTime);
            }
        }
        else
        {
            // Voltar ao idle
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }
    }

    private void HandleStandInput()
    {
        // Tecla para ativar a animação FemaleStanding (usa Layer Mask)
        if (Input.GetKeyDown(KeyCode.E))
        {
            isStanding = !isStanding;
            animator.SetBool(standParam, isStanding);
            
            // Se entrou no modo standing, reseta a velocidade
            if (isStanding)
            {
                currentSpeed = 0f;
            }
        }
    }
}