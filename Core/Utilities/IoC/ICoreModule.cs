using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    public interface ICoreModule // Her projemde kullanmam gereken injectionlar
    {
        void Load(IServiceCollection serviceCllection);
    }
}
