using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BoVoyage.Framework.UI
{
    public sealed class InformationAffichage
    {
        private InformationAffichage(PropertyInfo propriete, string entete, int nombreCaracteres)
        {
            this.Propriete = propriete;
            this.Entete = entete;
            this.NombreCaracteres = nombreCaracteres;
        }

        /// <summary>
        /// R�cup�rer le nombre de caract�res maximum � afficher
        /// </summary>
        internal int NombreCaracteres { get; }

        private PropertyInfo Propriete { get; }

        private string Entete { get; }

        /// <summary>
        /// G�n�re une instance de la classe <see cref="InformationAffichage"/>.
        /// </summary>
        /// <typeparam name="T">Type de la classe</typeparam>
        /// <param name="propriete">Propri�t� de la classe � afficher.</param>
        /// <param name="entete">Libell� de la colonne</param>
        /// <param name="nombreCaracteres">Nombre de caract�res maximum � afficher</param>
        /// <returns></returns>
        public static InformationAffichage Creer<T>(Expression<Func<T, object>> propriete, string entete, int nombreCaracteres)
        {
            return new InformationAffichage(OutilsReflection.GetInformationPropriete(propriete), entete, nombreCaracteres);
        }

        internal string GetEntete()
        {
            return this.Entete.ToUpper().PadRight(this.NombreCaracteres);
        }

        internal string GetValeur(object element)
        {
            var renduTexte = string.Empty;
            var value = this.Propriete.GetValue(element);
            if (value != null)
            {
                renduTexte = value.ToString().Replace(Environment.NewLine, " ");
                if (value is DateTime date)
                {
                    renduTexte = date.ToShortDateString();
                }
            }

            return renduTexte.Tronquer(this.NombreCaracteres).PadRight(this.NombreCaracteres);
        }
    }
}