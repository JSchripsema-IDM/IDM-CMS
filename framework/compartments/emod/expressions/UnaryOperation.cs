﻿using System;
using compartments.emod.interfaces;

namespace compartments.emod.expressions
{
    public abstract class UnaryOperation : IValue
    {
        protected IValue Argument;

        protected UnaryOperation(IValue argument)
        {
            Argument = argument;
        }

        public abstract float Value { get; }
    }

    // Exp, Log, Sin, Cos, Abs, Floor, Ceil, Sqrt, Neg, HeavisideStep

    public class Exponentiate : UnaryOperation
    {
        public Exponentiate(IValue argument) : base(argument)
        {
        }

        public override float Value
        {
            get { return (float) Math.Exp(Argument.Value); }
        }
    }

    public class Logarithm : UnaryOperation
    {
        public Logarithm(IValue argument) : base(argument)
        {
        }

        public override float Value
        {
            get { return (float) Math.Log(Argument.Value); }
        }
    }

    public class Sine : UnaryOperation
    {
        public Sine(IValue argument) : base(argument)
        {
        }

        public override float Value
        {
            get { return (float) Math.Sin(Argument.Value); }
        }
    }

    public class Cosine : UnaryOperation
    {
        public Cosine(IValue argument) : base(argument)
        {
        }

        public override float Value
        {
            get { return (float) Math.Cos(Argument.Value); }
        }
    }

    // Tangent
    // ArcSine
    // ArcCosine
    // ArcTangent

    public class Absolute : UnaryOperation
    {
        public Absolute(IValue argument) : base(argument)
        {
        }

        public override float Value
        {
            get { return Math.Abs(Argument.Value); }
        }
    }

    public class Floor : UnaryOperation
    {
        public Floor(IValue argument) : base(argument)
        {
        }

        public override float Value
        {
            get { return (float) Math.Floor(Argument.Value); }
        }
    }

    public class Ceiling : UnaryOperation
    {
        public Ceiling(IValue argument) : base(argument)
        {
        }

        public override float Value
        {
            get { return (float) Math.Ceiling(Argument.Value); }
        }
    }

    public class Sqrt : UnaryOperation
    {
        public Sqrt(IValue argument) : base(argument)
        {
        }

        public override float Value
        {
            get { return (float) Math.Sqrt(Argument.Value); }
        }
    }

    public class Negate : UnaryOperation
    {
        public Negate(IValue argument) : base(argument)
        {
        }

        public override float Value
        {
            get { return -Argument.Value; }
        }
    }

    public class HeavisideStep : UnaryOperation
    {
        public HeavisideStep(IValue argument) : base(argument)
        {
        }

        public override float Value
        {
            get { return Argument.Value >= 0.0f ? 1.0f : 0.0f; }
        }
    }
}
