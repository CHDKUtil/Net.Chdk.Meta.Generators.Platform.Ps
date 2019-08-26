﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Net.Chdk.Meta.Generators.Platform.Ps
{
    sealed class PsPlatformGenerator : InnerPlatformGenerator
    {
        protected override string Keyword => "PowerShot";

        protected override string[] Suffixes => new[] { "IS" };

        private IIxusPlatformGenerator IxusGenerator { get; }

        public PsPlatformGenerator(IIxusPlatformGenerator ixusGenerator)
        {
            IxusGenerator = ixusGenerator;
        }

        public override string GetPlatform(string[] models)
        {
            var ps = base.GetPlatform(models);
            if (ps != null && models.Length > 1)
            {
                var ixus = IxusGenerator.Generate(models[1]);
                if (ixus != null)
                {
                    return $"{ixus}_{ps}";
                }
            }
            return ps;
        }

        protected override IEnumerable<string> PreGenerate(string source)
        {
            var split = source.Split(' ');
            if (!Keyword.Equals(split[0]))
                return null;

            var index = Array.IndexOf(split, "Mark");
            if (index > 0)
            {
                var m = RomanToInteger(split[index + 1]);
                return split
                    .Take(index)
                    .Concat(new[] { m.ToString() })
                    .Skip(1);
            }

            return split.Skip(1);
        }

        protected override IEnumerable<string> Trim(IEnumerable<string> split)
        {
            Debug.Assert(split.Last().Equals("IS"));

            var split2 = split.Take(split.Count() - 1);
            var beforeLast = split2.Last();
            var index = GetIndexOfDigit(beforeLast);
            var series = beforeLast.Substring(0, index);
            var modelStr = beforeLast.Substring(index);
            uint model = uint.Parse(modelStr);
            switch (series)
            {
                case "": // ELPH
                case "A":
                case "SD":
                case "SX" when model < 100:
                    return split2;
                default:
                    return split;
            }
        }

        protected override IEnumerable<string> Process(IEnumerable<string> split)
        {
            if (split.Count() >= 2 && split.Skip(1).First() == "Facebook")
                return new[] { "N_Facebook" };
            return split;
        }

        private static int RomanToInteger(string roman)
        {
            switch (roman)
            {
                case "I":
                    return 1;
                case "II":
                    return 2;
                case "III":
                    return 3;
                case "IV":
                    return 4;
                default:
                    throw new InvalidOperationException();
            }
        }

        private static int GetIndexOfDigit(string value)
        {
            for (int i = 0; i < value.Length; i++)
                if (char.IsDigit(value[i]))
                    return i;
            return -1;
        }
    }
}
