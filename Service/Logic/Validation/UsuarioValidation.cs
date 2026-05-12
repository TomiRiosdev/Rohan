using System;
using System.Text.RegularExpressions;

namespace Service.Logic.Validation
{
    public static class UsuarioValidator
    {
        public static void Validar(string username, string nombre, string email, string password, string telefono)
        {
            // 1. Validar Campos Vacíos
            if (string.IsNullOrWhiteSpace(username)) throw new Exception("El nombre de usuario es obligatorio.");
            if (string.IsNullOrWhiteSpace(nombre)) throw new Exception("El nombre y apellido son obligatorios.");
            if (string.IsNullOrWhiteSpace(email)) throw new Exception("El correo electrónico es obligatorio.");
            if (string.IsNullOrWhiteSpace(password)) throw new Exception("La contraseña no puede estar vacía.");
           

            // 2. Validar Formato de Email (Regex)
            string expresionEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, expresionEmail))
            {
                throw new Exception("El formato del correo electrónico no es válido.");
            }

            // 3. Validar Longitud del Username (ej: mínimo 4 caracteres)
            if (username.Length < 4)
            {
                throw new Exception("El nombre de usuario debe tener al menos 4 caracteres.");
            }

            // 4. Validar Fortaleza de Contraseña (ej: mínimo 8 caracteres)
            if (password.Length < 8)
            {
                throw new Exception("La contraseña debe tener al menos 8 caracteres para mayor seguridad.");
            }

            // 5. Validar Teléfono (opcional pero con formato si existe)
            if (!string.IsNullOrWhiteSpace(telefono))
            {
                // Solo números, guiones o espacios
                string expresionTel = @"^[0-9\s\-]+$";
                if (!Regex.IsMatch(telefono, expresionTel))
                {
                    throw new Exception("El teléfono solo puede contener números, espacios o guiones.");
                }
            }
        }
    }
}
