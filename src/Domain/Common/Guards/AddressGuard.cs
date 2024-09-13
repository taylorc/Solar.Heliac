using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
            sb.AppendLine("Required input ");
            foreach (var item in validation)
            {
                sb.Append($"{item} ");
            }

            sb.AppendLine($"{wereWas} empty.");

            throw new ArgumentException(sb.ToString());
        }
    }
}
