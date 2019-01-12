using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elysium.Utils
{
    public static class MvcUtils
    {
        public static void RemoveApplicationPartsFromAssemblyType<T>(this IMvcBuilder mvcBuilder)
        {
            if (mvcBuilder != null)
            {
                mvcBuilder.ConfigureApplicationPartManager(apm =>
                {
                    var formsLibrary = apm.ApplicationParts.Where(x => x.Name == typeof(T).Assembly.GetName().Name).FirstOrDefault();

                    if (formsLibrary != null)
                    {
                        apm.ApplicationParts.Remove(formsLibrary);
                    }
                });
            }
        }
    }
}
