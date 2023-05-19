namespace DM.Scenes.Main.Octave {
    public class Pitch {
        public enum Name: sbyte {
            C, CS, D, DS, E, F, FS, G, GS, A, AS, B,
            Len,
        }

        private static readonly string[] nameStrs = {
            "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B",
        };

        public static Name PrevName(Name name, out bool sameGroup) {
            return (sameGroup = name != Name.C) ? --name : Name.B;
        }

        public static Name NextName(Name name, out bool sameGroup) {
            return (sameGroup = name != Name.B) ? ++name : Name.C;
        }

        public readonly sbyte group;
        public readonly Name name;
        public sbyte level { get; internal set; }
        public string levelStr { get; internal set; }

        public Pitch(sbyte group, Name name) {
            this.group = group;
            this.name = name;
            this.level = -1;
        }

        public string FullName() => nameStrs[(sbyte)this.name] + this.group;

        public Pitch Prev() {
            var name = PrevName(this.name, out bool sameGroup);
            return new Pitch((sbyte)(this.group - (sameGroup ? 0 : 1)), name);
        }

        public Pitch Next() {
            var name = NextName(this.name, out bool sameGroup);
            return new Pitch((sbyte)(this.group + (sameGroup ? 0 : 1)), name);
        }
    }
}
