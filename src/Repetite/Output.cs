using System;

namespace Repetite
{
    public class Output
    {
        public OutputType Type { get; }
        public string Name { get; }

        public Output(OutputType type, string name)
        {
            Type = type;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        protected bool Equals(Output other)
        {
            return Type == other.Type && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Output) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Type * 397) ^ Name.GetHashCode();
            }
        }

        public static bool operator ==(Output a, Output b)
        {
            return Equals(a, b);
        }

        public static bool operator !=(Output a, Output b)
        {
            return !(a == b);
        }
    }
}