using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompassSceneHandler : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Text textName;
    [SerializeField] private Text textRole;
    [SerializeField] private Text textBest;

    private void Start()
    {
        exitButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });
    }

    public void ClickGrace()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        textName.text = "Grace Ko";
        textRole.text = "Design Lead, Gameplay Programming";
        textBest.text = "I had a lot of fun designing the spells and implementing the code for all of them. It felt like real spellcrafting, how I had to think about how each spell works and what components would be needed to cast it. It was also really gratifying to see all the physical components come together.";
    }

    public void ClickSydney()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        textName.text = "Sydney Birakos";
        textRole.text = "Producer";
        textBest.text = "The best part of working on this project was being able to work with a group of such talented people who were all passionate about the work that they were doing. It was an honor managing this team and seeing the amazing work that they produced.";
    }

    public void ClickMoises()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        textName.text = "Moises Martinez";
        textRole.text = "Networking, Backend Programming";
        textBest.text = "It was amazing learning more about multiplayer gaming and how complex it can get.  But my favorite part of this experience was seeing this game come to life by the hands of these awesome people.  This team is a great example of what successful team dynamic is supposed to be. We all worked hard and with passion, I will miss all of them. ";
    }

    public void ClickJan()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        textName.text = "Jan Yu";
        textRole.text = "General Programming";
        textBest.text = "My favorite part of working on the game is working with the team. I learned a lot from working with my teammates and it was an amazing experience being able to develop a game with them. They are some of the best teammates I ever had and I was able to improve myself as a developer because of them.";
    }

    public void ClickMalcolm()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        textName.text = "Malcolm Riley";
        textRole.text = "Technical Artist, Toolmaking, VFX, UI and Physical Assets";
        textBest.text = "Although I enjoyed preparing the physical assets and writing shaders for the visual effects, I think my favorite part was toolmaking. The programmers would express a need, and I would try to write a script and facilitate their productivity towards that goal. Seeing them use my tool and their response of “Wow, that was easy” always made my day. I like being helpful and that felt like helping to me.";
    }

    public void ClickJohn()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        textName.text = "John Park";
        textRole.text = "Producer";
        textBest.text = "The best part of working on this project was being able to work with a group of such talented people who were all passionate about the work that they were doing. It was an honor managing this team and seeing the amazing work that they produced.";
    }

    public void ClickDina()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        textName.text = "Dina Rosenberg";
        textRole.text = "Digital ink & paint, Environmental artist";
        textBest.text = "My favorite part of creating this game was seeing my art become part of a whole, and different components that may have seemed random at the time coming together to make the game look like it does now. I also love that I was able to immerse myself in the world of these characters and shape what that world looked like.";
    }

    public void ClickJeff()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        textName.text = "Jeffrey Liu";
        textRole.text = "Sound Design, Composer";
        textBest.text = "My favorite part was simply getting out there and recording audio, then seeing my work come to life in the app. This was my first time creating sound for a mobile game, so I had to think differently from how I usually approach my design. Music was something I never did before, but I am still quite satisfied with how it turned out.";
    }
}
