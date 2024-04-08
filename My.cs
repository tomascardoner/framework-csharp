// Simular (en parte) el objeto My de Visual Basic 2005 (o superior)
// Nuevas propiedades                                           (21/Nov/07)

using System.Diagnostics;
using System.Reflection;

namespace CardonerSistemas
{
    // My.Settings y My.Application.Info
    static class My
    {
        // My.Application.Info
        public static class Application
        {
            public static class Info
            {
                static readonly FileVersionInfo fvi;
                static readonly Assembly ensamblado;
                static readonly AssemblyName an;

                static Info()
                {
                    ensamblado = System.Reflection.Assembly.GetExecutingAssembly();
                    fvi = FileVersionInfo.GetVersionInfo(ensamblado.Location);
                    an = ensamblado.GetName();
                }

                /// <summary>
                /// La versión del ensamblado
                /// Equivale al atributo AssemblyVersion
                /// </summary>
                public static System.Version Version
                {
                    get
                    {
                        return an.Version;
                    }
                }

                /// <summary>
                /// La versión del ensamblado (FileVersion)
                /// equivale al atributo: AssemblyFileVersion
                /// </summary>
                public static System.Version FileVersion
                {
                    get
                    {
                        return new System.Version(fvi.FileVersion);
                    }
                }

                public static string Title
                {
                    get
                    {
                        return fvi.FileDescription;
                    }
                }
                public static string Copyright
                {
                    get
                    {
                        return fvi.LegalCopyright;
                    }
                }
                public static string ProductName
                {
                    get
                    {
                        return fvi.ProductName;
                    }
                }
                public static string CompanyName
                {
                    get
                    {
                        return fvi.CompanyName;
                    }
                }
                public static string Trademark
                {
                    get
                    {
                        return fvi.LegalTrademarks;
                    }
                }
                public static string Description
                {
                    get
                    {
                        return fvi.Comments;
                    }
                }
            }
        }
    }
}