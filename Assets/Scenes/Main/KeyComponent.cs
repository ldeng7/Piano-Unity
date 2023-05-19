using UnityEngine;

namespace DM.Scenes.Main {
    public class KeyComponent: MonoBehaviour {
        public sbyte keyIndex;
        private Octave.Pitch pitch;
        private UnityEngine.UI.Text text2;
        private AudioSource audioSource;
        private bool mouseIn = false;
        private bool pressing = false;

        void Start() {
            var parent = this.GetComponentInParent<PianoComponent>();
            parent.updateModeEvent.AddListener(() => this.text2.text = this.pitch.levelStr);

            this.pitch = parent.hierarchy.Pitch(this.keyIndex);
            foreach (var text in this.GetComponentsInChildren<UnityEngine.UI.Text>()) {
                switch (text.name) {
                case "Text1":
                    text.text = this.pitch.FullName();
                    break;
                case "Text2":
                    this.text2 = text;
                    break;
                }
            }

            this.audioSource = this.GetComponent<AudioSource>();
            this.audioSource.clip = Resources.Load<AudioClip>("Pitch-" + this.pitch.FullName());
        }

        private void setPressing(bool pressing) {
            this.pressing = pressing;
            var s = this.transform.localScale;
            if (pressing) {
                s.x = 0.85f;
                s.z = 0.97f;
                this.audioSource.Play();
            } else {
                s.x = 1f;
                s.z = 1f;
            }
            this.transform.localScale = s;
        }

        void OnMouseEnter() {
            this.mouseIn = true;
        }

        void OnMouseExit() {
            this.mouseIn = false;
        }

        void Update() {
            if (this.mouseIn) {
                if (this.pressing != Input.GetMouseButton(0)) {
                    this.setPressing(!this.pressing);
                }
            } else {
                if (this.pressing) {
                    this.setPressing(false);
                }
            }
        }
    }
}
