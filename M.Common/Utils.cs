using System;

namespace M.Common
{
    public enum ValueAttributeType
    {
        Default = 1,
        ViewModel = 2,
        DataModel = 4,

    }

    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = true)]
    public class EnumValueAttribute : Attribute
    {
        public EnumValueAttribute(string value) : this(string.Empty, value) { }

        public EnumValueAttribute(string modelType, string value)
        {
            ModelType = modelType ?? string.Empty;
            Value = value ?? string.Empty;
        }

        public string ModelType { get; }

        public string Value { get; }

        public override bool Equals(object obj)
        {
            return obj is EnumValueAttribute attribute &&
                   ModelType == attribute.ModelType &&
                   Value == attribute.Value;
        }

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(ModelType, Value);
        //}

        public override bool Match(object obj)
        {
            return Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ModelType, Value);
        }
    }
}
