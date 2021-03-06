﻿using System;
using System.Collections.Generic;
using distlib;
using distlib.samplers;
using compartments.emod;
using compartments.solvers.solverbase;

namespace compartments.solvers
{
    public class MidPoint : TauLeaping
    {

#pragma warning disable 169
        IModelBuilder _modelbuilder = new MpModelBuilder();
#pragma warning restore 169
        private DistributionSampler _distributionSampler;

        public MidPoint(ModelInfo modelInfo, float duration, int repeats, int samples)
            : base(modelInfo, duration, repeats, samples,new MpModelBuilder())
        {
            Configuration config = Configuration.CurrentConfiguration;
            _distributionSampler = RandLibSampler.CreateRandLibSampler(rng);

            epsilon = config.GetParameterWithDefault("midpoint.epsilon", 0.01f);
            nc = config.GetParameterWithDefault("midpoint.Nc", 2);
            multiple = config.GetParameterWithDefault("midpoint.Multiple", 10);
            SSAruns = config.GetParameterWithDefault("midpoint.SSARuns", 100);

            CheckParameters();

            Console.WriteLine("epsilon =  {0}", epsilon);
            Console.WriteLine("nc =       {0}", nc);
            Console.WriteLine("multiple = {0}", multiple);
            Console.WriteLine("SSA runs = {0}", SSAruns);  
        }

        private void CheckParameters()
        {
            if (epsilon <= 0)
                throw new ApplicationException("Epsilon was set to less than or equal to zero.");

            if (nc <= 0)
                throw new ApplicationException("Nc was et to less than zero or equal to zero.");

            if (multiple <= 0)
                throw new ApplicationException("Multiple was set to less than or equal to zero.");

            if (SSAruns < 1)
                throw new ApplicationException("SSAruns was set to less than one.");
        }

        protected override void FireNonCriticalReactions(float tauMin, List<Reaction> subreactions, float[] noncritrates)
        {
            int[] recordIntermediateReactions = new int[subreactions.Count];

            //Compute Intermediate rates and "remember" how many reactions for the half step

            for (int jReaction = 0; jReaction < subreactions.Count; jReaction++)
            {
                float change = noncritrates[jReaction] * tauMin;
                int howManyReactions = _distributionSampler.GeneratePoisson(change);

                recordIntermediateReactions[jReaction] = howManyReactions;

                float intermediateNumOfReactions = howManyReactions / 2.0f;

                FireReaction(subreactions[jReaction], intermediateNumOfReactions);
            }

            // Update Rates

            for (int jReaction = 0; jReaction < subreactions.Count; jReaction++)
            {
                noncritrates[jReaction] = subreactions[jReaction].Rate;
            }

            //Compute actual change in species 

            for (int jReaction = 0; jReaction < subreactions.Count; jReaction++)
            {
                float change = noncritrates[jReaction] * tauMin;
                int howManyReactions = _distributionSampler.GeneratePoisson(change);

                float delta = howManyReactions - recordIntermediateReactions[jReaction] / 2.0f; 

                FireReaction(subreactions[jReaction], delta);
            }
        }

        public void FireReaction(Reaction reaction, float delta)
        {
                foreach (SpeciesMP species in reaction.Reactants)
                    species.Decrement(delta);

                foreach (SpeciesMP species in reaction.Products)
                    species.Increment(delta);
        }

        public override string ToString()
        {
            return "Mid-Point";
        }
    }
}
