using System.Text.Json.Serialization;

namespace Useful_training.Core.NeuralNetwork.ValueObject;

[Serializable]
public class DataSet : IEquatable<DataSet>
{
	public List<double> Inputs { get; } // un ValueObject doit toujours etre immutable sinon ce n'est plus un valueobject
	public List<double> TargetOutput { get; } 
		
	[JsonConstructor]
	public DataSet()
	{
		Inputs = new List<double>();
		TargetOutput = new List<double>();
	}
	public DataSet(List<double> values, List<double> targets)
	{
		Inputs = values;
		TargetOutput = targets;
	}
	
	// en general dans les ValueObjects on aime bien override les methodes d'egalité (et le GetHashCode())
	// car valueObject1 == valueObject2 devrait etre évalué à true si ils sont les memes valeurs
	// Ce qui est pratique c'est qu'en general ton IDE te propose de le faire pour toi en rajoutant l'interface IEquatable
	// Si tu veux encore moins t'embeter tu peux creer une class abstraite valueobject qui implemente IEquatable comme par exemple ici : https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects
	// Ou alors tu peux faire un systeme similaire qui detecte automatiquement les champs à comparer avec de la reflexion

	public bool Equals(DataSet? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Inputs.Equals(other.Inputs) && TargetOutput.Equals(other.TargetOutput);
	}

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
		return Equals((DataSet) obj);
	}

	public override int GetHashCode() 
		=> HashCode.Combine(Inputs, TargetOutput);

	public static bool operator ==(DataSet? left, DataSet? right) 
		=> Equals(left, right);

	public static bool operator !=(DataSet? left, DataSet? right) 
		=> !(left == right);
}