using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elysium.Extensions
{
    public static class ApplicationPartExtensions
    {
        public static void AddApplicationPartsFromObjectAssembly(object objectAssembly, ApplicationPartManager partsManager)
        {
            var type = objectAssembly.GetType();

            if (!typeof(Service).IsAssignableFrom(type))
            {
                return;
            }

            bool needAddAppPart;
            do
            {

                var appFac = ApplicationPartFactory.GetApplicationPartFactory(type.Assembly);
                var parts = appFac.GetApplicationParts(type.Assembly);

                foreach (var part in parts)
                {
                    partsManager.ApplicationParts.Add(part);
                }

                type = type.BaseType;

                needAddAppPart = type != typeof(Service);

            }
            while (needAddAppPart);
        }

     
    }
}
