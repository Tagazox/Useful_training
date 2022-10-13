
using System.Reflection;

namespace Useful_training.Core.NeuralNetwork.ValueObject;

public abstract class ValueObject : IEquatable<object>
{
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if ((ReferenceEquals(left, null) ^ ReferenceEquals(right, null)) )
            return false;
        
        return ReferenceEquals(left, right) || left!.Equals(right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left== right);

    }
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        
        ValueObject other = (ValueObject)obj;
        return GetValuesOfThePropertyOfTheObject(this).SequenceEqual(GetValuesOfThePropertyOfTheObject(other));
    }
    
    public override int GetHashCode()
    {
        return GetValuesOfThePropertyOfTheObject(this)
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    private static IEnumerable<object?> GetValuesOfThePropertyOfTheObject(object obj)
    {
        return obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic).Select(p => p.GetValue(obj));
    }
}