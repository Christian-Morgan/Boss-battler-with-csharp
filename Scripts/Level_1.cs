using Godot;

public partial class Level_1 : Node
{
    private AudioStreamPlayer audioPlayer;

    public override void _Ready()
    {
        audioPlayer = GetNode<AudioStreamPlayer>("CanvasLayer/AudioStreamPlayer");

        if (audioPlayer != null)
        {
            audioPlayer.VolumeDb = -5; // Adjust volume if necessary
            audioPlayer.Play();        // Start playing the song
        }
        else
        {
            GD.PrintErr("AudioStreamPlayer not found in the scene.");
        }
    }

    public void StopMusic()
    {
        if (audioPlayer != null)
            audioPlayer.Stop();
    }

    public void PauseMusic()
    {
        if (audioPlayer != null)
            audioPlayer.StreamPaused = true;
    }

    public void ResumeMusic()
    {
        if (audioPlayer != null)
        {
            audioPlayer.StreamPaused = false;
            audioPlayer.Play();
        }
    }
}
