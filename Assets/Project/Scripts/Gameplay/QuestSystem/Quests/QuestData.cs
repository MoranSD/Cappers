using System;
using UnityEngine;

namespace Gameplay.QuestSystem.Quests
{
    [Serializable]
    public struct QuestData
    {
        [field: SerializeField] public int QuestId { get; private set; }
        [field: SerializeField] public int OwnerLocationId { get; private set; }

        public QuestData(int id, int ownerLocationId)
        {
            QuestId = id;
            OwnerLocationId = ownerLocationId;
        }

        public bool Compare(QuestData other)
        {
            return QuestId == other.QuestId && OwnerLocationId == other.OwnerLocationId;
        }

        public override string ToString()
        {
            return $"{OwnerLocationId}:{QuestId}";
        }
    }
}
