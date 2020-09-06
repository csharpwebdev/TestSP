using Funq;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task1.Services;

namespace Task1
{
    public class ProductsAppHost : AppHostBase
    {
        public ProductsAppHost(): base("Products Service", typeof(ProductsService).Assembly)
        {

        }

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Container container)
        {
            //Register Redis Client Manager singleton in ServiceStack's built-in Func IOC
            // container.Register<IRedisClientsManager>(new BasicRedisClientManager("localhost"));
        }
    }

}
