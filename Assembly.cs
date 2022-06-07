
namespace CardonerSistemas.Assembly
{
    /// <summary>
    /// Call the asssembly dynamically and execute a method
    ///
    /// </summary>
    /// <param name="AssemblyName">Name of the Assembly to be loaded</param>
    /// <param name="className">Name of the class to be intantiated </param>
    /// <param name="methodName">Name of the method to be called</param>
    /// <param name="parameterForTheMethod">Parameters should be passed as object array</param>
    /// <returns>Returns as Generic object..</returns>

    public static object Process(string AssemblyName, string className, string methodName, object[] parameterForTheMethod)
    {
        object returnObject = null;
        MethodInfo mi = null;
        ConstructorInfo ci = null;
        object responder = null;
        Type type = null;
        System.Type[] objectTypes;
        int count = 0;
        try
        {
            //Load the assembly and get it's information
            type = System.Reflection.Assembly.LoadFrom(AssemblyName  +   .dll").GetType(AssemblyName +"." +  className);
            //Get the Passed parameter types to find the method type
            objectTypes = new System.Type[parameterForTheMethod.GetUpperBound(0) + 1];
            foreach (object objectParameter in parameterForTheMethod)

            {
                if (objectParameter != null)
                    objectTypes[count] = objectParameter.GetType();
                count++;
            }

            //Get the reference of the method
            mi = type.GetMethod(methodName, objectTypes);
            ci = type.GetConstructor(Type.EmptyTypes);
            responder = ci.Invoke(null);
            //Invoke the method
            returnObject = mi.Invoke(responder, parameterForTheMethod);
        }
        catch (Exception ex)
        {
        throw ex;
        }
        finally
        {
            mi = null;
            ci = null;
            responder = null;
            type = null;
            objectTypes = null;
        }
        //Return the value as a generic object
        return returnObject;
        }
    }
}