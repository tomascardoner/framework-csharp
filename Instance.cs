namespace CardonerSistemas
{
    static class Instance
    {
        static public bool IsRunningUnderIDE()
        {
            return System.Diagnostics.Debugger.IsAttached;
        }
    }
}
