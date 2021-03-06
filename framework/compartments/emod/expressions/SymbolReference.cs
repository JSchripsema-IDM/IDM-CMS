﻿using System;
using System.Collections.Generic;
using compartments.emod.interfaces;

namespace compartments.emod.expressions
{
    public class SymbolReference : INumericOperator
    {
        private readonly string _name;
        private IValue _value;

        public SymbolReference(string name)
        {
            _name  = name;
            _value = null;
        }

        public IValue ResolveReferences(IDictionary<string, IValue> map)
        {
            try
            {
                return _value ?? (_value = map[_name]);
            }
            catch (KeyNotFoundException e)
            {
                Console.Error.Write(e);
                Console.Error.WriteLine($":\n\t'{_name}'");
                throw;
            }
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
