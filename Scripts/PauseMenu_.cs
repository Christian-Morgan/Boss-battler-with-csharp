using Godot;
using System;

public partial class PauseMenu : Control
{
    private bool _isPaused = false;

    public bool IsPaused
    {
        get { return _isPaused; }
        set { SetPaused(value); }
    }

    public override void _Ready()
    {
        // Connect button signals to their respective methods
        GetNode<Button>("Resume").Pressed += OnResumeButtonPressed;
        GetNode<Button>("Quit").Pressed += OnQuitButtonPressed;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("pause"))
        {
            IsPaused = !IsPaused;
        }
    }

    private void SetPaused(bool value)
    {
        _isPaused = value;
        GetTree().Paused = _isPaused;
        Visible = _isPaused;
    }

    // Method called when the resume button is pressed
    private void OnResumeButtonPressed()
    {
        IsPaused = false;
    }

    // Method called when the quit button is pressed
    private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }
}
