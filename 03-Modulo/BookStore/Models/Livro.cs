using BookStore.Models.Interface;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Livro : IModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int Edicao { get; set; }
        public string Editora { get; set; }
        public string ISBN { get; set; }
        public string Error { get; private set; }

        public bool EstaValida()
        {
            Error = string.Empty;

            if (string.IsNullOrEmpty(Error)) 
            {
                Error = "Titulo e obrigatorio";
            }

            return !string.IsNullOrEmpty(Error);
        } 
    }
}