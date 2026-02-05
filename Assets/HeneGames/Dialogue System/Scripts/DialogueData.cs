using System.Collections.Generic;
using UnityEngine;

namespace HeneGames.DialogueSystem
{
    [CreateAssetMenu(fileName = "DialogueOptionsSO", menuName = "HeneGames/Dialogue/Dialogue Data")]
    public class DialogueData : ScriptableObject
    {
        [Header("Dialogue Content")]
        public List<NPC_Centence> sentences = new List<NPC_Centence>();
    }
}