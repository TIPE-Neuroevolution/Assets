﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Neuroevolution
{
    static class Counters
    {
        private static int currentGeneration;
        private static int genomeCount;
        private static int speciesCount;
        private static Dictionary<int, int> startGenerationPerSpecies = new Dictionary<int, int>();


        public static int CurrentGeneration
        {
            get
            {
                return currentGeneration;
            }
        }

        public static int GenomeCount
        {
            get
            {
                genomeCount++;
                return genomeCount - 1;
            }
        }

        public static int SpeciesCount
        {
            get
            {
                AddSpecies(speciesCount);
                speciesCount++;
                return speciesCount - 1;
            }
        }


        public static void AddSpecies(int species)
        {
            startGenerationPerSpecies.Add(species, CurrentGeneration);
        }

        public static float GetVariation(int species, float variation)
        {
            if (variation >= 0)
            {
                return variation;
            }
            else
            {
                int i;
                if (startGenerationPerSpecies.TryGetValue(species, out i))
                {
                    if (Globals.Log)
                    {
                        return -variation / Mathf.Log10(CurrentGeneration - i + 10);
                    }
                    else
                    {
                        return -variation / (CurrentGeneration - i + 1);
                    }
                }
                else
                {
                    throw new Exception("Bad species");
                }
            }
        }

        public static void AddGeneration()
        {
            currentGeneration++;
        }


        public static void Reset()
        {
            genomeCount = 0;
            speciesCount = 0;
            startGenerationPerSpecies.Clear();
            currentGeneration = 0;
        }

    }
}
