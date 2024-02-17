using Microsoft.Extensions.Hosting;
using Spectre.Console.Cli;

namespace henry_js.Cli.DependencyInjection
{
    public sealed class TypeResolver : ITypeResolver, IDisposable
    {
        private readonly IHost _host;

        public TypeResolver(IHost provider)
        {
            _host = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public object? Resolve(Type? type)
        {
            return type == null ? null : _host.Services.GetService(type);
        }

        public void Dispose()
        {
            if (_host is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
