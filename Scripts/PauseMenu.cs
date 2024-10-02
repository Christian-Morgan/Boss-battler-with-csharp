using Godot;
using System;

public partial class PauseMenu : Control
{
    public override void _Process(double delta)
    {
        // Check if the pause_game action is triggered
        if (Input.IsActionJustPressed("pause"))
        {
            // Toggle the paused state of the game
            GetTree().Paused = true;

            if (GetTree().Paused)
            {
                ChangeToScene("pause_menu.tscn");
            }
        }
    }

    public void ChangeToScene(string sceneName)
    {
        GetTree().ChangeSceneToFile($"res://Scenes/{sceneName}");
    }
}
