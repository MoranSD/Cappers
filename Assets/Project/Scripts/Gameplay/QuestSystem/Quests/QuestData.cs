using System;

namespace Gameplay.QuestSystem.Quests
{
    [Serializable]
    public struct QuestData
    {
        public int QuestId;
        public int OwnerLocationId;

        public QuestData(int id, int ownerLocationId)
        {
            QuestId = id;
            OwnerLocationId = ownerLocationId;
        }

        public bool Compare(QuestData other)
        {
            return QuestId == other.QuestId && 
                   OwnerLocationId == other.OwnerLocationId;
        }

        public override string ToString()
        {
            return $"{OwnerLocationId}:{QuestId}";
        }
    }
}
