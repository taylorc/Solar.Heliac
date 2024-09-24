using Ardalis.GuardClauses;
using System.Text;

namespace Solar.Heliac.Domain.Common.Guards;
public static class AddressGuard
{
    public static void Address(this IGuardClause guardClause,
       IDictionary<string,string> input)
    {
        IList<string> validation = new List<string>();
        foreach (var key in input) {
            if (string.IsNullOrEmpty(key.Value))
            {
                validation.Add(key.Key);
            }
        }

        string wereWas = validation.Count > 1 ? "were" : "was";

        if(validation.Any())
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in validation)
            {
                sb.Append($"{item} ");
            }

            sb.Append($"{wereWas} empty.");

            throw new ArgumentException(sb.ToString());
        }
    }
}
