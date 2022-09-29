namespace SqlFluentBuilder.Validations;

public static class ArrayValidations
{

    public static void ItsNotEmpty(IEnumerable<object?> data, string name)
    {
        if (!data.Any())
            throw new ArgumentException($"The provided array of {name} is empty.", name);
    }
}