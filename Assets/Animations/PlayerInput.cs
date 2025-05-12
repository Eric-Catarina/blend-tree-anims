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

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on this GameObject");
        }
        
        // Garante que comece com a animação standing desativada
        animator.SetBool(standParam, false);
    }

    void Update()
    {
        // Só processa movimento se não estiver em standing
        if (!animator.GetBool(standParam))
        {
            HandleMovementInput();
        }
        
        HandleStandInput();
        
        // Atualiza o parâmetro de velocidade no Animator
        animator.SetFloat(speedParam, currentSpeed);
    }

    private void HandleMovementInput()
    {
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
        // Tecla para alternar a animação FemaleStanding
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool currentlyStanding = animator.GetBool(standParam);
            animator.SetBool(standParam, !currentlyStanding);
            
            // Se entrou no modo standing, reseta a velocidade
            if (!currentlyStanding)
            {
                currentSpeed = 0f;
            }
        }
    }
}