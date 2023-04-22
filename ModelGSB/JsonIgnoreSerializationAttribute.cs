using System;

namespace ModelGSB
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    internal class JsonIgnoreSerializationAttribute : Attribute
    {
    }
}