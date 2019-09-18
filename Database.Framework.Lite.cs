using System;

namespace CardonerSistemas.Database.Framework
{
    static partial class Lite
    {

        #region Gender

        private const string EntityFemaleArticle = "la";
        private const string EntityFemalePluralArticle = "las";
        private const string EntityMaleArticle = "el";
        private const string EntityMalePluralArticle = "los";

        public static string GetEntityGenderArticle(bool isFemale)
        {
            return (isFemale ? EntityFemaleArticle : EntityMaleArticle);
        }

        public static string GetEntityGenderArticle(bool isFemale, bool isPlural)
        {
            if (isPlural)
            {
                return GetEntityGenderPluralArticle(isFemale);
            }
            else
            {
                return GetEntityGenderArticle(isFemale);
            }
        }

        public static string GetEntityGenderPluralArticle(bool isFemale)
        {
            return (isFemale ? EntityFemalePluralArticle : EntityMalePluralArticle);
        }

        #endregion

        #region Error messages

        private const string EntityLoadErrorMessage = "Error al cargar {0} {1} por Id.";
        private const string EntityLoadPropertiesErrorMessage = "Error al cargar las propiedades de {0} {1} por Id.";
        private const string EntityRelatedLoadErrorMessage = "Error al cargar {2} {3} de {0} {1}.";

        public static string GetEntityLoadErrorMessage(string entityDisplayName, bool isFemale)
        {
            return String.Format(EntityLoadErrorMessage, GetEntityGenderArticle(isFemale), entityDisplayName);
        }

        public static string GetEntityLoadPropertiesErrorMessage(string entityDisplayName, bool isFemale)
        {
            return String.Format(EntityLoadPropertiesErrorMessage, GetEntityGenderArticle(isFemale), entityDisplayName);
        }

        public static string GetEntityRelatedLoadErrorMessage(string entityDisplayName, bool isFemale, string entityRelatedDisplayName, bool relatedIsFemale, bool relatedIsPlural)
        {
            return String.Format(EntityRelatedLoadErrorMessage, GetEntityGenderArticle(isFemale), entityDisplayName, GetEntityGenderArticle(relatedIsFemale, relatedIsPlural), entityRelatedDisplayName);
        }

        #endregion

    }
}