using Godot;
using System;

public partial class CooldownManager : Node
{
    private float dashCooldown = 0f;
    private bool canDash = true;
    private bool cooldownsVisible = true;  // Default visibility

    private Label cooldownLabel;

    public override void _Ready()
    {
        // Find the label responsible for showing the cooldown
        cooldownLabel = GetNode<Label>("CanvasLayer/CooldownLabel");
        UpdateCooldownVisibility(); // Set the initial visibility based on the default value
    }

    public override void _Process(double delta)
    {
        // Update the cooldown timer if the player cannot dash
        if (!canDash)
        {
            dashCooldown -= (float)delta;
            if (dashCooldown <= 0)
            {
                dashCooldown = 0;
                canDash = true;
                cooldownLabel.Visible = false;  // Hide the label when the cooldown is done
            }
            else
            {
                UpdateCooldownLabel();
            }
        }
    }

    public void StartDashCooldown(float cooldownTime)
    {
        dashCooldown = cooldownTime;
        canDash = false;

        // Show the label if cooldowns are set to be visible
        if (cooldownsVisible)
        {
            cooldownLabel.Visible = true;
        }

        UpdateCooldownLabel();
    }

    private void UpdateCooldownLabel()
    {
        // Update the text to show the time remaining for the cooldown
        cooldownLabel.Text = $"Dash: {Mathf.Round(dashCooldown)}s";
    }

    // Method to return the visibility state of the cooldowns
    public bool AreCooldownsVisible()
    {
        return cooldownsVisible;
    }

    // Method to set the visibility of the cooldown label
    public void SetCooldownVisibility(bool visible)
    {
        cooldownsVisible = visible;
        UpdateCooldownVisibility();
    }

    private void UpdateCooldownVisibility()
    {
        // Set the cooldown label's visibility based on the player's preference
        if (canDash)
        {
            // If the player can dash, respect the visibility preference
            cooldownLabel.Visible = false;
        }
        else
        {
            // If the cooldown is in progress, only show the label if cooldowns are visible
            cooldownLabel.Visible = cooldownsVisible;
        }
    }
}
