using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Elysium.Infrastructure
{
    public class UnderServiceNamespaceControllerFeatureProvider : ControllerFeatureProvider 
    {
        private readonly Service service;

        public UnderServiceNamespaceControllerFeatureProvider(Service service)
        {
            this.service = service;
        }


        protected override bool IsController(TypeInfo typeInfo)
        {
            var serviceNameSpace = service.GetType().Namespace;

            if (!(typeInfo.Namespace?.StartsWith(serviceNameSpace) == true))
                return false;

            return base.IsController(typeInfo);
            
        }
    }
}
