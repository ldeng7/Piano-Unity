using System.Collections.Generic;

namespace DM.Scenes.Main.Octave {
    public class Mode {
        public enum Name: sbyte {
            Major, Minor, ChinaPentatonic,
            Len,
        }

        private static readonly sbyte[][] levelSteps = {
            new sbyte[] { 2, 2, 1, 2, 2, 2, 1, },
            new sbyte[] { 2, 1, 2, 2, 1, 2, 2, },
            new sbyte[] { 2, 2, 3, 2, 3, },
        };

        private static readonly string[][] levelNames = {
            new string[] { "¢ñ", "¢ò", "¢ó", "¢ô", "¢õ", "¢ö", "¢÷", },
            new string[] { "¢ñ", "¢ò", "¢ó", "¢ô", "¢õ", "¢ö", "¢÷", },
            new string[] { "¹¬", "ÉÌ", "½Ç", "áç", "Óð", },
        };

        public Name name;
        public Pitch.Name basePitchName;

        internal void Apply(List<Pitch> pitches) {
            pitches.ForEach(pitch => {
                pitch.level = -1;
                pitch.levelStr = "";
            });

            var p0 = pitches[0];
            var i = this.basePitchName - p0.name;
            if (i > 0) {
                i -= (sbyte)Pitch.Name.Len;
            }
            var lss = levelSteps[(sbyte)this.name];
            sbyte iLevel = 0;
            for (; i < 0; i += lss[iLevel++]) { }

            var lns = levelNames[(sbyte)this.name];
            for (; i < pitches.Count; i += lss[iLevel++]) {
                if (iLevel >= lss.Length) {
                    iLevel = 0;
                }
                pitches[i].level = iLevel;
                pitches[i].levelStr = lns[iLevel];
            }
        }
    }
}
