using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjAgendaContatosOO
{
    internal class Contact
    {
        private string _id;
        public string Name { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public Address Address { get; set; }

        public Contact(string name, string phone)
        {
            var temp = Guid.NewGuid();
            _id = temp.ToString().Substring(0, 8);

            this.Name = name;
            this.Phone = phone;
            this.Address = new Address();
        }

        public Contact(string id, string name, string phone)
        {
            this._id = id;
            this.Name = name;
            this.Phone = phone;
            this.Address = new Address();
        }

        public override string ToString()
        {
            return $"\n\nDADOS DO CONTATO\n ID: {_id}\nNome: {Name}\nTelefone: {Phone}\nE-mail: {Email}\nEndereço: {Address}";
        }

        public  string ToFile()
        {
            return $"{_id}|{Name}|{Phone}|{Email}|{Address.ToFile()}";
        }

        public void EditPhone(string phone)
        {
            this.Phone = phone;
        }

        public void EditEmail(string email)
        {
            this.Email = email;
        }

        public void EditName(string name)
        {
            this.Name = name;
        }

    }
}
