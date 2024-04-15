using BurnoutBuster.CommandPat.Commands;
using System.Collections.Generic;

namespace BurnoutBuster.Input
{
    public class ChordAnalyzer
    {
        // P R O P E R T I E S

        //buffer 
        Queue<Note> inputBuffer;
        int queueCapacity;
        Note[] tempBuffer;
        bool canClearBuffer;

        private ChordMap chordMap;
        ActionCommands CommandOutput;

        // C O N S T R U C T O R
        public ChordAnalyzer()
        {
            queueCapacity = 3;
            inputBuffer = new Queue<Note>(queueCapacity);
            chordMap = new ChordMap();
            canClearBuffer = false;
        }

        // I N I T
        public void Initialize()
        {

        }

        // U P D A T E
        public void Update(float time)
        {
            UpdateNotes(time);
        }
        void UpdateNotes(float time)
        {
            foreach (Note note in inputBuffer)
            {
                note.Duration.UpdateTimer(time);
                if (note.Duration.State == Utility.TimerState.Ended)
                {
                    canClearBuffer = true;
                    break;
                }
            }
            ClearBufferIfAble();
        }
        void ClearBufferIfAble()
        {
            if (canClearBuffer)
            {
                inputBuffer.Clear();
                canClearBuffer = false;
            }
        }

        // M I S C   M E T H O D S
        public void AddNote(Note note)
        {
            if (inputBuffer.Count == queueCapacity) // checks to see if the queue is full and makes room for the next item if it is
                inputBuffer.Dequeue();
            inputBuffer.Enqueue(note);
        }
        public void ClearBuffer()
        {
            inputBuffer.Clear();
        }

        public void CheckQueue()
        {
            Chord currentChord = new Chord(0, 0, 0);
            tempBuffer = inputBuffer.ToArray();
            
            if (tempBuffer.Length > 0 )
                currentChord.Note1 = tempBuffer[0].Command;
            if (tempBuffer.Length > 1 )
                currentChord.Note2 = tempBuffer[1].Command;
            if (tempBuffer.Length > 2)
                currentChord.Note3 = tempBuffer[2].Command;

            IsChordValid(currentChord, out CommandOutput);
        }

        bool CompareChords(Chord chord1, Chord chord2)
        {
            if (chord1.Note1 == chord2.Note1 
                && chord1.Note2 == chord2.Note2
                && chord1.Note3 == chord2.Note3)
            {
                return true;
            }
            return false;
        }

        bool IsChordValid(Chord chord, out ActionCommands commandRef)
        {
            commandRef = ActionCommands.Null;
            foreach (Chord chordFromMap in chordMap.MappedCommands.Keys)
            {
                if (CompareChords(chord, chordFromMap))
                {
                    commandRef = chordMap.MappedCommands[chordFromMap];
                    return true;
                }
            }
            return false;
        }

        public ActionCommands GetCurrentCommandOutput()
        {
            return CommandOutput;
        }
    }
}
