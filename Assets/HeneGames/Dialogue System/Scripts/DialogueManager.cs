using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem; // Added for New Input System

namespace HeneGames.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        private int currentSentence;
        private float coolDownTimer;
        private bool dialogueIsOn;
        private DialogueTrigger dialogueTrigger;

        public enum TriggerState
        {
            Collision,
            Input
        }

        [Header("References")]
        [SerializeField] private AudioSource audioSource;

        [Header("Events")]
        public UnityEvent startDialogueEvent;
        public UnityEvent nextSentenceDialogueEvent;
        public UnityEvent endDialogueEvent;

        [Header("Dialogue")]
        [SerializeField] private TriggerState triggerState;
        [SerializeField] private List<NPC_Centence> sentences = new List<NPC_Centence>();

        private void Update()
        {
            //Timer
            if (coolDownTimer > 0f)
            {
                coolDownTimer -= Time.deltaTime;
            }

            //Start dialogue by input
            if (Keyboard.current != null)
            {
                string keyName = DialogueUI.instance.actionInput.ToString();

                // Cast the control to a KeyControl to access .wasPressedThisFrame
                var control = Keyboard.current[keyName] as UnityEngine.InputSystem.Controls.KeyControl;

                if (control != null && control.wasPressedThisFrame && dialogueTrigger != null && !dialogueIsOn)
                {
                    //Trigger event inside DialogueTrigger component
                    if (dialogueTrigger != null)
                    {
                        dialogueTrigger.startDialogueEvent.Invoke();
                    }

                    startDialogueEvent.Invoke();

                    //If component found start dialogue
                    DialogueUI.instance.StartDialogue(this);

                    //Hide interaction UI
                    DialogueUI.instance.ShowInteractionUI(false);

                    dialogueIsOn = true;
                }
            }
        }

        //Start dialogue by trigger
        private void OnTriggerEnter(Collider other)
        {
            if (triggerState == TriggerState.Collision && !dialogueIsOn)
            {
                if (other.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
                {
                    dialogueTrigger = _trigger;
                    dialogueTrigger.startDialogueEvent.Invoke();

                    startDialogueEvent.Invoke();

                    DialogueUI.instance.StartDialogue(this);

                    dialogueIsOn = true;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (triggerState == TriggerState.Collision && !dialogueIsOn)
            {
                if (collision.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
                {
                    dialogueTrigger = _trigger;
                    dialogueTrigger.startDialogueEvent.Invoke();

                    startDialogueEvent.Invoke();

                    DialogueUI.instance.StartDialogue(this);

                    dialogueIsOn = true;
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (dialogueTrigger != null)
                return;

            if (triggerState == TriggerState.Input && dialogueTrigger == null)
            {
                if (other.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
                {
                    DialogueUI.instance.ShowInteractionUI(true);

                    dialogueTrigger = _trigger;
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (dialogueTrigger != null)
                return;

            if (triggerState == TriggerState.Input && dialogueTrigger == null)
            {
                if (collision.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
                {
                    DialogueUI.instance.ShowInteractionUI(true);

                    dialogueTrigger = _trigger;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
            {
                DialogueUI.instance.ShowInteractionUI(false);

                StopDialogue();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
            {
                DialogueUI.instance.ShowInteractionUI(false);

                StopDialogue();
            }
        }

        public void StartDialogue()
        {
            if (dialogueTrigger != null)
            {
                dialogueTrigger.startDialogueEvent.Invoke();
            }

            currentSentence = 0;

            ShowCurrentSentence();

            PlaySound(sentences[currentSentence].sentenceSound);

            coolDownTimer = sentences[currentSentence].skipDelayTime;
        }

        public void NextSentence(out bool lastSentence)
        {
            if (coolDownTimer > 0f)
            {
                lastSentence = false;
                return;
            }

            currentSentence++;

            if (dialogueTrigger != null)
            {
                dialogueTrigger.nextSentenceDialogueEvent.Invoke();
            }

            nextSentenceDialogueEvent.Invoke();

            if (currentSentence > sentences.Count - 1)
            {
                StopDialogue();

                lastSentence = true;

                endDialogueEvent.Invoke();

                return;
            }

            lastSentence = false;

            PlaySound(sentences[currentSentence].sentenceSound);

            ShowCurrentSentence();

            coolDownTimer = sentences[currentSentence].skipDelayTime;
        }

        public void StopDialogue()
        {
            if (dialogueTrigger != null)
            {
                dialogueTrigger.endDialogueEvent.Invoke();
            }

            DialogueUI.instance.ClearText();

            if (audioSource != null)
            {
                audioSource.Stop();
            }

            dialogueIsOn = false;
            dialogueTrigger = null;
        }

        private void PlaySound(AudioClip _audioClip)
        {
            if (_audioClip == null || audioSource == null)
                return;

            audioSource.Stop();

            audioSource.PlayOneShot(_audioClip);
        }

        private void ShowCurrentSentence()
        {
            if (sentences[currentSentence].dialogueCharacter != null)
            {
                DialogueUI.instance.ShowSentence(sentences[currentSentence].dialogueCharacter, sentences[currentSentence].sentence);

                sentences[currentSentence].sentenceEvent.Invoke();
            }
            else
            {
                DialogueCharacter _dialogueCharacter = new DialogueCharacter();
                _dialogueCharacter.characterName = "";
                _dialogueCharacter.characterPhoto = null;

                DialogueUI.instance.ShowSentence(_dialogueCharacter, sentences[currentSentence].sentence);

                sentences[currentSentence].sentenceEvent.Invoke();
            }
        }

        public int CurrentSentenceLenght()
        {
            if (sentences.Count <= 0)
                return 0;

            return sentences[currentSentence].sentence.Length;
        }
    }

    [System.Serializable]
    public class NPC_Centence
    {
        [Header("------------------------------------------------------------")]

        public DialogueCharacter dialogueCharacter;

        [TextArea(3, 10)]
        public string sentence;

        public float skipDelayTime = 0.5f;

        public AudioClip sentenceSound;

        public UnityEvent sentenceEvent;
    }
}