using System;
using System.Collections.Generic;
using System.Text;

namespace Elysium.Extensions
{
    public static class StringExtensions
    {
        public static string SlashBranchName(this string branch)
        {
            return branch.StartsWith("/") ? branch : "/" + branch;
        }
    }
}
