using System;

namespace Repetite
{
    public class Input
    {
        public InputType Type { get; }
        public string Name { get; }
        public object DefaultValue { get; }

        public Input(InputType type, string name, object defaultValue)
        {
            Type = type;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DefaultValue = defaultValue;
        }

        public bool CanReceive(OutputType outputType)
        {
            switch (Type)
            {
                case InputType.Any:
                    return true;
                case InputType.Integer:
                    return outputType == OutputType.Integer;
                default:
                    return false;
            }
        }

        public bool CanReceive(object value)
        {
            switch (Type)
            {
                case InputType.Any:
                    return true;
                case InputType.Integer:
                    return value is int;
                default:
                    return false;
            }
        }

        protected bool Equals(Input other)
        {
            return Type == other.Type && string.Equals(Name, other.Name) && Equals(DefaultValue, other.DefaultValue);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Input) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Type;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (DefaultValue != null ? DefaultValue.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Input a, Input b)
        {
            return Equals(a, b);
        }

        public static bool operator !=(Input a, Input b)
        {
            return !(a == b);
        }
    }
}