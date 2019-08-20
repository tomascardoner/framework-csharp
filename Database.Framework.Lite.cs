using System;

namespace CardonerSistemas.Database.Framework
{
    static partial class Lite
    {
        private const string EntityLoadErrorMessage = "Error al cargar {0} {1} por Id.";
        private const string EntityLoadPropertiesErrorMessage = "Error al cargar las propiedades de {0} {1} por Id.";
        private const string EntityFemaleArticle = "la";
        private const string EntityMaleArticle = "el";

        public static string GetEntityGenderArticle(bool EntityDisplayNameIsFemale)
        {
            return (EntityDisplayNameIsFemale ? EntityFemaleArticle : EntityMaleArticle);
        }

        public static string GetEntityLoadErrorMessage(string EntityDisplayName, bool EntityDisplayNameIsFemale)
        {
            return String.Format(EntityLoadErrorMessage, GetEntityGenderArticle(EntityDisplayNameIsFemale), EntityDisplayName);
        }

        public static string GetEntityLoadPropertiesErrorMessage(string EntityDisplayName, bool EntityDisplayNameIsFemale)
        {
            return String.Format(EntityLoadPropertiesErrorMessage, GetEntityGenderArticle(EntityDisplayNameIsFemale), EntityDisplayName);
        }
    }
}