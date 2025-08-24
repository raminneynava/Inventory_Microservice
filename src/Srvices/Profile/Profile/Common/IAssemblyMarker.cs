using System.Reflection;

namespace Profile.Common
{
    public interface IAssemblyMarker
    {
    }

    public static class AssemblyMarker
    {
        private static readonly Lazy<Assembly> _currentAssembly =
            new Lazy<Assembly>(() => typeof(IAssemblyMarker).Assembly);

        public static Assembly Assembly => _currentAssembly.Value;
    }
}
