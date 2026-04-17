using System.Reflection;

namespace Framwork;

public class ServiceCollection
{
    private readonly List<Type> controllerTypes = [];

    public void AddControllers()
    {
        var controllers = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && t.Name.EndsWith("Controller"));
        
        controllerTypes.AddRange(controllers);
    }

    public List<Type> GetControllerTypes()
    {
        return controllerTypes;
    }
}