using UnityEngine;

namespace DM.Scenes.Main {
    public class PianoComponent: MonoBehaviour {
        internal Octave.Hierarchy hierarchy { get; private set; }
        internal UnityEngine.Events.UnityEvent updateModeEvent { get; private set; }
        private Octave.Mode mode;

        private void Awake() {
            this.hierarchy = new Octave.Hierarchy(new Octave.Pitch(0, Octave.Pitch.Name.A), 88);
            this.updateModeEvent = new UnityEngine.Events.UnityEvent();
            this.mode = new Octave.Mode();
            this.mode.name = Octave.Mode.Name.Major;
            this.mode.basePitchName = Octave.Pitch.Name.C;
        }

        void Update() {
            bool updateMode = false;
            if (Input.GetKeyDown(KeyCode.RightAlt)) {
                updateMode = true;
                if (++this.mode.name == Octave.Mode.Name.Len) {
                    this.mode.name = Octave.Mode.Name.Major;
                };
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                updateMode = true;
                this.mode.basePitchName = Octave.Pitch.PrevName(this.mode.basePitchName, out bool _);
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                updateMode = true;
                this.mode.basePitchName = Octave.Pitch.NextName(this.mode.basePitchName, out bool _);
            }
            if (updateMode) {
                this.hierarchy.ApplyMode(this.mode);
                this.updateModeEvent.Invoke();
            }
        }
    }
}
