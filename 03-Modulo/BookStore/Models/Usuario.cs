using BookStore.Models.Interface;

namespace BookStore.Models
{
    public class Usuario : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public string Error { get; private set; }

        public bool EstaValida()
        {
            if (string.IsNullOrEmpty(Name)) 
            {
                Error = "Nome e obrigatorio";
            }

            if (string.IsNullOrEmpty(Password))
            {
                Error = "Senha e obrigatorio";
            }

            return !string.IsNullOrEmpty(Error);
        }
    }
}
