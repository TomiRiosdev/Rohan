using System;


namespace Service.DomainModel.Exceptions
{
    internal class WordNotFoundException : Exception
    {
        public int CodigoPersonalizado { get; }

        public WordNotFoundException() : base("Palabra no encontrada")
        {
            this.Source = "?";
            this.HelpLink = "?";
            this.CodigoPersonalizado = 10;

            //Envío un correo...
        }
    }
}
