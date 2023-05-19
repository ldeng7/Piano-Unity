using System.Collections.Generic;

namespace DM.Scenes.Main.Octave {
    public class Hierarchy {
        private readonly List<Pitch> pitches;

        public Hierarchy(Pitch pitchStart, sbyte len) {
            this.pitches = new List<Pitch>(len) { pitchStart };
            Pitch pitchPrev = pitchStart;
            for (int i = 1; i < len; i++) {
                var pitch = pitchPrev.Next();
                this.pitches.Add(pitchPrev = pitch);
            }
        }

        public Pitch Pitch(int i) => this.pitches[i];

        public void ApplyMode(Mode mode) => mode.Apply(this.pitches);
    }
}
