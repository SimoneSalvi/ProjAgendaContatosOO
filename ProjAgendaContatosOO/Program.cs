using System.Net.Http.Headers;
using ProjAgendaContatosOO;

internal class Program
{
    private static void Main(string[] args)
    {
        string contactsFile = "contactsFile.txt";

        List<Contact> phoneBook = new();

        phoneBook = LoadFromFile(contactsFile, phoneBook);

        bool flag = false;

        do
        {
            switch (Menu())
            {
                case 1:
                    phoneBook.Add(CreateContact(phoneBook));
                    ContactsToFile(phoneBook, contactsFile);
                    phoneBook.Clear();
                    break;
                case 2:
                    //                    EditContact();
                    break;
                case 3:
                    //                   phoneBook.Remove(DeletContact());
                    break;
                case 4:
                    PrintPhoneBook(phoneBook);
                    break;
                case 5:
                    System.Environment.Exit(0);
                    flag = true;
                    break;

            }
        } while (!flag);

        // Menu 
        int Menu()
        {
            Console.WriteLine("\n\nMENU DE OÇÕES:" +
                                "\n1 - Inserir Contato" +
                                "\n2 - Editar Contato" +
                                "\n3 - Remover contato" +
                                "\n4 - Mostrar agenda" +
                                "\n5 - Sair " +
                                "\n\nEscolha uma opção:");
            int op = int.Parse(Console.ReadLine());
            return op;
        }
    }

    private static List<Contact> LoadFromFile(string contactsFile, List<Contact> phoneBook)
    {
        if (File.Exists(contactsFile))
        {
            StreamReader sr = new(contactsFile);

            while(!sr.EndOfStream)
            {
                string[] contact = sr.ReadLine().Split("|");
                string id = contact[0];
                string name = contact[1];
                string phone = contact[2];
                string street = contact[3];
                string city = contact[4];
                string state = contact[5];
                string postalCode = contact[6];
                string country = contact[7];

                //$"{Street}|{City}|{State}|{PostalCode}|{Country}";

                Address temp = new();
                temp.Street = street;
                temp.City = city;
                temp.State = state;
                temp.PostalCode = postalCode;
                temp.Country = country;

                Contact contacto = new Contact(name, phone);
                contacto.Address = temp;

                phoneBook.Add(contacto);

            }
            sr.Close();
        }
        else
        {
            Console.WriteLine("Criando arquivo...");
        }

        return phoneBook;
    }

    //Método Criar Contato
    public static Contact CreateContact(List<Contact> l)
    {
        Console.WriteLine("Digite o nome:");
        string nome = Console.ReadLine();
        Console.WriteLine("Digite o telefone:");
        string telefone = Console.ReadLine();
        Contact contatoNovo;

        if (l.Any())
        {
            foreach (var item in l)
            {
                if (item.Phone.Equals(telefone))
                {
                    Console.WriteLine($"O Número de telefone já existe no contato {item.Name}");
                }
                else
                {
                    contatoNovo = new Contact(nome, telefone);
                    return contatoNovo;
                }
            }

        }
        else
        {
            contatoNovo = new Contact(nome, telefone);
            return contatoNovo;
        }

        return null;
    }

    // Método imprimir lista de contatos
    public static void PrintPhoneBook(List<Contact> l)
    {
        if (!l.Any())
        {
            Console.WriteLine("A agenda está vazia!");
        }
        else
        {
            /*int op;
            Console.WriteLine("\nEscolha uma opção:\n" +
                "1 - todos os contatos\n" +
                "2 - escolhar ");*/
            foreach (var item in l)
            {
                Console.WriteLine(item);
            }
        }
    }

    // Método para salvar qualquer arquivo
    public static void ContactsToFile(List<Contact> l, string p)
    {
        //StreamWriter sw = new(p);  
        try
        {
            if (File.Exists(p))
            {
                var txt = ReadFile(p);
                StreamWriter sw = new StreamWriter(p);
                sw.WriteLine(txt);
                foreach (Contact item in l)
                {
                    sw.WriteLine(item.ToFile());
                }

                sw.Close();

            }
            else
            {
                StreamWriter sw = new(p);
                foreach (Contact item in l)
                {
                    sw.WriteLine(item.ToFile());
                }

                sw.Close();
            }

        }
        catch (Exception e)
        {
            StreamWriter sw = new(p);
            p = "error.log";
            sw = new(p);

            sw.WriteLine(e.Message.ToString());

            sw.Close();
        }

    }

    // metodo para Ler o arquivo
    private static object ReadFile(string p)
    {
        {
            StreamReader sr = new StreamReader(p);
            string text = "";
            try
            {
                text = sr.ReadToEnd();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sr.Close();
            }
            return text;
        }
    }
}