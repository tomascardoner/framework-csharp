namespace CardonerSistemas
{
    static class Process
    {

        static public void Start(string processName)
        {
            try
            {
                System.Diagnostics.Process.Start(processName);
            }
            catch (System.Exception ex)
            {
                CardonerSistemas.Error.ProcessError(ex, $"Error al iniciar el proceso '{processName}'.");
            }
        }

    }
}
