﻿using compartments.emod.interfaces;

namespace compartments.emod.expressions
{
    public abstract class ComparisonOperation : IBoolean
    {
        protected IValue leftValue;
        protected IValue rightValue;

        protected ComparisonOperation(IValue left, IValue right)
        {
            leftValue  = left;
            rightValue = right;
        }

        public abstract bool Value { get; }
    }

    public class LessThan : ComparisonOperation
    {
        public LessThan(IValue left, IValue right) : base(left, right)
        {
        }

        public override bool Value
        {
            get { return leftValue.Value < rightValue.Value; }
        }
    }

    public class LessThanOrEqual : ComparisonOperation
    {
        public LessThanOrEqual(IValue left, IValue right) : base(left, right)
        {
        }

        public override bool Value
        {
            get { return leftValue.Value <= rightValue.Value; }
        }
    }

    public class GreaterThan : ComparisonOperation
    {
        public GreaterThan(IValue left, IValue right) : base(left, right)
        {
        }

        public override bool Value
        {
            get { return leftValue.Value > rightValue.Value; }
        }
    }

    public class GreaterThanOrEqual : ComparisonOperation
    {
        public GreaterThanOrEqual(IValue left, IValue right) : base(left, right)
        {
        }

        public override bool Value
        {
            get { return leftValue.Value >= rightValue.Value; }
        }
    }

    public class EqualTo : ComparisonOperation
    {
        public EqualTo(IValue left, IValue right) : base(left, right)
        {
        }

        public override bool Value
        {
            get { return leftValue.Value == rightValue.Value; }
        }
    }

    public class NotEqualTo : ComparisonOperation
    {
        public NotEqualTo(IValue left, IValue right) : base(left, right)
        {
        }

        public override bool Value
        {
            get { return leftValue.Value != rightValue.Value; }
        }
    }
}
