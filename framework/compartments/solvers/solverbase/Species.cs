﻿using System;
using System.Collections.Generic;
using compartments.emod;
using compartments.emod.interfaces;

namespace compartments.solvers.solverbase
{
    public class Species : IValue, IUpdateable
    {
        private readonly IValue _initialPopulation;

        public Species(SpeciesDescription speciesDescription, IDictionary<string, IValue> map)
        {
            Description = speciesDescription;
            _initialPopulation = speciesDescription.InitialPopulation.ResolveReferences(map);
        }

        public SpeciesDescription Description { get; private set; }
        public string Name { get { return Description.Name; } }
        public LocaleInfo Locale { get { return Description.Locale; } }

        public virtual int Count { get; set; }

        public virtual float Value
        {
            get { return Count; }
            set { ;}
        }

        public int Reset()
        {
            Count = Math.Max((int)Math.Round(_initialPopulation.Value), 0);
            return Count;
        }

        public virtual void Update(float value)
        {
            Count = (int) value;
        }

        public virtual int Increment() { return ++Count; }
        public virtual int Increment(int delta) { return Count += delta; }
        public virtual int Decrement() { return --Count; }
        public virtual int Decrement(int delta) { return Count -= delta; }

        public override string ToString()
        {
            return Description.ToString();
        }
    }
}