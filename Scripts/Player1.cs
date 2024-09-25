using Godot;
using System;
using System.IO;

public partial class Player1 : CharacterBody3D
{
    public const float Speed = 15.0f;
    public const float JumpVelocity = 18f;
    private float dashTime = .2f;  // Duration of the dash (in seconds)
    private float dashSpeed = 75f;  // Dash speed multiplier
    private bool isDashing = false;
    private bool canDash = true;
    private float dashCooldown = 1.0f;  // Cooldown duration (in seconds)
    private Timer dashCooldownTimer;
    private AudioStreamPlayer jumpSound;
    private AudioStreamPlayer dashSound;

    private Label deathLabel;  // Reference to the death UI label
    private Button playAgainButton;  // Reference to the "Play Again" button
    private Button quitButton;
    private bool isDead = false;  // Track if the player is dead

    private float currentDashTime = 0;  // Timer for current dash duration

    public override void _Ready()
    {
        // Get the death label and play again button from the scene
        deathLabel = GetNode<Label>("../playAgain/Label");
        quitButton = GetNode<Button>("../playAgain/VBoxContainer/QuitButton");
        playAgainButton = GetNode<Button>("../playAgain/VBoxContainer/PlayAgainButton");
        if (playAgainButton != null)
        {
            // Subscribe to the "Pressed" event for the "Play Again" button
            playAgainButton.Pressed += OnPlayAgainPressed;
        }
        if (quitButton != null)
        {
            quitButton.Pressed += OnQuitPressed;
        }

        // Timer setup for dash cooldown
        dashCooldownTimer = GetNode<Timer>("DashCooldownTimer");
        dashCooldownTimer.Timeout += OnDashCooldownTimeout;

        // Initially hide the "Play Again" button
        deathLabel.Visible = false;
        playAgainButton.Visible = false;
        quitButton.Visible = false;
        jumpSound = GetNode<AudioStreamPlayer>("Jump");
        dashSound = GetNode<AudioStreamPlayer>("DashSound");
    }

    public override void _PhysicsProcess(double delta)
    {
        // Check if the player is dead
        if (isDead)
            return;  // Prevent any further movement if dead

        Vector3 velocity = Velocity;

        // Check if the player falls off the platform
        if (GlobalTransform.Origin.Y < -10)  // Example threshold: -10 units below the platform
        {
            PlayerDied();
            return;
        }

        // Add gravity.
        if (!IsOnFloor())
        {
            velocity += 5 * GetGravity() * (float)delta;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
            if (jumpSound != null)
            {
                jumpSound.Play();
            }
        }

        // Handle input and movement
        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_up", "move_back");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

        // Handle Dash
        if (Input.IsActionJustPressed("dash") && direction != Vector3.Zero && canDash && !isDashing)
        {
            // Start dash
            isDashing = true;
            canDash = false;  // Disable dashing during cooldown
            currentDashTime = dashTime;  // Set dash duration
            dashSound.Play();
            // Access the CooldownManager and start the cooldown
            // Since CooldownManager is an autoload singleton, you can access it directly by its name
            CooldownManager cooldownManager = (CooldownManager)GetNode("/root/CooldownManager");

            // Start the dash cooldown
            cooldownManager.StartDashCooldown(dashCooldown);  // Start cooldown
        }

        if (isDashing)
        {
            // Perform dash movement (override normal movement with dash speed)
            velocity.X = direction.X * dashSpeed;
            velocity.Z = direction.Z * dashSpeed;

            currentDashTime -= (float)delta;  // Reduce dash time

            // Stop dashing after the dash duration ends
            if (currentDashTime <= 0)
            {
                isDashing = false;
                dashCooldownTimer.Start(dashCooldown);  // Start cooldown timer after dash ends
            }
        }
        else
        {
            // Normal movement when not dashing
            if (direction != Vector3.Zero)
            {
                velocity.X = direction.X * Speed;
                velocity.Z = direction.Z * Speed;
            }
            else
            {
                // Decelerate smoothly
                velocity.X = Mathf.MoveToward(velocity.X, 0, Speed);
                velocity.Z = Mathf.MoveToward(velocity.Z, 0, Speed);
            }
        }

        // Set and apply velocity
        Velocity = velocity;
        MoveAndSlide();
    }

    private void PlayerDied()
    {
        isDead = true;  // Set the player to "dead" state
        deathLabel.Visible = true;  // Show the "You died!" label
        playAgainButton.Visible = true;  // Show the "Play Again" button
        quitButton.Visible = true;
    }

    private void OnDashCooldownTimeout()
    {
        canDash = true;  // Re-enable dashing after the cooldown
    }

    // This method will be called when the "Play Again" button is pressed
    private void OnPlayAgainPressed()
    {
        // Reload the current scene to reset the game
        GetTree().ReloadCurrentScene();
    }
    private void OnQuitPressed()
    {
        GetTree().Quit();
    }
}
