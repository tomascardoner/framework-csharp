namespace CardonerSistemas
{
    static class Instance
    {
        static public bool IsRunningUnderIde()
        {
            return System.Diagnostics.Debugger.IsAttached;
        }
    }
}
